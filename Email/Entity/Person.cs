namespace Email.Entity
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string? PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public int UserAccountId { get; set; }
        public User UserAccount { get; set; }
    }
}
