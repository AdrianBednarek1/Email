using Email.DatabaseModel;
using Email.Entity;
using Microsoft.EntityFrameworkCore;

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
            database.AttachRange(mail.Receivers);
            database.Mails.Add(mail);
            await database.SaveChangesAsync();
        }
        public async Task DeleteMail(Mail mail)
        {
            database.Mails.Remove(mail);
            await database.SaveChangesAsync();
        }
        public async Task<Mail> GetMailById(int id)
        {
            return await database.Mails.FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
