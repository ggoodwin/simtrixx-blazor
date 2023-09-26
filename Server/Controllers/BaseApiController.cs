using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    /// <summary>
    /// Abstract BaseApi Controller Class
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private IConfiguration? _configurationInstance;
        private IMediator? _mediatorInstance;
        private ILogger<T>? _loggerInstance;
        protected IConfiguration? _config => _configurationInstance ??= HttpContext.RequestServices.GetService<IConfiguration>();
        protected IMediator? _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T>? _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
