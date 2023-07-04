using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace FoodDeliveryApi.Repository
{
    public class JwtTokenManager
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;
        public JwtTokenManager(DapperContext context, IConfiguration c)
        {
            _context = context;
            _configuration = c;
        }

        public async Task<bool> IsUserInDatabase(LoginModel user)
        {
            string query = $"SELECT email, pass AS password FROM users" +
            $" WHERE email = @Email AND pass = @Password";

            var parameters = new DynamicParameters();
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                LoginModel userFromDb = await connection.QueryFirstOrDefaultAsync<LoginModel>(query,parameters);
                if (userFromDb == null)
                {
                    return false;
                }
                if (userFromDb.Email.ToLower() == user.Email.ToLower() && userFromDb.Password == user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<string> GenerateToken(LoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            //claim is used to add identity to JWT token Admin, Driver ...
            var parameters = new DynamicParameters();
            parameters.Add("Email", user.Email, DbType.String);

            string query = $"Select role_name FROM users JOIN user_roles ON users.id = user_roles.userid JOIN roles ON user_roles.roleid = roles.id WHERE users.email = @Email";
            List<string> roles;

            using (var connection = _context.CreateConnection())
            {
               roles = (await connection.QueryAsync<string>(query,parameters)).ToList();
            }

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            claims.Add(new Claim("Date", DateTime.Now.ToString()));

            var token = new JwtSecurityToken(_configuration["JwtAuth:Issuer"],
               _configuration["JwtAuth:Issuer"],
               claims,// Assuming claims is of type List<Claim> or compatible with IEnumerable<Claim>
               expires: DateTime.Now.AddMinutes(120),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
