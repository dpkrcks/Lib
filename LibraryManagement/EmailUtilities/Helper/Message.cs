using MimeKit;

namespace LibraryManagement.EmailUtilities.Helper
{
    public class Message
    {
        private List<MailboxAddress>? receiver;

        private string? subject;

        private string? content;

        public List<MailboxAddress>? Receiver
        {
           get => receiver;
            set => receiver = value;
        }

        public string? Subject {
            get => subject;
            set=>subject = value;
        }

        public string? Content {
            get => content;
            set=>content = value;
        }

        public Message(IEnumerable<string> receiver, string subject, string content) { 
        
            Receiver = new List<MailboxAddress>();
            Receiver.AddRange(receiver.Select(b => new MailboxAddress(string.Empty, b)));

            Subject = subject;

            Content = content;
        }

    }
}
