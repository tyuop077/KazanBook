using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public FailedResponse Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            if (exception is BAL.DALError)
            {
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
        public bool success = false;
        public string reason;
        public FailedResponse(string reason)
        {
            this.reason = reason;
        }
    }
}
