using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<string> CreateUser(string firstName, string lastName, string email, string password, string roleName)
        {
            int roleId = 0;
            int insertedUserId = 0;

            var parameters = new DynamicParameters();
            parameters.Add("roleName", roleName, DbType.String);
            parameters.Add("firstName", firstName, DbType.String);
            parameters.Add("lastName", lastName, DbType.String);
            parameters.Add("email", email, DbType.String);
            parameters.Add("password", password, DbType.String);
            //check if the role is right and recieving id of this role
            using (var connection = _context.CreateConnection())
            {
                roleId = await connection.QueryFirstOrDefaultAsync<int>("select id from roles where role_name = @roleName", parameters);
            }
            if (roleId == 0) { return "Provided role does not exist!"; }

            string query = $"INSERT INTO users VALUES (@firstName,@lastName,@email,@password);" +
            " SELECT CAST(scope_identity() AS int);";
            //insert new user
            using (var connection = _context.CreateConnection())
            {
                insertedUserId = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }

            if (insertedUserId == 0)
            {
                return "User Was Not Created!";
            }

            parameters.Add("userid", insertedUserId, DbType.Int32);
            parameters.Add("roleid", roleId, DbType.Int32);
            //add relation many to many between new user and his role
            using (var connection = _context.CreateConnection())
            {
                await connection.QueryAsync($"INSERT INTO user_roles VALUES (@userid,@roleId);", parameters);
            }
            
            return $"User named {firstName} {lastName} that has role of a {roleName} was Created";
        }
    }
}
