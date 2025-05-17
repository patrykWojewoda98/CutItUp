using CutItUp.Data.Context;
using CutItUp.Data.Data.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Website.Models;



namespace Website.Services
{
    public class JwtService
    {
        public readonly string _secretKey;
        private readonly int _expirationDays;
        public readonly CutItUpContext _context; 
        public JwtService(CutItUpContext cutItUpContext)
        {
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new Exception("Zmienna środowiskowa JWT_SECRET nie została ustawiona");
            var _expirationDaysString = Environment.GetEnvironmentVariable("JWT_EXPIRATION_DAYS")
                ?? throw new Exception("Zmienna środowiskowa JWT_EXPIRATION_DAYS nie została ustawiona");
            _expirationDays = int.TryParse(_expirationDaysString, out var expirationDays) ? expirationDays : 7;
            _context = cutItUpContext;
        }
        public string GenerateToken(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim("id", client.Id.ToString()),
                new Claim("email", client.Email),
                new Claim("firstName", client.FirstName),
                new Claim("lastName", client.LastName),
                new Claim("phoneNumber", client.PhoneNumber),
                new Claim("address", client.Address),
                new Claim("city", client.City),
                new Claim("postalCode", client.PostalCode),
                new Claim("country", client.Country),
                new Claim("login", client.Login),
            };
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_expirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);
        }

        public TokenInfo CheckToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return new TokenInfo
                {
                    IsValid = true,
                    Message = "Token is valid"
                };
            }
            catch (SecurityTokenExpiredException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Token is expired"
                };
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Invalid token signature"
                };
            }
            catch (SecurityTokenInvalidIssuerException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Invalid token issuer"
                };
            }
            catch (SecurityTokenInvalidAudienceException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Invalid token audience"
                };
            }
            catch (SecurityTokenNoExpirationException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Token has no expiration"
                };
            }
            catch (SecurityTokenInvalidLifetimeException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Invalid token lifetime"
                };
            }
            catch (ArgumentException)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Malformed token"
                };
            }
            catch (Exception)
            {
                return new TokenInfo
                {
                    IsValid = false,
                    Message = "Unknown token validation error"
                };
            }
        }

        public TokenInfo DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            var idClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            int? id = null;

            if (int.TryParse(idClaim, out var parsedId))
            {
                id = parsedId;
            }

            var client = _context.Client.FirstOrDefault(c => c.Id == id);

            return new TokenInfo
            {
                Client = client,
                IsValid = true,
                Message = "Token is valid"
            };
        }

    }
}
