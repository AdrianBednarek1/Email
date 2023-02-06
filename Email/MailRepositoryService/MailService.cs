using Email.Entity;
using Email.Models.MailModels;
using Microsoft.Identity.Client;

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
        public async Task<IQueryable<ListMailModel>> GetMails(GetMailsModel getMails)
        {
            List<Mail> mails = await mailRepository.GetMailsByEmail(getMails.EmailAddress);
            List<ListMailModel> mailsModel = new List<ListMailModel>();
            mails.ForEach(item => mailsModel.Add(new ListMailModel(item)));
            IQueryable<ListMailModel> filteredMails = FilterMails(mailsModel, getMails);
            return filteredMails;
        }

        private IQueryable<ListMailModel> FilterMails(List<ListMailModel> mailsModel, GetMailsModel getMails)
        {
            return mailsModel.Where(x =>
                    x.EmailCategory.Equals(getMails.EmailCategories_) &&
                    !x.Sender.Equals(getMails.DontReceiveFrom) &&
                    x.Sender.Contains(getMails.Filter) ||
                    x.Message.Contains(getMails.Filter) ||
                    x.Subject.Contains(getMails.Filter))
                .OrderBy(x => x.DateTime_).AsQueryable();
        }
    }
}
