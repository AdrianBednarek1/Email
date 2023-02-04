namespace Email.Models.MailModels
{
    public class DeleteMailModel
    {
        public string Username { get; set; }
        public int MailId{ get; set; }
        public DeleteMailModel(string username_, int mailId_)
        {
            Username = username_;
            MailId = mailId_;
        }
    }
    
}
