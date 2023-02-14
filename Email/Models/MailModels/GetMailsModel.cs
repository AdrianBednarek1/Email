using Email.Entity;
using System.Configuration;

namespace Email.Models.MailModels
{
    public class GetMailsModel
    {
        public string EmailAddress { get; set; }
        private string? filter;
        public string Filter {
            get { return filter ?? ""; } 
            set { filter = value; }
        }
        private EmailCategories? emailCategories_;
        public EmailCategories EmailCategories_ {
            get { return emailCategories_ ?? EmailCategories.Primary; } 
            set { emailCategories_ = value; }
        }
        private EmailTypes? emailType;
        public EmailTypes EmailType {
            get { return emailType ?? EmailTypes.Received; }
            set { emailType = value; }
        }
        public GetMailsModel(){}
        public GetMailsModel(string value, string email)
        {
            EmailAddress = email;
            bool valueIsNull = value == null;
            if(valueIsNull) return;
            string[] array = value.Split(':').ToArray();
            SetValue(array);
        }
        public GetMailsModel
            (string email, string filter, EmailCategories emailCategories_, EmailTypes emailType)
        {
            EmailAddress = email;
            Filter = filter;
            EmailCategories_ = emailCategories_;
            EmailType = emailType;
        }
        private void SetValue(string[] array)
        {
            bool setEmailCategory = array[0].Contains("category");
            bool setEmailType = array[0].Contains("type");
            bool setFilter = array[0].Contains("search");

            if (setEmailCategory) EmailCategories_ = (EmailCategories)Enum.Parse(EmailCategories_.GetType(), array[1]);
            else if (setEmailType && !setFilter) EmailType = (EmailTypes)Enum.Parse(EmailType.GetType(), array[1]);
            else Filter = array[1];
        }
    }
    
}
