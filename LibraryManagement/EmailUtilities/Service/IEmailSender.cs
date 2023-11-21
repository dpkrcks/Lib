using LibraryManagement.EmailUtilities.Helper;

namespace LibraryManagement.EmailUtilities.Service
{
    public interface IEmailSender
    {
        public void SendEmail(Message message);
    }
}
