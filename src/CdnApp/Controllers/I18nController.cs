#nullable disable

using Microsoft.AspNetCore.Mvc;

namespace CdnApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class I18nController : ControllerBase
{
    [HttpGet("{language}")]
    public async Task<ActionResult<dynamic>> Get(string language)
    {
        try
        {
            var text = await System.IO.File.ReadAllTextAsync($"./wwwroot/i18n/{language}.json");
            var rs = System.Text.Json.JsonSerializer.Deserialize<dynamic>(text);
            return rs;
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
        }
    }
}