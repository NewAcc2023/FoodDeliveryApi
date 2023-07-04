using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IFoodOrderRepository
    {
        public Task<string> CreateFoodOrder(int customerId, int deliveryAddressId, int driverId, int orderStatusId, int restaurantId, decimal deliveryFee, int totalAmount, DateTime orderDate, DateTime deliveryDate);

        public Task<string> SetDriverFoodOrder(int order_id, int driver_id);

        public Task<string> CancelFoodOrder(int order_id);
    }
}
