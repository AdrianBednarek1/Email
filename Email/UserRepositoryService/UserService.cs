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
        public async Task<LoggedModel> GetUserByEmail(string email)
        {
            User user = await userRepository.GetUserByEmail(email);
            bool userDoesntExists = user == null;
            LoggedModel loggedInModel = userDoesntExists ? null : new LoggedModel(user);
            return loggedInModel;
        }
        public async Task<bool> SendEmail(SendMailModel modelEmail)
        {
            bool mailsAreValid = await CheckEmailValidations(modelEmail.GetListOfReceivers());
            if (!mailsAreValid) return false;
            List<Mail> mails = await ModelToEntity(modelEmail);
            bool mailSent = await mailService.CreateMails(mails);
            if (!mailSent) return false;
            return true;
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
        public async Task<User> ModelToEntity(LoggedModel model)
        {
            bool modelIsEmpty = model == null;
            if (modelIsEmpty) return null;
            User user = await userRepository.GetUserByEmail(model.EmailAddress);
            return user;
        }
        private async Task<List<Mail>> ModelToEntity(SendMailModel mailModel)
        {
            List<Mail> mails = new List<Mail>();
            foreach (var item in mailModel.GetListOfReceivers())
            {
                User user = await userRepository.GetUserByEmail(item);
                Mail email = new Mail()
                {
                    DateTime_ = mailModel.DateTime_,
                    EmailCategory = mailModel.EmailCategory,
                    Message = mailModel.Message,
                    Sender = mailModel.Sender,
                    Subject = mailModel.Subject,
                    Receivers = mailModel.GetListOfReceivers(),
                    Destination = user
                };
                mails.Add(email);
            }
            User sender = await userRepository.GetUserByEmail(mailModel.Sender);
            Mail mailToSender = new Mail()
            {
                DateTime_ = mailModel.DateTime_,
                EmailCategory = mailModel.EmailCategory,
                Message = mailModel.Message,
                Sender = mailModel.Sender,
                Subject = mailModel.Subject,
                Receivers = mailModel.GetListOfReceivers(),
                Destination = sender
            };
            mails.Add(mailToSender);

            return mails;
        }
    }
}
