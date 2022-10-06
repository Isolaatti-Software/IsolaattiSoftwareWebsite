using IsolaattiSoftwareWebsite.Model;
using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.Mvc;

namespace IsolaattiSoftwareWebsite.Controllers;

[ApiController]
[Route("/api/contact")]
public class ContactFormController : ControllerBase
{
    private readonly IFirstContactFormResponses _firstContactFormResponses;

    public ContactFormController(IFirstContactFormResponses firstContactFormResponses)
    {
        _firstContactFormResponses = firstContactFormResponses;
    }
    
    [HttpPost]
    [Route("submit")]
    public IActionResult Submit(ContactUsForm contactUsForm)
    {
        var contactUs = new ContactUs(contactUsForm);
        if (!_firstContactFormResponses.ValidateForm(contactUs))
        {
            return Problem("Validation errors");
        }
        
        _firstContactFormResponses.SaveForm(contactUs);
        
        return Ok();
    }
}