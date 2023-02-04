using Email.DatabaseModel;
using Email.Entity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Email.UserRepositoryService
{
    public class UserRepository
    {
        private readonly Db database;
        public UserRepository(Db database_)
        {
            database = database_;
        }
        public async Task<List<User>> GetAccounts()
        {
            return await database.Users.ToListAsync();
        }
        public async Task CreateUser(User user)
        {
            if (user == null) return;
            await database.Users.AddAsync(user);
            await database.SaveChangesAsync();
        }
        public async Task Delete(User? user)
        {
            if (user == null) return; 
            database.Users.Remove(user);
            await database.SaveChangesAsync();
        }
        public async Task Update(User? user)
        {
            var updateItem = await database.Users.FindAsync(user?.Id);
            if (updateItem == null) return;
            database.Users.Entry(updateItem).CurrentValues.SetValues(user);
            await database.SaveChangesAsync();
        }
        public async Task<User?> GetAccountById(int id)
        {
            return await database.Users.FindAsync(id);
        }
        public async Task<User?> GetUserByEmail(string emailAdress)
        {
            return await database.Users
                .Include(x => x.Mails)
                .Include(x => x.PersonalInfo)
                .SingleOrDefaultAsync(user => user.EmailAddress.Equals(emailAdress));
        }

        //public async Task DeleteMailFromUser(User? user, Mail mail)
        //{
        //    database.Users.Attach(user);
        //    user.ReceivedMails.Remove(mail);
        //    await database.SaveChangesAsync();
        //}
    }   
}
