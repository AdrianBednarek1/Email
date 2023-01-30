using Email.Entity;
using Email.Models;
using Email.Models.MailModels;
using Email.UserRepositoryService;

namespace Email.MailRepositoryService
{
    public class MailService
    {
        private readonly MailRepository mailRepository;
        public MailService(MailRepository mailRepository_) 
        {
            mailRepository = mailRepository_;
        }
        public async Task<bool> CreateMail(Mail mail)
        {
            bool mailIsnull = mail == null;
            if (mailIsnull) return false;
            await mailRepository.CreateMail(mail);
            return true;
        }
        
    }
}
