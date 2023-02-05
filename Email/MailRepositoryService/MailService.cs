using Email.Entity;

namespace Email.MailRepositoryService
{
    public class MailService
    {
        private readonly MailRepository mailRepository;
        public MailService(MailRepository mailRepository_) 
        {
            mailRepository = mailRepository_;
        }
        public async Task<bool> CreateMails(List<Mail> mails)
        {
            foreach (var mail in mails)
            {
                bool mailIsnull = mail == null;
                if (mailIsnull) return false;
                await mailRepository.CreateMail(mail);
            }
            return true;
        }
        
    }
}
