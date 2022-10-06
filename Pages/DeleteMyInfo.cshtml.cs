using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages;

public class DeleteMyInfo : PageModel
{
    public bool FormInvalid;
    public bool Success;
    
    [BindProperty]
    public string EmailAddress { get; set; }

    private readonly IDeleteMyInformationService _deleteMyInformationService;

    public DeleteMyInfo(IDeleteMyInformationService deleteMyInformationService)
    {
        _deleteMyInformationService = deleteMyInformationService;
    }
    
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        try
        {
            await _deleteMyInformationService.AddRequest(EmailAddress);
            Success = true;
        }
        catch (FormatException)
        {
            FormInvalid = true;
        }
        catch(NoRequestsFoundException){}

        return Page();
    }
}