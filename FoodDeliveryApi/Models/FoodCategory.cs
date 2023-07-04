namespace FoodDeliveryApi.Models
{
    public class FoodCategory
    {
        public int Id { get; set; }
        public string FoodCategoryName { get; set; }
        public int RestaurantId { get; set; }
    }
}