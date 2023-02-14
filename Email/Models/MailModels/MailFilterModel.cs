using Email.Entity;

namespace Email.Models.MailModels
{
    public class MailFilterModel
    {
        public string EmailAddress { get; set; }
        public string Filter { get; set; } = "";
        public EmailCategories EmailCategories_ { get; set; }
        public EmailTypes EmailType { get; set; }
        public MailFilterModel(){}
        public MailFilterModel
            (string email, string filter, EmailCategories emailCategories_, EmailTypes emailType)
        {
            EmailAddress = email;
            Filter = filter;
            EmailCategories_ = emailCategories_;
            EmailType = emailType;
        }
    }
    
}
