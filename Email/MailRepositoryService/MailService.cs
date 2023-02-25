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
        public async Task<IQueryable<ListMailModel>> GetMails(MailFilterModel getMails)
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
        private IQueryable<ListMailModel> FilterMails(IQueryable<ListMailModel> mailsModel, MailFilterModel getMails)
        {
            if(getMails.Filter is not null) return mailsModel.Where(x => 
            x.Sender.Contains(getMails.Filter) ||
            x.Message.Contains(getMails.Filter) ||
            x.Subject.Contains(getMails.Filter))
                    .OrderByDescending(x => x.DateTime_);

            if(getMails.EmailCategories_ is not null && getMails.EmailCategories_ != EmailCategories.Primary) 
                return mailsModel.Where(x =>
                x.EmailCategory.Equals(getMails.EmailCategories_))
                    .OrderByDescending(x => x.DateTime_);

            if (getMails.EmailType is not null) return mailsModel.Where(x =>
            x.EmailType.Equals(getMails.EmailType) &&
            x.EmailCategory!=EmailCategories.Junk)
                    .OrderByDescending(x => x.DateTime_);

            return mailsModel.Where(x =>
            x.EmailCategory.Equals(EmailCategories.Primary) &&
            x.EmailType == EmailTypes.Received)
                    .OrderByDescending(x => x.DateTime_);
        }
        private List<Mail> ModelsToEntity(SendMailModel mailModel, List<User> destinations)
        {
            List<Mail> mails = new List<Mail>();
            int count = 0;
            foreach (var destination in destinations)
            {
                count++;
                bool mailForSender = destinations.Count() == count;
                Mail email = ModelToEntity(mailModel, destination);
                email.EmailType = mailForSender ? EmailTypes.Sent : EmailTypes.Received;
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
            bool isInJunk = mail.EmailCategory == EmailCategories.Junk;
            if (isInJunk) 
            {
                await mailRepository.DeleteMail(mail);
                return;
            }
            mail.EmailCategory = EmailCategories.Junk;
            await mailRepository.UpdateMail(mail);
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
