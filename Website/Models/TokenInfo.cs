using CutItUp.Data.Data.Client;


namespace Website.Models
{
    public class TokenInfo
    {
        public bool IsValid { get; set; }
        public string? Message { get; set; }
        public Client? Client { get; set; }
    }
}
