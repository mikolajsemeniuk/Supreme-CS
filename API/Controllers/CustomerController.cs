using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new { message = "works" });
    }
}
