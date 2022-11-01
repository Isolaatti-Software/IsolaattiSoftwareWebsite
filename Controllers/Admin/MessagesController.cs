using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

public class MessagesController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}