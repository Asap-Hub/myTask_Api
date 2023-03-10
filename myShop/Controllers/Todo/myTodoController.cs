using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myShop.Application.Command.myTodo;
using myShop.Application.Dto.Todo;

namespace myShop.Api.Controllers.Todo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class myTodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public myTodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //creating myTodo
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateMyTodo([FromBody] CreateTodoDto createDto) {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            var createTodo = await _mediator.Send(new CreateMyTodoCommand { createDto = createDto});
            return NoContent();
        }
    }
}
