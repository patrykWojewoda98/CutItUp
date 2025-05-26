namespace Website.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string Password { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Address { get; set; } = string.Empty;


        public string? City { get; set; }

        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
