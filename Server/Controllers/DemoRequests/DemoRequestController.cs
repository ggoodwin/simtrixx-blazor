using Application.Features.DemoRequests.Commands.AddEdit;
using Application.Features.DemoRequests.Commands.Delete;
using Application.Features.DemoRequests.Queries.GetAll;
using Application.Features.DemoRequests.Queries.GetById;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.DemoRequests
{
    public class DemoRequestController : BaseApiController<DemoRequestController>
    {
        [Authorize(Policy = Permissions.DemoRequests.View)]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllDemoRequestsAsync()
        {
            var demoRequests = await _mediator.Send(new GetAllDemoRequestsQuery());
            return Ok(demoRequests);
        }

        [Authorize(Policy = Permissions.DemoRequests.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDemoRequestByIdAsync(int id)
        {
            var demoRequest = await _mediator.Send(new GetDemoRequestByIdQuery() { Id = id });
            return Ok(demoRequest);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddDemoRequestAsync(AddEditDemoRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.DemoRequests.Delete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDemoRequestAsync(DeleteDemoRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
