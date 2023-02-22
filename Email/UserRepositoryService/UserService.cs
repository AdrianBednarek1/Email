using Email.Authentication;
using Email.Entity;
using Email.MailRepositoryService;
using Email.Models;
using Email.Models.MailModels;
using System.Security.Claims;

namespace Email.UserRepositoryService
{
    public class UserService
    {
        private UserRepository userRepository;
        private MailService mailService;
        public HttpAuthentication authentication = new HttpAuthentication();
        public UserService(UserRepository repository, MailService mailService_)
        {
            userRepository = repository;
            mailService = mailService_;
        }
        public async Task<bool> Create(RegisterModel registerModel)
        {
            bool emailIsCorrect = await ValidateNewEmail(registerModel.EmailAddressNameOnly);
            if (!emailIsCorrect) return false;
            User account = registerModel.GetUserEntity();
            await userRepository.CreateUser(account);
            return true;
        }
        private async Task<bool> ValidateNewEmail(string emailAddress)
        {
            bool emailAddressExists = await userRepository.GetUserByEmail(emailAddress) != null;
            if (emailAddressExists) return false;
            bool hasNotAllowedSymbols= emailAddress.Any(x => !Char.IsLetterOrDigit(x)&&!x.Equals('.')&&!x.Equals('-'));
            bool doesntHaveLetters = !emailAddress.Any(x => Char.IsLetter(x));
            if (hasNotAllowedSymbols || doesntHaveLetters) return false;
            return true;
        }
        public async Task<ClaimsPrincipal> Login(LoginModel loginModel)
        {
            User? acc = await userRepository.GetUserByEmail(loginModel.EmailAddress);
            bool incorrectInput = !acc?.Password.Equals(loginModel.Password) ?? true;
            if (incorrectInput) return null;
            return authentication.GetLoginClaim(acc);
        }
        public async Task<LoggedModel> GetUserByEmail(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            bool userDoesntExists = user == null;
            LoggedModel? loggedInModel = userDoesntExists ? null : new LoggedModel(user);
            return loggedInModel;
        }
        public async Task<bool> SendEmail(SendMailModel modelEmail)
        {
            bool mailsAreValid = await CheckEmailValidations(modelEmail.ReceiversToList());
            if (!mailsAreValid) return false;
            List<string> destinationEmails = new List<string>(modelEmail.ReceiversToList()) { modelEmail.Sender };
            List<User> destinations = new List<User>();
            foreach (var email in destinationEmails) destinations.Add(await userRepository.GetUserByEmail(email));        
            bool mailIsSent = await mailService.SendMails(modelEmail, destinations);
            return mailIsSent;
        }
        public async Task<bool> CheckEmailValidations(List<string> list)
        {
            foreach (var item in list)
            {
                LoggedModel? user = await GetUserByEmail(item);
                bool mailIsNotValid = user == null;
                if(mailIsNotValid) return false;
            }
            return true;
        }   
    }
}
