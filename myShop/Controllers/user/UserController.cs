using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myShop.Application.Command.User;
using myShop.Application.Dto.User;
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
        private readonly IConfiguration configuration;

        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            this.configuration = configuration;
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

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secreteKey = Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")); 

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


    public class Credential
    {
        public string? userName { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
