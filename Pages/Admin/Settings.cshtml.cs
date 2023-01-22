using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages.Admin;

[Authorize(Roles="admin")]
public class Settings : PageModel
{
    public void OnGet()
    {
        
    }
}