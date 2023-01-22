using IsolaattiSoftwareWebsite.Model;
using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages.Admin;

[Authorize(Roles = "admin")]
public class EmailProfile : PageModel
{
    private readonly IFirstContactFormResponses _firstContactFormResponses;

    public EmailProfile(IFirstContactFormResponses firstContactFormResponses)
    {
        _firstContactFormResponses = firstContactFormResponses;
    }
    
    public IEnumerable<ContactUs> ContactUsEnumerable;

    public async Task<IActionResult> OnGet(string email)
    {
        ViewData["email"] = email;
        ContactUsEnumerable = await _firstContactFormResponses.GetFirstContactsByEmailAddress(email);
        return Page();
    }
}