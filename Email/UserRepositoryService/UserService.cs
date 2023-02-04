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
            bool emailAddressExists = await userRepository.GetUserByEmail(registerModel.EmailAddress) != null;
            if (emailAddressExists) return false;
            User account = registerModel.GetUserEntity();
            await userRepository.CreateUser(account);
            return true;
        }
        public async Task<ClaimsPrincipal> Login(LoginModel loginModel)
        {
            User? acc = await userRepository.GetUserByEmail(loginModel.EmailAddress);
            bool incorrectInput = !acc?.Password.Equals(loginModel.Password) ?? true;
            if (incorrectInput) return null;
            return authentication.GetLoginClaim(acc);
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            bool userDoesntExists = user == null;
            UserModel loggedInModel = userDoesntExists ? null : new UserModel(user);
            return loggedInModel;
        }
        public async Task<bool> DeleteMailFromUser(DeleteMailModel deleteMail)
        {
            User user = await userRepository.GetUserByEmail(deleteMail.Username);
            Mail mail = await mailService.GetMailById(deleteMail.MailId);
            await userRepository.DeleteMailFromUser(user, mail);
            return true;
        }
        public async Task<bool> SendEmail(SendMailModel modelEmail)
        {
            bool mailsAreValid = await CheckEmailValidations(modelEmail.GetListOfReceivers());
            if (!mailsAreValid) return false;
            Mail mail = await ModelToEntity(modelEmail);
            bool mailSent = await mailService.CreateMail(mail);
            if (!mailSent) return false;
            return true;
        }
        public async Task<bool> CheckEmailValidations(List<string> list)
        {
            foreach (var item in list)
            {
                UserModel? user = await GetUserByEmail(item);
                bool mailIsNotValid = user == null;
                if(mailIsNotValid) return false;
            }
            return true;
        }   
        public async Task<User> ModelToEntity(UserModel model)
        {
            bool modelIsEmpty = model == null;
            if (modelIsEmpty) return null;
            User user = await userRepository.GetUserByEmail(model.EmailAddress);
            return user;
        }
        private async Task<List<Mail>> ModelToEntity(SendMailModel mailModel)
        {
            List<User> destinations = new List<User>();
            foreach (var item in mailModel.GetListOfReceivers())
            {
                User receiver = await userRepository.GetUserByEmail(item);
                destinations.Add(receiver);
            }
            User sender = await userRepository.GetUserByEmail(mailModel.Sender);
            destinations.Add(sender);
            List<Mail> mails = new List<Mail>();
            foreach (var destination in destinations)
            {
                Mail mail = new Mail()
                {
                    DateTime_ = mailModel.DateTime_,
                    EmailCategory = EmailCategories.Primary,
                    Message = mailModel.Message,
                    Destination = destination,
                    Subject = mailModel.Subject,
                    Sender= mailModel.Sender,
                    Receivers = mailModel.GetListOfReceivers()
                };
                mails.Add(mail);
            }
            return mails;
        }
    }
}
