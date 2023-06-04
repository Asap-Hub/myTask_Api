using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myShop.Application.Command.User;
using myShop.Application.Dto.User;
using myShop.Domain.Model;
using myShop.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myShop.Api.Controllers.user
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailServices _emailServices;

        

        public UserController(IMediator mediator, IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailServices emailServices )
        {
            _mediator = mediator;
           _configuration = configuration;
           _userManager = userManager;
           _signInManager = signInManager;
            _emailServices = emailServices;
        }

        //testing jwt 
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential )
        {

            if(credential.userName == "asap" && credential.Password == "1234")
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "asap"),
                    new Claim(ClaimTypes.Email, "asap@gmail.com" ),
                    new Claim("Admin", "true")
                };


                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok( new
                {
                    access_token = CreateToken(Claims, expiresAt),
                    expires_at = expiresAt,
                });
            }
            ModelState.AddModelError("UnAuthorized", "you are not authorized to access the endpoint");
            return Unauthorized(ModelState);
        }


        //testing creating using userManager
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> createUserUsingManager([FromBody] TblUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new IdentityUser
            {
                Email = user.Email,
                UserName = user.PassWord

            };

            var createUser = await _userManager.CreateAsync(dto, user.PassWord);
            if(createUser.Succeeded)
            {
                var confirmEmail = await _userManager.GenerateEmailConfirmationTokenAsync(dto);
                // return Ok( value: new {
                //     User = dto.Id,
                //     Token = confirmEmail,
                //});
                var verificationLink = Url.PageLink(pageName: "SOME ROUTE",
                             values: new
                             {
                                 User = dto.Id,
                                 Token = confirmEmail,
                             }
                    );

                await _emailServices.sendEmailAsync("ab@gmail.com",
                    user.Email!,
                    "Please confirm your email",
                    $"please click on this link to confirms your email address:{verificationLink}"
                    );

            }
            else
            {
                foreach(var error in createUser.Errors)
                {
                    ModelState.AddModelError("Registration", error.Description);
                }
            }
            return BadRequest(ModelState);

        }



        //loging user out
        [HttpPost]
        public async Task<IActionResult> logOut()
        {
           await _signInManager.SignOutAsync();
            return Ok();
        }


        //use to confirm account of user
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> confirmAccount( string userID, string Token )
        {
            var findUser = await _userManager.FindByIdAsync(userID);

            if(findUser != null)
            {
                var result = await _userManager.ConfirmEmailAsync(findUser, Token);
                if (result.Succeeded)
                {
                    return Ok (value: new Message { message = "Email Confirmation was successful" });
                   
                }
            }
            return BadRequest(error: new Message { message = "Email Confirmation was Unsuccessful" });
        }


        //for loging in using a user
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public  async Task<IActionResult> LogUserIn([FromBody] TblUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signUserIn = await _signInManager.PasswordSignInAsync(
                user.Email,
                user.PassWord,
                user.RememberMe, 
                false);

            if(signUserIn.Succeeded)
            {

                //you can use createAtRoute and pass the route to home page 
                //use claims to assign to other pages
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (signUserIn.RequiresTwoFactor)
                {
                    return RedirectToPage("/LoginUsingTwoFactor",
                        new
                        {
                            Email = user.Email,
                            RememberMe = user.RememberMe,
                        }
                        );
                }

                if (signUserIn.IsLockedOut)
                {
                    ModelState.AddModelError("Alert", "You are LockedOut");
                }
                else
                {
                    ModelState.AddModelError("Error", "Failed to login");
                }
            }
            return BadRequest();
        }



        //for using two factor loging
        public EmailMFA emailMFA { get; set; }

        [HttpGet]
        public async Task<IActionResult> LoginUsingTwoFactor(string Email, bool RememberMe)
        {


            //generate the authentication code
            var findUser = await _userManager.FindByEmailAsync(Email);

            emailMFA.securityCode = string.Empty;
            emailMFA.RememberMe = RememberMe;

            var getSecurityCode = await _userManager.GenerateTwoFactorTokenAsync(findUser, "Email");

            //send the authentication code

               await _emailServices.sendEmailAsync(
                "ab@gmail.com",
                Email,
                "Asap-Hub OTP Code",
                $"use this otp to login into your app.{getSecurityCode}"
                );
            return Ok();
             
        }


        //verifing the user using two face

        [HttpPost]  
        public async Task<IActionResult> verifyTwoFactorLogging([FromBody] EmailMFA emailMFA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.TwoFactorSignInAsync("Email", emailMFA.securityCode, emailMFA.RememberMe, false);


            if (result.Succeeded)
            {

                //you can use createAtRoute and pass the route to home page 
                //use claims to assign to other pages
                return RedirectToAction(nameof(Index));
            }
            else
            { 
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login2FA", "You are LockedOut");
                }
                else
                {
                    ModelState.AddModelError("Login2FA", "Failed to login");
                }
            } 
            return Ok();
        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secreteKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")); 

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secreteKey),
                    SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwt);   
        }


       

        //create user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> createUser( [FromBody] CreateUserDto createUserDto)
        {
            var user = await _mediator.Send(new createUserCommand { createUserDto = createUserDto });

            return NoContent();
        }


        //getting login user
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> login([FromQuery] string userName, string firstPassword, string confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logMein = await _mediator.Send(new GetloginCommand { userName = userName, firstPassword = firstPassword, confirmPassword = confirmPassword });
            if (logMein != null)
            {

                return Ok(logMein);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }


    public class EmailMFA
    {
        public string? securityCode { get; set; }
        public bool RememberMe { get; set; }
    }

    public class Message
    {
        public string message { get; set; }
    }

    public class Credential
    {
        public string? userName { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
    public class findUser
    {
        public string? userID { get; set; } = null!;
        public string? Token { get; set; } = null!;
    }

    public class ConfirmEmailMessage
    {
        public int? UserID { get; set; }
        public string? Token { get; set; }
    }
}
