using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace store.Helper.Jwt
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string id, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim("id", id),
            new Claim("role",role)
            // You can add more claims here if needed
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetUserIdFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Retrieve the ID claim from the JWT token
                var id = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
                return id;
            }
            catch (Exception)
            {
                // Token validation failed or ID claim not found
                // You can handle this according to your application's requirements
                return null;
            }
        }

        ////public string GetUserRoleFromToken(string token)
        ////{
        ////    try
        ////    {
        ////        var tokenHandler = new JwtSecurityTokenHandler();
        ////        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        ////        // Retrieve the ID claim from the JWT token
        ////        var role = jwtToken?.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
        ////        return role;
        ////    }
        ////    catch (Exception)
        ////    {
        ////        // Token validation failed or ID claim not found
        ////        // You can handle this according to your application's requirements
        ////        return null;
        ////    }
        ////}
    }
}
