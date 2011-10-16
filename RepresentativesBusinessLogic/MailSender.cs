using System.Net.Mail;

namespace RepresentativesBusinessLogic
{
    public class MailSender
    {
        public void SendMessage(string toAddress, string subject, string body)
        {
            var to = new MailAddress(toAddress);
            var from = new MailAddress("ben@contoso.com");
            var message = new MailMessage(from, to);
            
            message.Subject = subject;
            message.Body = body;
         
            // Use the application or machine configuration to get the 
            // host, port, and credentials.

            using (var client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}
