using clutch_identity.Data;

namespace clutch_identity.Requests
{
    public class PostUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Role Role { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
