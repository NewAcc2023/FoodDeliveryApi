using FoodDeliveryApi.contexts;
using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Dapper;
using System.Data;

namespace FoodDeliveryApi.Repository
{
    public class FoodCategoryRepository : IFoodCategoryRepository
    {

        private readonly DapperContext _context;
        public FoodCategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<string> CreateCategory(int id,string categoryName )
        {
            var addCategory = $"INSERT INTO food_categories VALUES (@CategoryName',@Id); SELECT @@ROWCOUNT";

            var parameters = new DynamicParameters();
            parameters.Add( "Id", id , DbType.Int32);
            parameters.Add("CategoryName", categoryName, DbType.String);

            int affectedRows = 0;

            using (var connection = _context.CreateConnection())
            {
                affectedRows =await connection.QueryFirstOrDefaultAsync<int>(addCategory, parameters);
            }
            if(affectedRows > 0)
            {
                return "Category Created!";
            }
            return "Category was not Created!";
        }

        public async Task<IEnumerable<FoodCategory>> GetCategories(int id)
        {
            using var connection = _context.CreateConnection();
            var foodcategories = await connection.QueryAsync<FoodCategory>($"SELECT id AS Id,food_category_name AS FoodCategoryName,restaurant_id AS RestaurantId FROM food_categories WHERE restaurant_id = {id}");
            return foodcategories;
        }

        public async Task<string> UpdateCategory(int id, string categoryName)
        {
            var query = $"UPDATE food_categories SET food_category_name = @CategoryName WHERE id = @Id; select @@ROWCOUNT";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("CategoryName", categoryName, DbType.String);

            int affectedRows = 0;

            using (var connection = _context.CreateConnection())
            {
                affectedRows = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            }
            if (affectedRows > 0)
            {
                return "Category Updated!";
            }
            return "Category was not Updated!";
        }
    }
}
