using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomers();
        public Task<string> CreateCustomer(string firstName, string lastName);
        public Task<string> UpdateCustomer(int id, string firstName, string lastName);
        public Task<Customer> GetCustomer(int id);
    }


}
