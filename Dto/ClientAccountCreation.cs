using System.ComponentModel.DataAnnotations;

namespace IsolaattiSoftwareWebsite.Dto;

public class ClientAccountCreation
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Address1 { get; set; }
    
    [Required]
    public string Address2 { get; set; }
}