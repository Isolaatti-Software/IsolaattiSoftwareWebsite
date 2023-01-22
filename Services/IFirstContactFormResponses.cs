using IsolaattiSoftwareWebsite.Model;

namespace IsolaattiSoftwareWebsite.Services;

public interface IFirstContactFormResponses
{
    public Task SaveForm(ContactUs contactUsObj);
    public bool ValidateForm(ContactUs contactUsObj);
    Task<IEnumerable<ContactUs>> GetFirstContactsByEmailAddress(string emailAddress);
    Task<IEnumerable<ContactUs>> GetFirstContact(string? lastId, int count);
}