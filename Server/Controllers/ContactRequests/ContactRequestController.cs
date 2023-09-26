using Application.Features.ContactRequests.Commands.AddEdit;
using Application.Features.ContactRequests.Commands.Delete;
using Application.Features.ContactRequests.Queries.GetAll;
using Application.Features.ContactRequests.Queries.GetById;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.ContactRequests
{
    public class ContactRequestController : BaseApiController<ContactRequestController>
    {
        [Authorize(Policy = Permissions.ContactRequests.View)]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllContactRequestsAsync()
        {
            var contactRequests = await _mediator.Send(new GetAllContactRequestsQuery());
            return Ok(contactRequests);
        }

        [Authorize(Policy = Permissions.ContactRequests.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactRequestByIdAsync(int id)
        {
            var contactRequest = await _mediator.Send(new GetContactRequestByIdQuery() { Id = id });
            return Ok(contactRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddContactRequestAsync(AddEditContactRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.ContactRequests.Delete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteContactRequestAsync(DeleteContactRequestCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
