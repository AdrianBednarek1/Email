using Email.DatabaseModel;
using Email.Entity;

namespace Email.MailRepositoryService
{
    public class MailRepository
    {
        private readonly Db database;
        public MailRepository(Db database_)
        {
            database= database_;
        }
        public async Task CreateMail(Mail mail)
        {
            database.Users.Attach(mail.Destination);
            database.Mails.Add(mail);
            await database.SaveChangesAsync();
        }
        public async Task DeleteMail(Mail mail)
        {
            database.Mails.Remove(mail);
            await database.SaveChangesAsync();
        }
    }
}
