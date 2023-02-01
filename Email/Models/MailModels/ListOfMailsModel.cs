using Email.Entity;
using Microsoft.Identity.Client;

namespace Email.Models.MailModels
{
    public class ListOfMailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public string DateTime_ { get; set; }
        public bool Seen { get; set; }
        private const int allowedStringSize = 105;
        public ListOfMailsModel(Mail mail)
        {
            Id= mail.Id;
            Name = mail.Sender.PersonalInfo.FirstName;
            Subject = FixedSize(mail.Subject, allowedStringSize);
            int allowedMessageSize = allowedStringSize - mail.Subject.Count();
            Message = FixedSize(mail.Message, allowedMessageSize);
            EmailCategory = mail.EmailCategory;
            DateTime_ = mail.DateTime_;
            Seen= mail.Seen;
        }

        private string? FixedSize(string? message, int allowedSize)
        {
            int messageSize = message.Count();
            if (messageSize <= allowedSize) return message;
            string fixedSize = new string(message.Take(allowedSize).ToArray()) + "...";
            return fixedSize;
        }

        public ListOfMailsModel(){}
    }
    
}
