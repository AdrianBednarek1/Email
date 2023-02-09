using Email.Entity;

namespace Email.Models.MailModels
{
    public class UpdateMail
    {
        public UpdateMail(MailModel mailModel)
        {
            Id= mailModel.Id;
            EmailCategory = mailModel.EmailCategory;
            Seen = true;
        }

        public int Id { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public bool Seen { get; set; }
    }
}
