using FoodDeliveryApi.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [Route("/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("/users/add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(string firstName, string lastName, string email, string password, string roleName)
        {
            return Ok(await _userRepo.CreateUser(firstName, lastName, email, password, roleName));
        }
    }
}
