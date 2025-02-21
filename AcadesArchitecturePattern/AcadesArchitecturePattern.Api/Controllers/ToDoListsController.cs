using AcadesArchitecturePattern.Domain.Commands.ToDoLists;
using AcadesArchitecturePattern.Domain.Queries.ToDoLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/lists")]
    [ApiController]
    public class ToDoListsController(IMediator mediator) : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator mediator = mediator;

        // Commands:

        // Register a new list
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateToDoListCommand command)
        {
            var result = await mediator.Send(command);

            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Delete a list
        [HttpDelete("delete/{id?}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteToDoListCommand { Id = id };
            var result = await mediator.Send(command);
                               
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        // Queries:

        // List all lists
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListToDoListQuery();
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // Search list by id
        [HttpGet("search/{id?}")]
        public async Task<IActionResult> SearchById(Guid id)
        {
            var query = new SearchToDoListByIdQuery { Id = id };
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }
    }
}