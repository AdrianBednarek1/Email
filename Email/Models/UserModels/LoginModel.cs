using System.ComponentModel.DataAnnotations;

namespace Email.Models
{
    public class LoginModel
    {
        private string emailAddress;
        [Required]
        public string EmailAddress
        {
            get { return emailAddress + $"@gmail.com"; }
            set { emailAddress = value; }
        }
        [Required]
        public string Password { get; set; }
    }
}
