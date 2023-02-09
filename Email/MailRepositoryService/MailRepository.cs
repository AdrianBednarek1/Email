using Email.DatabaseModel;
using Email.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

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
        public async Task<List<Mail>> GetMailsByEmail(string emailAddress)
        {
            return await database.Mails
                .Include(x=>x.Destination)
                .Where(x=>x.Destination.EmailAddress.Equals(emailAddress))
                .ToListAsync();
        }

		public async Task<Mail> GetMailById(int mailId)
		{
            return await database.Mails
                .Include(x=>x.Destination)
                .FirstOrDefaultAsync(x=>x.Id==mailId);
		}

        public async Task UpdateMail(Mail mail)
        {
            database.Mails.Update(mail);
            await database.SaveChangesAsync();
        }
    }
}
