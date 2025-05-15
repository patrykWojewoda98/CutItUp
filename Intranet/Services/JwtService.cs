namespace Intranet.Services
{
    public class JwtService
    {
        public readonly string _secretKey;
        private readonly int _expirationDays;
        public JwtService()
        {
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new Exception("Zmienna środowiskowa JWT_SECRET nie została ustawiona");
            var _expirationDaysString = Environment.GetEnvironmentVariable("JWT_EXPIRATION_DAYS")
                ?? throw new Exception("Zmienna środowiskowa JWT_EXPIRATION_DAYS nie została ustawiona");
            _expirationDays = int.TryParse(_expirationDaysString, out var expirationDays) ? expirationDays : 7;
        }
    }
}
