using Microsoft.AspNetCore.Mvc;

namespace ProblemDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProblemDetailsController : ControllerBase
    {
        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest() => BadRequest();

        [HttpGet("NotImplementedException")]
        public IActionResult GetNotImplementedException() => throw new NotImplementedException();

        [HttpGet("NotFound")]
        public IActionResult GetNotFound() => NotFound();

        [HttpGet("404")]
        public void Get404() => this.Response.StatusCode = 404;

        [HttpGet("CustomBadRequest")]
        public IActionResult GetCustomBadRequest() => BadRequest(new
        {
            A = 1,
            B = "ff"
        });
    }
}
