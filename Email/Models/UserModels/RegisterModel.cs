using Email.Entity;
using Email.Models.MailModels;

namespace Email.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress + $"@gmail.com"; }
            set { emailAddress = value; }
        }
        public string Password { get; set; }
        public RegisterModel() { }
        public User GetUserEntity()
        {
            Person person = new Person()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate= BirthDate,
                PhoneNumber =PhoneNumber
            };
            User account = new User()
            {
                EmailAddress = EmailAddress,
                Password = Password,
                PersonalInfo = person,
                ReceivedMails = new List<Mail>(),
                SentMails = new List<Mail>()
            };
            return account;
        }
    }
}
