using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myShop.Application.Command.myTodo;
using myShop.Application.Dto.Todo;

namespace myShop.Api.Controllers.Todo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(policy:"AdminOnly")]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMyTodo([FromBody] CreateTodoDto createDto) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createTodo = await _mediator.Send(new CreateMyTodoCommand { createDto = createDto});
            //if (createTodo > 0) {
            //    var getresult = await _mediator.Send(new getMyTodoCommand {ID = createTodo });
            //    return Ok(getresult);
            //}
            return Ok(createTodo);
         }


        //getting all result
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getAllMyTodo()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var getAllResponse = await _mediator.Send(new GetAllMyTodoCommand { });

            if(getAllResponse.Count > 0) {
                return Ok(getAllResponse);
            }
            return NotFound();
            
        }

        //getting one result
        [HttpGet("${todoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyTodo([FromRoute] int todoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var getData = await _mediator.Send(new GetMyTodoCommand { ID = todoId });

            if(getData != null)
            {
                return Ok(getData);
            }
            return NotFound();
        }

        //updating record
        [HttpPut("${todoId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> updateMyTodo([FromRoute] int Id, [FromBody] UpdateTodoDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getTodo = await _mediator.Send(new GetMyTodoCommand { ID = Id });

            if(getTodo != null)
            {
                var updateData = await _mediator.Send(new UpdateMyTodoCommand { updateDto = updateDto });
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("${todoId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> deleteTodo([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var delete = await _mediator.Send(new DeleteMyTodoCommand { Id = Id });
            if(delete > 0)
            {
                return Ok(delete);
            }
            return NotFound();
        }
    }
}
