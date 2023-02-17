using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftCorpTestApp.Core.Exceptions;

namespace SoftCorpTestApp.Api.Controllers
{
    /// <summary>
    /// Exception handler
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExceptionController : ControllerBase
    {
        [Route("exception")]
        public SoftCorpExceptionResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            var code = 500;

            if (exception is SoftCorpException httpException)
            {
                code = (int)httpException.Status;
            }

            Response.StatusCode = code;

            return new SoftCorpExceptionResponse(exception);
        }
    }
}
