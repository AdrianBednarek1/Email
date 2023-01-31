using Email.Entity;
using Email.Models.MailModels;

namespace Email.Models
{
    public class LoggedInModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress + $"@gmail.com"; }
            set { emailAddress = value; }
        }
        public string Password { get; set; }
        public List<ListOfMailsModel> ReceivedMails { get; set; } = new List<ListOfMailsModel>();
        public List<ListOfMailsModel> SentMails { get; set; } = new List<ListOfMailsModel>();
        public LoggedInModel(User user)
        {
            Id = user.Id;
            FirstName = user.PersonalInfo.FirstName;
            LastName = user.PersonalInfo.LastName;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
            List<Mail> receivedMails = new List<Mail>(user.ReceivedMails);
            List<Mail> sentMails = new List<Mail>(user.SentMails);
            receivedMails.ForEach(item => ReceivedMails.Add(new ListOfMailsModel(item)));
            sentMails.ForEach(item => SentMails.Add(new ListOfMailsModel(item)));
        }
        public LoggedInModel() { }
    }
}
