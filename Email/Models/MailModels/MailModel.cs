using Email.Entity;

namespace Email.Models.MailModels
{
    public class MailModel
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public DateTime DateTime_ { get; set; }
        public string Sender { get; set; }
        public List<string> Receivers { get; set; } = new List<string>();
        public MailModel(Mail mail)

        {
            Id = mail.Id;
            Subject = mail.Subject;
            Message = mail.Message;
            EmailCategory = mail.EmailCategory;
            Sender = mail.Sender.EmailAddress;
            Receivers.AddRange(mail.Receivers.Select(item => item.EmailAddress));
        }
        public MailModel() { }
    }
}
