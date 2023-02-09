using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;
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
    public enum EmailTypes
    {
        Sent,
        Received
    }
    public class Mail
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string DateTime_ { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public EmailTypes EmailType { get; set; }
        public bool Seen { get; set; } = false;
        public string Sender { get; set; }
        public string Receivers_ { get; set; }
        [NotMapped]
        public List<string> Receivers
        {
            get { return Receivers_.Split(",").ToList(); }
            set { Receivers_ = String.Join(",",value); }
        }

        public int DestinationId { get; set; }
        public User Destination { get; set; }
    }
}
