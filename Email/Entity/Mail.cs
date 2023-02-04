using System.ComponentModel.DataAnnotations.Schema;

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
        public string Sender { get; set; }
        private string receivers;
        [NotMapped]
        public List<string> Receivers
        {
            get { return receivers.Split(",").ToList(); }
            set { receivers = String.Join(",",value); }
        }
        public int DestinationId { get; set; }
        public User Destination { get; set; }
    }
}
