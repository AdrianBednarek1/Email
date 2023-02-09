using Email.Entity;

namespace Email.Models.MailModels
{
    public class MailModel
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public string DateTime_ { get; set; }
        public string Sender { get; set; }
        public bool Seen { get; set; }
        public List<string> Receivers { get; set; } = new List<string>();
        public MailModel(Mail mail)
        {
            Id = mail.Id;
            Subject = mail.Subject;
            Message = mail.Message;
            EmailCategory = mail.EmailCategory;
            DateTime_= mail.DateTime_;
            Sender = mail.Sender;
            Receivers.AddRange(mail.Receivers);
            Seen = mail.Seen;
        }
        public MailModel() { }
    }
}
