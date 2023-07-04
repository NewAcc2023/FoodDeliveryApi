using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;
        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<string> CreateCustomer(string firstName, string lastName)
        {
            var addCustomerQuery = $"INSERT INTO customers VALUES (@FirstName,@LastName); select @@ROWCOUNT";
            
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", firstName, DbType.String);
            parameters.Add("LastName", lastName, DbType.String);

            int affectedRows;

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstAsync<int>(addCustomerQuery,parameters);
            }
            if (affectedRows > 0)
            {
                return "Customer Created!";
            }
            return "Customer Was Not Created";
        }

        public async Task<Customer> GetCustomer(int id)
        {
            string query = $"SELECT id AS Id,first_name AS FirstName,last_name AS LastName  FROM customers WHERE id = @Id";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            
            using var connection = _context.CreateConnection();
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>(query, parameters);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            string query = "SELECT id AS Id,first_name AS FirstName,last_name AS LastName FROM customers";
            using var connection = _context.CreateConnection();
            var customers = await connection.QueryAsync<Customer>(query);
            return customers;
        }

        public async Task<string> UpdateCustomer(int id, string firstName, string lastName)
        {
            var query = $"UPDATE customers SET first_name = @FirstName, last_name = @LastName WHERE id = @Id;select @@ROWCOUNT";
            
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", firstName, DbType.String);
            parameters.Add("LastName", lastName, DbType.String);
            parameters.Add("Id", id, DbType.Int32);

            int affectedRows = 0;

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if(affectedRows > 0)
            {
                return "Customer Updated!";
            }
            return "Customer was not Updated!";
        }
    }
}
