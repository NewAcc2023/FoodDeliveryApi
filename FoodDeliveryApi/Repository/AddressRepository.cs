using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class AddressRepository : IAddressRepository
    {

        private readonly DapperContext _context;
        public AddressRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Address> GetAddress(int id)
        {
            string query = $"SELECT id AS Id, house_number AS HouseNumber,street_name AS StreetName, city AS City, postal_code AS PostalCode FROM addresses WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using var connection = _context.CreateConnection();
            Address address = await connection.QueryFirstOrDefaultAsync<Address>(query, parameters);
            return address;
        }

        public async Task<string> UpdateAddress(int id, int houseNumber, string streetName, string city, string postalCode)
        {
            var query = $"UPDATE addresses SET house_number = @HouseNumber, street_name = @StreetName,city = @City, postal_code = @PostalCode WHERE id = @Id; Select @@ROWCOUNT";
            
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("HouseNumber", houseNumber, DbType.Int32);
            parameters.Add("StreetName", streetName, DbType.String);
            parameters.Add("City", city, DbType.String);
            parameters.Add("PostalCode", postalCode, DbType.String);

            int updatedRows;
            using (var connection = _context.CreateConnection())
            {
                updatedRows = await connection.QueryFirstOrDefaultAsync<int>(query,parameters); 
            }
            if (updatedRows > 0)
            {
                return "Address Was Updated!";
            }
            return "Address Was Not Updated";
        }
    }
}
