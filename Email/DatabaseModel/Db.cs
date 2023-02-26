using Email.Entity;
using Microsoft.EntityFrameworkCore;
using Email.Models;

namespace Email.DatabaseModel
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Person> People { get; set; }
        public Db(DbContextOptions<Db> db):base(db)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Mails)
                .WithOne(x => x.Destination)
                .HasForeignKey(x=>x.DestinationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasOne(x => x.UserAccount).WithOne(x=>x.PersonalInfo);
        }
        public DbSet<Email.Models.LoggedModel> LoggedModel { get; set; } = default!;
    }
}
