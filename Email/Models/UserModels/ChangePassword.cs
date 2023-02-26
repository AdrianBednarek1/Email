using System.ComponentModel.DataAnnotations;

namespace Email.Models.UserModels
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Confirmation of Old Password is required.")]
        [Compare("OldPassword", ErrorMessage = "Old password is incorrect!")]
        public string ConfirmOldPassword { get; set; }
        [MinLength(6)]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password must match.")] 
        public string ConfirmNewPassword { get; set;}
        public ChangePassword(string email, string currentPassword)
        {
            Email= email;
            OldPassword= currentPassword;
        }
        public ChangePassword() { } 
    }
}
