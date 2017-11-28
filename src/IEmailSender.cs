using System.Threading.Tasks;

namespace ctorx.Core.SmtpMail
{
	public interface IEmailSender
	{
		/// <summary>
		/// Sends an Email Message
		/// </summary>
		Task SendAsync(EmailMessage emailMessage);
	}
}