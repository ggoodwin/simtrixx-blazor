using Application.Features.Licenses.Commands.AddEdit;
using Application.Features.Licenses.Queries.GetAll;
using Application.Features.Licenses.Queries.GetById;
using Application.Features.Licenses.Queries.GetByUser;
using Application.Interfaces.Services.Models;
using Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Licenses
{
    public class LicenseController : BaseApiController<LicenseController>
    {
        private readonly ILicenseService _licenseService;

        [HttpGet]
        [Route("all")]
        [Authorize(Policy = Permissions.Licenses.ViewAll)]
        public async Task<IActionResult> GetAllLicensesAsync()
        {
            var licenses = await _mediator.Send(new GetAllLicensesQuery());
            return Ok(licenses);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Licenses.Create)]
        public async Task<IActionResult> AddLicensesAsync(AddEditLicenseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Policy = Permissions.Licenses.View)]
        public async Task<IActionResult> GetLicensesByUserAsync()
        {
            var licenses = await _mediator.Send(new GetLicensesByUserQuery());
            return Ok(licenses);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Licenses.View)]
        public async Task<IActionResult> GetLicensesByIdAsync(int id)
        {
            var license = await _mediator.Send(new GetLicenseByIdQuery { Id = id });
            return Ok(license);
        }

        [HttpGet("export")]
        [Authorize(Policy = Permissions.Licenses.Export)]
        public async Task<IActionResult> ExportExcel(string searchString = "")
        {
            var data = await _licenseService.ExportToExcelAsync(searchString);
            return Ok(data);
        }
    }
}
