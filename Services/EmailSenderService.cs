using SendGrid;
using SendGrid.Helpers.Mail;

namespace IsolaattiSoftwareWebsite.Services;

public class EmailSenderService : IEmailSenderService
{
    public static string ComposeHtml(string name, string link)
    {
        return $@"
<html>
<body>
<h1>¡Buen día, {name}!</h1>
<h2>Procedamos con la eliminación de sus datos</h2>
<p>Solo tiene que hacer clic en el enlace, que lo llevará directamente al sitio web de 
Isolaatti Software, y desde ahí podrá ver lo que nos ha enviado y proceder a eliminarlo.</p>
<a href=""{link}"">{link}</a>
</body>
</html>
";
    }

    private readonly ISendGridClient _sendGrid;
    
    public EmailSenderService(ISendGridClient sendGridClient)
    {
        _sendGrid = sendGridClient;
    }
    
    public async Task SendDeleteMyInfoCode(string emailAddress, string name, string id,  string code)
    {
        var from = new EmailAddress("software@isolaatti.com", "Isolaatti Software");
        var to = new EmailAddress(emailAddress, name);
        var subject = "Eliminación de sus datos del nuestro sitio web";
        var url = $"https://isolaattisoftware.com.mx/eliminar_datos?id={id}&code={code}";
        var plainTextMessage =
            $"Buen día, {name}, usted solicitó eliminar los datos que usted envió a Isolaatti Software. Para proceder con la eliminación, siga el siguiente enlace: {url}";
        var email = MailHelper.CreateSingleEmail(from, to, subject, plainTextMessage, ComposeHtml(name, url));
        await _sendGrid.SendEmailAsync(email);
    }
}