using MediatR;
using Microsoft.AspNetCore.Mvc;
using myShop.Application.Command.User;
using myShop.Application.Dto.User;

namespace myShop.Api.Controllers.user
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class createUserController : Controller
    {
        private readonly IMediator _mediator;

        public createUserController(IMediator mediator)
        {
            _mediator = mediator;
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
}
