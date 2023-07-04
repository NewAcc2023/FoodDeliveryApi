using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IFoodItemRepository
    {
        public Task<IEnumerable<FoodItem>> GetFoodItems(int id);

        public  Task<IEnumerable<FoodItem>> GetFoodItemsBySearch(string query);

        public Task<string> UpdateFoodItem(int id, string foodItemName, decimal foodItemPrice, int foodCategoryId);

    }
}
