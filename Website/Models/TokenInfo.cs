namespace Website.Models
{
    public class TokenInfo
    {
        public bool IsValid { get; set; }
        public string? Message { get; set; }
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
