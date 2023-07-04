using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApi.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string FoodItemName { get; set; }
    
        public string FoodItemPrice { get; set; }
    
        public int FoodCategoryId { get; set; }
    }
}
