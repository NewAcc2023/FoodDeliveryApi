using Dapper;
using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DapperContext _context;
        public RestaurantRepository(DapperContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            var query = "SELECT restaurants.id AS Id, restaurant_name as RestaurantName, addresses.street_name as AddressName, addresses.id as AddressId" +
                " FROM restaurants JOIN addresses ON restaurants.address_id = addresses.id ";
            using var connection = _context.CreateConnection();
            var restaurants = await connection.QueryAsync<Restaurant>(query);
            return restaurants;
        }
        public async Task<Restaurant> GetRestaurant(int id)
        {
            var query = $"SELECT restaurants.id AS Id, restaurant_name as RestaurantName, addresses.street_name as AddressName, addresses.id as AddressId" +
                $" FROM restaurants JOIN addresses ON restaurants.address_id = addresses.id WHERE restaurants.id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                Restaurant restaurant = await connection.QueryFirstOrDefaultAsync<Restaurant>(query, parameters);
                return restaurant;
            }
        }
        public async Task<string> CreateRestaurant(int houseNumber, string streetName, string city, string postalCode, string restaurantName)
        {
            var query = $"INSERT INTO addresses VALUES (@houseNumber,@streetName,@city,@postalCode);" +
             "SELECT CAST(scope_identity() AS int);";
            int affectedRows = 0;
            int addressId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("houseNumber", houseNumber, DbType.Int32);
            parameters.Add("streetName", streetName, DbType.String);
            parameters.Add("city", city, DbType.String);
            parameters.Add("postalCode", postalCode, DbType.String);
            parameters.Add("restaurantName", restaurantName, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                addressId = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
                if (addressId < 1)
                {
                    return "Restaurant was not Created!";
                }
            }

            parameters.Add("addressId", addressId, DbType.Int32);

            query = $"INSERT INTO restaurants VALUES (@restaurantName,@addressId);" +
             "SELECT @@ROWCOUNT";
            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }

            if (affectedRows > 0)
            {
                return "Restaurant Created!";
            }
            return "Restaurant was not Created!";
        }

        public async Task<string> UpdateRestaurant(int id, string name)
        {
            var query = $"UPDATE restaurants SET restaurant_name = @name WHERE id = @id; SELECT @@ROWCOUNT";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("name", name, DbType.String);

            int affectedRows = 0;
            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Restaurant Updated!";
            }
            return "Restaurant was not Updated!";
        }

        public async Task<IEnumerable<MenuItem>> GetRestaurantMenu(int id)
        {
            var query = $"SELECT restaurant_id AS Id,food_item_name AS FoodItemName, food_item_price AS FoodItemPrice FROM restaurants JOIN food_categories  ON restaurants.id = food_categories.restaurant_id JOIN food_items ON food_items.food_category_id = food_categories.id WHERE restaurants.id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                var menu = await connection.QueryAsync<MenuItem>(query,parameters);
                return menu;
            }
        }
    }
}


