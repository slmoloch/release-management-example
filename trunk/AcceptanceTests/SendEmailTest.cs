using System.Collections;
using System.Threading;
using System.Web.Mail;

using AcceptanceTests.Core;

using NUnit.Framework;

namespace AcceptanceTests
{
    [TestFixture]
    public class SendEmailTest : TestBase
    {
        private SmtpMock smtpServerMock;

        [SetUp]
        public void SetUp()
        {
            smtpServerMock = new SmtpMock();
            smtpServerMock.Start();
        }
        
        [TearDown]
        public void TearDown()
        {
            smtpServerMock.Stop();
        }

        [Test]
        public void SendEmail()
        {
            SmtpMail.SmtpServer = "localhost";
            SmtpMail.Send("somebody@foo.com", "everybody@bar.com", "This is the subject", "This is the body.");

            Thread.Sleep(10000);


            Assert.AreEqual(1, ((ICollection)smtpServerMock.Sessions).Count);

            var session = smtpServerMock.Sessions[0];
            Assert.IsTrue(session.SessionProtocol.IndexOf("somebody@foo.com") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("everybody@bar.com") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("This is the subject") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("This is the body.") > 0);
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


            Assert.AreEqual(1, ((ICollection)smtpServerMock.Sessions).Count);
            
            var session = smtpServerMock.Sessions[0];
            Assert.IsTrue(session.SessionProtocol.IndexOf("somebody@foo.com") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("everybody@bar.com") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("This is the subject") > 0);
            Assert.IsTrue(session.SessionProtocol.IndexOf("This is the body.") > 0);
        }
    }
}
