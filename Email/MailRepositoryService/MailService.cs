using Email.Entity;
using Email.Models.MailModels;

namespace Email.MailRepositoryService
{
    public class MailService
    {
        private readonly MailRepository mailRepository;
        public MailService(MailRepository mailRepository_) 
        {
            mailRepository = mailRepository_;
        }
        public async Task<bool> SendMails(SendMailModel modelEmail, List<User> destinations)
        {
            List<Mail> mails = ModelsToEntity(modelEmail, destinations);
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
            IQueryable<ListMailModel> filteredMails = FilterMails(mailsModel.AsQueryable(), getMails);
            return filteredMails;
        }
        public async Task<MailModel> GetMailById(int mailId)
        {
            Mail mail = await mailRepository.GetMailById(mailId);
            MailModel mailModel = new MailModel(mail);
            return mailModel;
        }
        private IQueryable<ListMailModel> FilterMails(IQueryable<ListMailModel> mailsModel, GetMailsModel getMails)
        {
            return mailsModel.Where(x =>
                    x.EmailCategory.Equals(getMails.EmailCategories_) &&
                    x.EmailType.Equals(getMails.EmailType) && (
                    x.Sender.Contains(getMails.Filter) ||
                    x.Message.Contains(getMails.Filter) ||
                    x.Subject.Contains(getMails.Filter)))
                .OrderBy(x => x.DateTime_);
        }
        private List<Mail> ModelsToEntity(SendMailModel mailModel, List<User> destinations)
        {
            List<Mail> mails = new List<Mail>();
            int count = 0;
            foreach (var destination in destinations)
            {
                count++;
                bool thisIsLastItem = destinations.Count() == count;
                Mail email = ModelToEntity(mailModel, destination);
                email.EmailType = thisIsLastItem ? EmailTypes.Sent : EmailTypes.Received;
                mails.Add(email);
            }
            return mails;
        }
        private Mail ModelToEntity(SendMailModel mailModel, User destination)
        {
            Mail email = new Mail()
            {
                DateTime_ = mailModel.DateTime_,
                EmailCategory = mailModel.EmailCategory,
                Message = mailModel.Message,
                Sender = mailModel.Sender,
                Subject = mailModel.Subject,
                Receivers = mailModel.ReceiversToList(),
                Destination = destination,
            };
            return email;
        }
		public async Task Delete(int mailId)
		{
            Mail mail = await mailRepository.GetMailById(mailId);
            await mailRepository.DeleteMail(mail);
		}

        public async Task UpdateMail(UpdateMail updateMail)
        {
            Mail mail = await mailRepository.GetMailById(updateMail.Id);
            mail.EmailCategory = updateMail.EmailCategory;
            mail.Seen= updateMail.Seen;
            await mailRepository.UpdateMail(mail);
        }
    }
}
