using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Contracts
{
    public interface IUserRepository
    {
        public Task<string> CreateUser(string firstName, string lastName, string email, string password, string RoleName);
    }
}
