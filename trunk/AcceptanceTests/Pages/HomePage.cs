using AcceptanceTests.Core;

using Tests.SmokeTest.PageObjects.Controls;

namespace AcceptanceTests.Pages
{
        public class HomePage : PageBase
        {
            public HomePage() : base("Default.aspx") { }

            private Link sendMailLink = null;
            public Link SendMailLink
            {
                get
                {
                    if (sendMailLink == null)
                    {
                        sendMailLink = new Link(Selenium, @"//a[@onclick=""OnSendEmailClick(this, '1')""]");
                    }
                    return sendMailLink;
                }
            }
            
            private TextField bodyText = null;

            public TextField BodyText
            {
                get
                {
                    if (bodyText == null)
                    {
                        bodyText = new TextField(Selenium, @"//input[@id='txtMessage']");
                    }
                    return bodyText;
                }
            }

            private Link sendButton = null;

            public Link SendButton
            {
                get
                {
                    if (sendButton == null)
                    {
                        sendButton = new Link(Selenium, @"//div[@id='MainContent_popup_callbackPanel_ASPxButton1_CD']/span");
                    }
                    return sendButton;
                }
            }
        }
    }
