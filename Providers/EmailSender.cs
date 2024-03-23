using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FIAB.Providers;

public class EmailSender : IEmailSender
{
	private IConfiguration Configuration { get; set; }

	public EmailSender(IConfiguration config)
	{
		Configuration = config;
	}

	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		var host = FIAB.Models.Utilities.Env("SmtpServer", true, Configuration);
		int.TryParse(FIAB.Models.Utilities.Env("SmtpPort", true, Configuration), out int port);
		var u = FIAB.Models.Utilities.Env("SmtpUser", true, Configuration);
		var p = FIAB.Models.Utilities.Env("SmptPassword", true, Configuration);
		
		using var client = new SmtpClient();
		client.Credentials = new System.Net.NetworkCredential(u, p);
		client.Host = host;
		client.Port = port;
		client.EnableSsl = true;

		var mail = new MailMessage();
		mail.From = new MailAddress(u);
		mail.To.Add(new MailAddress(email));
		mail.Subject = subject;
		mail.Body = htmlMessage;
		mail.IsBodyHtml = true;

		// await client.SendMailAsync(mail);
		client.Send(mail);
		await Task.CompletedTask;
	}
}
