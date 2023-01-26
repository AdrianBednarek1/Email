using Email.Authentication;
using Email.Entity;
using Email.Models;
using Email.Models.MailModels;
using System.Security.Claims;

namespace Email.UserRepositoryService
{
    public class UserService
    {
        private UserRepository userRepository;
        public HttpAuthentication authentication = new HttpAuthentication();
        public UserService(UserRepository repository)
        {
            userRepository = repository;
        }
        public async Task<bool> Create(RegisterModel registerModel)
        {
            bool emailAddressExists = await userRepository.GetAccountByEmailAdress(registerModel.EmailAddress) != null;
            if (emailAddressExists) return false;
            User account = registerModel.GetUserEntity();
            await userRepository.CreateAccount(account);
            return true;
        }
        public async Task<ClaimsPrincipal> Login(LoginModel loginModel)
        {
            User? acc = await userRepository.GetAccountByEmailAdress(loginModel.EmailAddress);
            bool incorrectInput = !acc?.Password.Equals(loginModel.Password) ?? true;
            if (incorrectInput) return null;
            return authentication.GetLoginClaim(acc);
        }
        public async Task<LoggedInModel> GetAccByEmail(string? email)
        {
            User user = await userRepository.GetAccountByEmailAdress(email);
            LoggedInModel loggedInModel = user == null ? null : new LoggedInModel(user);
            return loggedInModel;
        }
        public async Task SendEmail(MailModel modelEmail)
        {
            Mail email = await ModelToEntity(modelEmail);
            List<User> accounts = new List<User>();
            foreach (var item in modelEmail.Receivers)
            {
                User account = await userRepository.GetAccountByEmailAdress(item);
                email.Receivers.Add(account);
            }
            await userRepository.SendEmail(email);
        }
        private async Task<Mail> ModelToEntity(MailModel modelEmail)
        {
            User sender = await userRepository.GetAccountByEmailAdress(modelEmail.Sender);
            List<User> receivers = new List<User>();
            foreach (var item in modelEmail.Receivers)
            {
                User receiver = await userRepository.GetAccountByEmailAdress(item);
                receivers.Add(receiver);
            }
            Mail email = new Mail()
            {
                DateTime_ = modelEmail.DateTime_,
                EmailCategory = modelEmail.EmailCategory,
                Message = modelEmail.Message,
                Sender = sender,
                Subject = modelEmail.Subject,
                Receivers = receivers
            };
            return email;
        }
    }
}
