using Email.Entity;
using Email.Models.MailModels;

namespace Email.Models
{
    public class LoggedModel
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
        public List<ListOfMailsModel> Mails { get; set; } = new List<ListOfMailsModel>();
        public LoggedModel(User user)
        {
            Id = user.Id;
            FirstName = user.PersonalInfo.FirstName;
            LastName = user.PersonalInfo.LastName;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
            user.Mails.ForEach(item => Mails.Add(new ListOfMailsModel(item)));
        }
        public LoggedModel() { }
    }
}
