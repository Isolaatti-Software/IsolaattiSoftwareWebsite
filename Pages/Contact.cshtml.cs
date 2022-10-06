using IsolaattiSoftwareWebsite.Enums;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IsolaattiSoftwareWebsite.Pages;

public class Contact : PageModel
{ 
    public int SubjectId { get; set; }
    
    public void OnGet(ContactUsSubject reference)
    {
        SubjectId = (int)reference;
    }
}