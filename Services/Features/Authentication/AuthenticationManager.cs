using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Services.Features.Authentication
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationManager(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        //User bilgileri gelecek.
        //Oda bilgileri gelmezse default olarak appsetting.json'daki "Expires" değeri baz alınsın.
        public string GenerateToken(User user, int? expireMinutes = null)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];

            // 1. Claims: Pasaportun içindeki bilgiler (Kullanıcı kim, yetkisi ne?)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), //Kullanıcı Id
                new Claim(ClaimTypes.Name, user.NickName), //Kullanıcı NickName
                new Claim("AccessKey", user.AccessKey) //Kullanıcı AccessKey
            };

            // 2. Key & Credentials: İmzayı atacak olan mühür
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Expiration: Süre hesabı (Oda süresi mi yoksa default mu?)
            var expiresValue = expireMinutes ?? int.Parse(jwtSettings["Expires"]);
            var expires = DateTime.UtcNow.AddMinutes(expiresValue);

            // 4. Token Oluşturma
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        //Lobby oluşturma logic için önemli
        public Guid? GetUserIdFromCurrentContext() //Token içerisindeki id değerini alır.
        {
            // HttpContextAccessor üzerinden User nesnesine (Claim'lere) erişiyoruz
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var claimId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(claimId, out Guid userId))
                    return userId;
            }

            return null; // Token yoksa veya geçersizse sessizce null döner
        }
    }
}
