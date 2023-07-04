namespace FoodDeliveryApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
