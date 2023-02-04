using Email.Entity;
using Email.Models.MailModels;

namespace Email.Models
{
    public class UserModel
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
        public List<MailListModel> ReceivedMails { get; set; } = new List<MailListModel>();
        public List<MailListModel> SentMails { get; set; } = new List<MailListModel>();
        public UserModel(User user)
        {
            Id = user.Id;
            FirstName = user.PersonalInfo.FirstName;
            LastName = user.PersonalInfo.LastName;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
            List<Mail> receivedMails = new List<Mail>(user.Mails.Where(x=>!x.Sender.Equals(user.EmailAddress)));
            List<Mail> sentMails = new List<Mail>(user.Mails.Where(x => x.Sender.Equals(user.EmailAddress)));
            receivedMails.ForEach(item => ReceivedMails.Add(new MailListModel(item)));
            sentMails.ForEach(item => SentMails.Add(new MailListModel(item)));
        }
        public UserModel() { }
    }
}
