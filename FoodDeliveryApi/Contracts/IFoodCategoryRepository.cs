using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IFoodCategoryRepository
    {
        public Task<IEnumerable<FoodCategory>> GetCategories(int id);
        public Task<string> CreateCategory(int id, string categoryName);
        public Task<string> UpdateCategory(int id, string categoryName);


    }
}

