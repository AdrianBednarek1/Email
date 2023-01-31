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
        public ListOfMailsModel(Mail mail)
        {
            Id= mail.Id;
            Name = mail.Sender.PersonalInfo.FirstName;
            Subject = mail.Subject;
            Message = mail.Message;
            EmailCategory = mail.EmailCategory;
            DateTime_ = mail.DateTime_;
            Seen= mail.Seen;
        }
        public ListOfMailsModel(){}
    }
    
}
