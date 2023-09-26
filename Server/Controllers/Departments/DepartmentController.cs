using Application.Features.Departments.Commands.AddEdit;
using Application.Features.Departments.Commands.Delete;
using Application.Features.Departments.Queries.GetAll;
using Application.Features.Departments.Queries.GetById;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Departments
{
    public class DepartmentController : BaseApiController<DepartmentController>
    {
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var departments = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }

        [Authorize(Policy = Permissions.Departments.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(int id)
        {
            var department = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });
            return Ok(department);
        }

        [Authorize(Policy = Permissions.Departments.Add)]
        [HttpPost]
        public async Task<IActionResult> AddDepartmentAsync(AddEditDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Departments.Delete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentAsync(DeleteDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
