using Company.G01.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G01.PL.Helpers
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("nassermansy2024@gmail.com", "cwyhurkcupuukezj");

			var mail = new MailMessage();
			mail.From = mail.From = new MailAddress("nassermansy2024@gmail.com");
			mail.Subject = email.Subject;
			mail.Body = email.Body;
			mail.To.Add(email.To);





			//client.Send("nassermansy2024@gmail.com", email.To, email.Subject, email.Body);

			client.Send(mail);

		}
	}
}
