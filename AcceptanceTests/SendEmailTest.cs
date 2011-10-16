using System.Threading;
using System.Web.Mail;

using AcceptanceTests.Core;

using NUnit.Framework;

using nDumbster.smtp;

namespace AcceptanceTests
{
    [TestFixture]
    public class SendEmailTest : TestBase

    {
        private SimpleSmtpServer smtpServer = null;

        [SetUp]
        public void Initialize()
        {
            smtpServer = SimpleSmtpServer.Start();
        }

        [TearDown]
        public void Cleanup()
        {
            if (smtpServer != null) smtpServer.Stop();
        }

        [Test]
        public void SendEmail()
        {
            // Use SmtpMail to send an SMTP message to localhost (port 25)
            // (in the real world you would be testing your own code that talks to an SMTP server).
            SmtpMail.SmtpServer = "localhost";
            SmtpMail.Send("somebody@foo.com", "everybody@bar.com", "This is the subject", "This is the body.");
        }

        [Test]
        public void LoginAndSendEmail()
        {
            using (var start = GetStart())
            {
                start
                    .GoToHomePage()
                    .ShowEmailPopup()
                    .FillBody("Lama mila ramu")
                    .ClickOnSend();

                    Thread.Sleep(5000);    
            }

            
            // Check that the mail has been received.
            Assert.That(smtpServer.ReceivedEmail[0].Body, Is.EqualTo("This is the body."));
        }
    }
}
