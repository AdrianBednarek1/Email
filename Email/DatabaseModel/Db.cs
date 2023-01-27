using Email.Entity;
using Microsoft.EntityFrameworkCore;

namespace Email.DatabaseModel
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Emails { get; set; }
        public DbSet<Person> People { get; set; }
        public Db(DbContextOptions<Db> db):base(db)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.SentMails).WithOne(x => x.Sender).HasForeignKey(x=>x.SenderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.UserAccount).WithOne(x=>x.PersonalInfo);
            modelBuilder.Entity<User>().HasMany(x=>x.ReceivedMails).WithMany(x=>x.Receivers).UsingEntity(x=>x.ToTable("UserMail"));
        }
    }
}
