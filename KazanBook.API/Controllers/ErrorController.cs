using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KazanBook.Api.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ActionResult<FailedResponse> Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            if (exception is BAL.DALError)
            {
                HttpContext.Response.StatusCode = 404;
                //await HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new FailedResponse(exception.Message)));
                //await JsonSerializer.SerializeAsync(HttpContext.Response.Body, new FailedResponse(exception.Message));
                return new FailedResponse(exception.Message);
            }
            else
            {
                throw exception;
            }
        }
    }
    public class FailedResponse
    {
        public bool success { get; set; } = false;
        public string reason { get; set; }
        public FailedResponse(string reason)
        {
            this.reason = reason;
        }
    }
}
