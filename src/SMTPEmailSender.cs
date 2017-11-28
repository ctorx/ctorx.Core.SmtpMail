using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ctorx.Core.SmtpMail
{
	public class SmtpEmailSender : IEmailSender
	{
	    readonly SmtpSettings SmtpSettings;

	    /// <summary>
	    /// ctor the Mighty
	    /// </summary>	    
		public SmtpEmailSender(IOptions<SmtpSettings> smtpSettingsProvider)
		{
		    this.SmtpSettings = smtpSettingsProvider.Value;
		}

		/// <summary>
		/// Sends the provided email message
		/// </summary>
		public async Task SendAsync(EmailMessage emailMessage)
		{
			if (emailMessage == null)
			{
				throw new ArgumentNullException(nameof(emailMessage));
			}

            // Fail Gracefully if SMTP Settings are not set
		    if(string.IsNullOrWhiteSpace(this.SmtpSettings.SmtpHost) || this.SmtpSettings.SmtpPort == 0)
		    {
		        return;
		    }

			var message = new MailMessage();

			// Set Sender
			message.From = new MailAddress(emailMessage.SenderAddress, emailMessage.SenderDisplayName);

			// Set Recipients
		    foreach (var recipient in emailMessage.ToRecipients)
		    {
                message.To.Add(recipient);
		    }

            // Set CC
		    foreach (var recipient in emailMessage.CcRecipients)
		    {
		        message.CC.Add(recipient);
		    }

            // Set BCC
		    foreach (var recipient in emailMessage.BccRecipients)
		    {
		        message.Bcc.Add(recipient);
		    }

            // Set ReplyTo
		    foreach (var recipient in emailMessage.ReplyToRecipients)
		    {
		        message.ReplyToList.Add(recipient);
		    }
            
			// Set Content
			message.IsBodyHtml = emailMessage.FormatAsHtml;
			message.Body = await emailMessage.BuildMessageBody();
			message.Subject = emailMessage.Subject;

			// Set Attachments
			if(emailMessage.Attachments.Count > 0)
			{
                foreach(var attachment in emailMessage.Attachments)
                { 
					var emailAttachment = new Attachment(attachment.GetContentStream(), attachment.Name);
					message.Attachments.Add(emailAttachment);
				};
			}

			// Set SMTP Settings
			var smtpClient = new SmtpClient(this.SmtpSettings.SmtpHost, this.SmtpSettings.SmtpPort);
			smtpClient.EnableSsl = this.SmtpSettings.SmtpEnableSsl;

			// Set Credentials
			if (!string.IsNullOrWhiteSpace(this.SmtpSettings.SmtpUsername) || !string.IsNullOrWhiteSpace(this.SmtpSettings.SmtpPassword))
			{
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(this.SmtpSettings.SmtpUsername, this.SmtpSettings.SmtpPassword);
			}

			// Send Message
			await smtpClient.SendMailAsync(message);
		}
	}
}