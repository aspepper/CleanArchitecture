using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Queries.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }



        // Commands:

        // Register a new task
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Delete a task
        [HttpDelete("delete/{id?}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskCommand { Id = id };
            var result = await _mediator.Send(command);

            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        // Update a task
        [HttpPut("update/{id?}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }



        // Queries:

        // List all tasks
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListTaskQuery();
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // Search task by id
        [HttpGet("search/{id?}")]
        public async Task<IActionResult> SearchById(Guid id)
        {
            var query = new SearchTaskByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }
    }
}
