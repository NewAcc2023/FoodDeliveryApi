namespace FoodDeliveryApi.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryAddressId { get; set; }
        public int DriverId { get; set; }
        public int OrderStatusId { get; set;}
        public int RestaurantId { get; set; }
        public double DeliveryFee { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderDatetime { get; set; }
        public DateTime RequestedDeliveryDatetime { get; set; }

       
    }
}
