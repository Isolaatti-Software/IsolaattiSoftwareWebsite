using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using IsolaattiSoftwareWebsite.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IsolaattiSoftwareWebsite.Model;

public class ContactUs : ContactUsForm
{

    public ContactUs(ContactUsForm form)
    {
        Name = form.Name.Trim();
        LastName = form.LastName.Trim();
        Email = form.Email.Trim();
        Subject = form.Subject;
        Message = form.Message.Trim();
    }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}

public class ContactUsForm
{
    [Required(ErrorMessage = "Error is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Subject is required")]
    public ContactUsSubject Subject { get; set; } 
    
    [Required(ErrorMessage = "UserMessage is required")]
    public string Message { get; set; }
}