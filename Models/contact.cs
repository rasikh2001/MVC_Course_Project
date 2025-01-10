namespace communityApp.Models
{
    public class contact
    {
        public int Id { get; set; }  // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}
