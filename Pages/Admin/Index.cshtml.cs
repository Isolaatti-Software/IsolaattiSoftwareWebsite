using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages.Admin;

[Authorize(Roles = "admin")]
public class Index : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}