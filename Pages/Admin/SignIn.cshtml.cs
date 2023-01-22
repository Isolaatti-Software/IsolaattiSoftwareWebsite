using System.ComponentModel.DataAnnotations;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages.Admin;

public class SignIn : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignIn(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public bool ValidationErrors;
    
    public void OnGet()
    {
        
    }

    [BindProperty]
    [Required]
    public string UserName { get; set; }
    
    [BindProperty]
    [Required]
    public string Password { get; set; }
    
    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            await _signInManager.PasswordSignInAsync(UserName, Password, true, false);
            
            return RedirectToPage("/Admin/Index");
        }
        
        ValidationErrors = true;
        return Page();

    }
}