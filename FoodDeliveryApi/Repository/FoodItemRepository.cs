using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Models;
using Dapper;
using FoodDeliveryApi.Contracts;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly DapperContext _context;
        public FoodItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodItem>> GetFoodItems(int id)
        {
            using var connection = _context.CreateConnection();
            var fooditems = await connection.QueryAsync<FoodItem>($"SELECT id AS Id, food_item_name AS FoodItemName,food_item_price AS FoodItemPrice,food_category_id AS FoodCategoryId FROM food_items WHERE food_category_id = {id}");
            return fooditems;
        }

        public async Task<IEnumerable<FoodItem>> GetFoodItemsBySearch(string query)
        {
            using var connection = _context.CreateConnection();
            var fooditems = await connection.QueryAsync<FoodItem>($"SELECT id AS Id, food_item_name AS FoodItemName,food_item_price AS FoodItemPrice,food_category_id AS FoodCategoryId FROM food_items WHERE food_item_name LIKE '%{query}%'");
            return fooditems;
        }
        public async Task<string> UpdateFoodItem(int id, string foodItemName, decimal foodItemPrice, int foodCategoryId)
        {
            var query = $"UPDATE food_items SET food_item_name = @FoodItemName, food_item_price = @FoodItemPrice, food_category_id = @FoodCategoryId  WHERE id = @Id; select @@ROWCOUNT";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FoodItemName", foodItemName, DbType.String);
            parameters.Add("FoodItemPrice", foodItemPrice, DbType.Decimal);
            parameters.Add("FoodCategoryId", foodCategoryId, DbType.Int32);

            int affectedRows = 0;

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Food Item Updated!";
            }
            return "Food Item was not Updated!";
        }
    }
}
