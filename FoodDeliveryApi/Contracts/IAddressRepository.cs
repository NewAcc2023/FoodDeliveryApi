using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IAddressRepository
    {
        public Task<string> UpdateAddress(int id, int houseNumber, string streetName, string city, string postalCode);
        public Task<Address> GetAddress(int id);
    }
}
