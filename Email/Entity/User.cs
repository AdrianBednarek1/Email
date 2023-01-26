namespace Email.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Person PersonalInfo { get; set; }
        public ICollection<Mail> SentMails { get; set; } 
        public ICollection<Mail> ReceivedMails { get; set; }
    }
}
