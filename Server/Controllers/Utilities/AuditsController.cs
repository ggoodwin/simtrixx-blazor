using Application.Interfaces.Services.Models;
using Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.Utilities
{
    [Authorize]
    public class AuditsController : BaseApiController<AuditsController>
    {
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;

        public AuditsController(IAuditService auditService, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _auditService = auditService;
        }

        /// <summary>
        /// Get Current User Audit Trails
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.AuditTrails.View)]
        [HttpGet]
        public async Task<IActionResult> GetUserTrailsAsync()
        {
            return Ok(await _auditService.GetCurrentUserTrailsAsync(_currentUserService.UserId));
        }

        /// <summary>
        /// Search Audit Trails and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchInOldValues"></param>
        /// <param name="searchInNewValues"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.AuditTrails.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false)
        {
            var data = await _auditService.ExportToExcelAsync(_currentUserService.UserId, searchString, searchInOldValues, searchInNewValues);
            return Ok(data);
        }
    }
}
