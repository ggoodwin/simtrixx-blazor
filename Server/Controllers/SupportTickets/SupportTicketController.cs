using Application.Features.SupportTickets.Commands.AddEdit;
using Application.Features.SupportTickets.Commands.Delete;
using Application.Features.SupportTickets.Queries.GetAll;
using Application.Features.SupportTickets.Queries.GetAllByStatus;
using Application.Features.SupportTickets.Queries.GetAllByUserId;
using Application.Features.SupportTickets.Queries.GetById;
using Common.Constants.Permission;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.SupportTickets
{
    public class SupportTicketController : BaseApiController<SupportTicketController>
    {
        [Authorize(Policy = Permissions.SupportTickets.ViewAll)]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllSupportTicketsAsync()
        {
            var supportTickets = await _mediator.Send(new GetAllSupportTicketsQuery());
            return Ok(supportTickets);
        }

        [Authorize(Policy = Permissions.SupportTickets.ViewAllStatus)]
        [HttpGet]
        [Route("bystatus/{status}")]
        public async Task<IActionResult> GetAllSupportTicketsByStatusAsync(SupportStatus status)
        {
            var supportTickets = await _mediator.Send(new GetAllSupportTicketsByStatusQuery() { Status = status });
            return Ok(supportTickets);
        }

        [HttpGet]
        [Route("byuser/{userId}")]
        public async Task<IActionResult> GetAllSupportTicketsByUserAsync(string userId)
        {
            var supportTickets = await _mediator.Send(new GetAllSupportTicketsByUserIdQuery() { UserId = userId });
            return Ok(supportTickets);
        }

        [Authorize(Policy = Permissions.SupportTickets.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupportTicketByIdAsync(int id)
        {
            var supportTickets = await _mediator.Send(new GetSupportReplyByIdQuery() { Id = id });
            return Ok(supportTickets);
        }

        [HttpPost]
        public async Task<IActionResult> AddSupportTicketAsync(AddEditSupportTicketCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.SupportTickets.Delete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteSupportTicketAsync(DeleteSupportTicketCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
