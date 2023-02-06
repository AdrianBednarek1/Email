using Email.Entity;

namespace Email.Models.MailModels
{
    public class GetMailsModel
    {
        public string EmailAddress { get; set; }
        public string DontReceiveFrom { get; set; } = "";
        public string Filter { get; set; } = "";
        public EmailCategories EmailCategories_ { get; set; }
        public GetMailsModel(){}
        public GetMailsModel(string email, string dontReceive, string filter, EmailCategories emailCategories_)
        {
            EmailAddress= email;
            DontReceiveFrom= dontReceive;
            Filter= filter;
            EmailCategories_ = emailCategories_;
        }
    }
    
}
