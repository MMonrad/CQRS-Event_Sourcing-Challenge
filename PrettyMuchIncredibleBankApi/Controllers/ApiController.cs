using Microsoft.AspNetCore.Mvc;

namespace PMI.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces(typeof(string))]
    public Task<ActionResult> Get()
    {
        return Task.FromResult<ActionResult>(Ok());
    }
    
}