using IsolaattiSoftwareWebsite.Model;
using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages;

public class ValidateSecretCode : PageModel
{
    private readonly IDeleteMyInformationService _deleteMyInformationService;
    private readonly IFirstContactFormResponses _firstContactFormResponses;

    public bool Deleted;
    
    public IEnumerable<ContactUs> ContactUsEnumerable;
    
    [BindProperty]
    public string Id { get; set; }
    [BindProperty]
    public string Code { get; set; }

    public ValidateSecretCode(IDeleteMyInformationService deleteMyInformationService, IFirstContactFormResponses firstContactFormResponses)
    {
        _deleteMyInformationService = deleteMyInformationService;
        _firstContactFormResponses = firstContactFormResponses;
    }
    
    public async Task<IActionResult> OnGet(string id, string code)
    {
        var deleteInfoRequest = _deleteMyInformationService.GetDeleteMyInfoRequest(id);

        if (deleteInfoRequest == null)
        {
            return NotFound();
        }

        ContactUsEnumerable = await _firstContactFormResponses.GetFirstContactsByEmailAddress(deleteInfoRequest.Email);

        Id = id;
        Code = code;
        
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteInfo()
    {
        try
        {
            await _deleteMyInformationService.DeleteData(Id, Code);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (RequestCodeNotCorrectException)
        {
            return NotFound();
        }

        Deleted = true;
        return Page();
    }
}