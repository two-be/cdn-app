using Microsoft.AspNetCore.Mvc;

namespace CdnApp.Abstracts;

public abstract class BaseController : ControllerBase
{
    protected ActionResult InternalServerError(Exception ex)
    {
        return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
    }
}