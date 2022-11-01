using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

public class Clients : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}