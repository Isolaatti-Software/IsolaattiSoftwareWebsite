using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

public class BlogController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}