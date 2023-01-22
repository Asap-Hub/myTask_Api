using MediatR;
using Microsoft.AspNetCore.Mvc;
using myShop.Application.Command.User;
using myShop.Application.Dto.User;

namespace myShop.Api.Controllers.user
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class loginController : Controller
    {
        private readonly IMediator mediator;

        public loginController(IMediator mediator)
        {
            this.mediator = mediator;
        }


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

            var logMein = await mediator.Send(new GetloginCommand { userName = userName, firstPassword = firstPassword, confirmPassword = confirmPassword});
            if(logMein != null)
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
