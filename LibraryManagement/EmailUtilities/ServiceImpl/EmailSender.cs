using LibraryManagement.EmailUtilities.Helper;
using LibraryManagement.EmailUtilities.Service;
using LibraryManagement.Utilities;
using MailKit.Net.Smtp;
using MimeKit;

namespace LibraryManagement.EmailUtilities.ServiceImpl
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }   



        public void SendEmail(Message message)
        {
            var eMessage = CreateMimeMessage(message);
            SendMesage(eMessage);
        }


        /**
         * Private method to create the message that is to be sent.
         */
        private MimeMessage CreateMimeMessage(Message message) {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.Receiver);
            emailMessage.Subject = message.Subject;

            var textPart = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            emailMessage.Body = textPart;

            return emailMessage;           

        }


        /**
         * Private method to connect smtp configured server and authenticate.
         * Send message using SmtpClient.
         */
        private void SendMesage(MimeMessage message) {

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    client.Send(message);
                }
                catch(Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
                finally {

                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }




    }
}
