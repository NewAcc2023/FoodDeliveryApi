namespace FoodDeliveryApi.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int AddressId { get; set; }
        public string AddressName { get; set; }
    }
}
