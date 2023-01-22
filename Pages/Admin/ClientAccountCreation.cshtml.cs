using System.ComponentModel.DataAnnotations;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages.Admin;

public class ClientAccountCreation : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ClientAccountCreation(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    [BindProperty]
    [Required]
    public Dto.ClientAccountCreation ClientAccountCreationData { get; set; }

    public ApplicationUser? Client { get; set; }
    
    public async Task<IActionResult> OnGet(string email)
    {
        Client = await _userManager.FindByEmailAsync(email);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        await _userManager.CreateAsync(new ApplicationUser
        {
            Email = ClientAccountCreationData.Email,
            UserName = $"{ClientAccountCreationData.FirstName} {ClientAccountCreationData.LastName}"
        });

        return Page();
    }
}