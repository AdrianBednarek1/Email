using Email.Entity;
using Email.Models.MailModels;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Email.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        private int age;
        [Range(10,70,ErrorMessage = "Your age must be between 10 and 70")]
        public int Age
        {
            get { return age; }
            set { age = DateTime.Now.Year - BirthDate.Year; }
        }
		public string? PhoneNumber { get; set; }
        private string emailAddress;
        [MinLength(1)]
		public string EmailAddress
        {
            get { return String.IsNullOrEmpty(emailAddress) ? null : emailAddress + $"@gmail.com"; }
            set { emailAddress = value; }
        }
        [MinLength(6)]
		public string Password { get; set; }
        public RegisterModel() { }
        public User GetUserEntity()
        {
            Person person = new Person()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate= BirthDate.ToString("yyyy/MM/dd"),
                PhoneNumber =PhoneNumber
            };
            User account = new User()
            {
                EmailAddress = EmailAddress,
                Password = Password,
                PersonalInfo = person,
                Mails = new List<Mail>()
            };
            return account;
        }
    }
}
