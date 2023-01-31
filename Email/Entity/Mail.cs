namespace Email.Entity
{
    public enum EmailCategories
    {
        Primary,
        Promotion,
        SocialMedia,
        Unwanted,
        Junk
    }
    public class Mail
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string DateTime_ { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public bool Seen { get; set; } = false;
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public ICollection<User> Receivers { get; set; }
    }
}
