using MidtownRestaurantSystem;
using MidtownRestaurantSystem.Email;
using MidtownRestaurantSystem.Interfaces.IServices;
using Moq;

namespace MidtownRestaurant.NUnitTests
{
    public class EmailServiceTests
    {
        private readonly IEmailService _systemUnderTest;
        private readonly Mock<IEmailClient> _emailClient = new Mock<IEmailClient>();

        public EmailServiceTests()
        {
            _systemUnderTest = new EmailService(_emailClient.Object);
        }

        [Test]
        public void CombineEmail_EmailAndOrderIDAndAttachmentLocation_CorrectDataPassedToSendEmailMethod()
        {
            // Arange
            string email = "game@game.com";
            int orderID = 1;
            string subject = "Invoice from Midtown Restaurant";
            string expectedEmailBody = "Dear customer,\r\n\r\n\r\nPlease find attached an invoice No. 1\r\n\r\n\r\nThank you for using our services.\r\n\r\n\r\nYours sincerely,\r\nMidtown Restaurant.";
            string attachmentLocation = "C:\\cheques";

            // Act
            _systemUnderTest.CombineEmail(email, orderID, attachmentLocation);

            //Assert
            _emailClient.Verify(x => x.SendMail(
                It.Is<String>(str => str == email),
                It.Is<String>(str => str == subject),
                It.Is<String>(str => str == expectedEmailBody),
                It.Is<String>(str => str == attachmentLocation)));
        }
    }
}