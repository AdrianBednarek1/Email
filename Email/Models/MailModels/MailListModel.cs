using Email.Entity;
using Microsoft.Identity.Client;

namespace Email.Models.MailModels
{
    public class MailListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public string DateTime_ { get; set; }
        public bool Seen { get; set; }
<<<<<<< HEAD:Email/Models/MailModels/MailListModel.cs
        private const int allowedStringSize = 105;
        public MailListModel(Mail mail)
        {
            Id= mail.Id;
            Name = mail.Destination.PersonalInfo.FirstName;
            Subject = FixedSize(mail.Subject, allowedStringSize);
            int allowedMessageSize = allowedStringSize - mail.Subject.Count();
            Message = FixedSize(mail.Message, allowedMessageSize);
=======
        public ListOfMailsModel(Mail mail)
        {
            Id= mail.Id;
            Name = mail.Sender.PersonalInfo.FirstName;
            Subject = mail.Subject;
            Message = mail.Message;
>>>>>>> parent of bd8c286 (emali validation on send):Email/Models/MailModels/ListOfMailsModel.cs
            EmailCategory = mail.EmailCategory;
            DateTime_ = mail.DateTime_;
            Seen= mail.Seen;
        }
<<<<<<< HEAD:Email/Models/MailModels/MailListModel.cs

        private string? FixedSize(string? message, int allowedSize)
        {
            int messageSize = message.Count();
            if (messageSize <= allowedSize) return message;
            string fixedSize = new string(message.Take(allowedSize).ToArray()) + "...";
            return fixedSize;
        }

        public MailListModel(){}
=======
        public ListOfMailsModel(){}
>>>>>>> parent of bd8c286 (emali validation on send):Email/Models/MailModels/ListOfMailsModel.cs
    }
    
}
