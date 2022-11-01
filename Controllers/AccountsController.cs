using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers;

public class AccountsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}