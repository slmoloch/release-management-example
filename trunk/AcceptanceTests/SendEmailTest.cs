using AcceptanceTests.Core;

using NUnit.Framework;

using nDumbster.smtp;

namespace AcceptanceTests
{
    [TestFixture]
    public class SendEmailTest : TestBase
    {
        SimpleSmtpServer smtpServer;

        [SetUp]
        public void Setup()
        {
            smtpServer = SimpleSmtpServer.Start();
        }

        [TearDown]
        public void TearDown()
        {
            smtpServer.Stop();
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
            }

            Assert.AreEqual(1, smtpServer.ReceivedEmailCount, "1 mails sent");
            
            var mail = smtpServer.ReceivedEmail[0];
            Assert.AreEqual("slmoloch@gmail.com", mail.Headers["To"], "Receiver");
            Assert.AreEqual("ben@contoso.com", mail.Headers["From"], "Sender");
            Assert.AreEqual("Hi From Test Application", mail.Headers["Subject"], "Subject");

            Assert.AreEqual("Lama mila ramu\r\n", mail.Body);
        }
    }
}
