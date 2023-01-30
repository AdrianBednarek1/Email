using Email.Entity;

namespace Email.Models.MailModels
{
    public class SendMailModel
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public EmailCategories EmailCategory { get; set; }
        public string DateTime_ { get; set; }
        public string Sender { get; set; }
        public string Receivers { get; set; }
        public List<string> GetListOfReceivers()
        {
            if (String.IsNullOrEmpty(Receivers)) return null;
            string removedWhiteSpace = String.Concat(Receivers.Where(c => !Char.IsWhiteSpace(c)));
            List<string> listOfReceivers = new List<string>();
            listOfReceivers.AddRange(removedWhiteSpace.Split(',').ToList());
            return listOfReceivers;
        }
    }
}
