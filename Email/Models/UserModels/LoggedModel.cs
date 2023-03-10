using Email.Entity;
using Email.Models.MailModels;

namespace Email.Models
{
    public class LoggedModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress + $"@gmail.com"; }
            set { emailAddress = value; }
        }
        public string Password { get; set; }
        public List<ListMailModel> Mails { get; set; } = new List<ListMailModel>();
        public LoggedModel(User user)
        {
            Id = user.Id;
            FirstName = user.PersonalInfo.FirstName;
            LastName = user.PersonalInfo.LastName;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
            BirthDate = user.PersonalInfo.BirthDate;
            PhoneNumber = user.PersonalInfo.PhoneNumber;
            user.Mails.ForEach(item => Mails.Add(new ListMailModel(item)));
        }
        public LoggedModel() { }
    }
}
