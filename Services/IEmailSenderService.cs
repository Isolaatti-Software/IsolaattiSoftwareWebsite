namespace IsolaattiSoftwareWebsite.Services;

public interface IEmailSenderService
{
    Task SendDeleteMyInfoCode(string emailAddress, string name, string id,  string code);
}