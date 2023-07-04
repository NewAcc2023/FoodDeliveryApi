using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>> GetRestaurants();
        public Task<Restaurant> GetRestaurant(int id);
        public Task<string> CreateRestaurant(int houseNumber, string streetName, string city, string postalCode, string restaurantName);
        public Task<string> UpdateRestaurant(int id, string name);
        public Task<IEnumerable<MenuItem>> GetRestaurantMenu(int id);
    }
}
