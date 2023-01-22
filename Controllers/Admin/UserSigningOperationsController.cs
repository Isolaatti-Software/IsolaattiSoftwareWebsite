using IsolaattiSoftwareWebsite.Model;
using IsolaattiSoftwareWebsite.Pages.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers.Admin;

[Route("/Admin/Signing")]
public class UserSigningOperationsController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserSigningOperationsController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    [Route("SignOut")]
    [Authorize]
    public async Task<IActionResult> SignOutUser()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPage("/Admin/Index");
    }
}