namespace clutch_identity.Resources
{
    public class UserResource
    {
        public long Id { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}
