using Email.Entity;
using System.Configuration;

namespace Email.Models.MailModels
{
    public class MailFilterModel
    {
        public string EmailAddress { get; set; }
        public string? Filter { get; set; }
        public EmailCategories? EmailCategories_ { get; set; }
        public EmailTypes? EmailType { get; set; }
        public MailFilterModel(){}
        public MailFilterModel(string value, string email)
        {
            EmailAddress = email;
            if(value is null) return;
            string[] array = value.Split(':').ToArray();
            SetValue(array);
        }
        private void SetValue(string[] array)
        {
            bool setEmailCategory = array[0].Contains("category");
            bool setEmailType = array[0].Contains("type");

            if (setEmailCategory) EmailCategories_ = (EmailCategories)Enum.Parse(typeof(EmailCategories),array[1]);
            else if (setEmailType) EmailType = (EmailTypes)Enum.Parse(typeof(EmailTypes), array[1]);
            else Filter = array[0];
        }
    }
    
}
