using Microsoft.IdentityModel.Tokens;
using Server.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public static class JwtHelpers
    {
        public static SymmetricSecurityKey GetKey()
        {
            var secretBytes = Encoding.UTF8.GetBytes(SystemConstant.Secret);
            return new SymmetricSecurityKey(secretBytes);
        }
        public static string GetToken()
        {
            var token = new JwtSecurityToken(
                issuer: SystemConstant.Issuer,
                audience: SystemConstant.Audience,
                claims: GetClaims(),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(SystemConstant.TokenExpMin),
                signingCredentials: getCredentials()
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static Claim[] GetClaims()
        {

            return new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"KullaniciId"),
                new Claim("TestVerisi","BuğraÖçalan")
            };

        }
        private static SigningCredentials getCredentials()
        {

            return new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256);
        }
    }
}
