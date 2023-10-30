using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    [HttpGet]
    public IActionResult List()
    {
        return Ok("Listing todos");
    }
}
