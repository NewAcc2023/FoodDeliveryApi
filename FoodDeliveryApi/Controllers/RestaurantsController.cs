using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace FoodDeliveryApi.Controllers
{
    [Route("/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;

        public RestaurantsController(IRestaurantRepository restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> GetRestaurants()
        {
                var restaurants = await _restaurantRepo.GetRestaurants();
                return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
                var r = await _restaurantRepo.GetRestaurant(id);
                if (r == null)
                    return NotFound("restaurant does not exist");
                return Ok(r);
        }

        [HttpPost("/restaurants/add")]
        public async Task<IActionResult> CreateRestaurant(int houseNumber, string streetName, string city, string postalCode, string restaurantName)
        {
            return Ok(await _restaurantRepo.CreateRestaurant(houseNumber, streetName, city, postalCode, restaurantName));
        }

        [HttpPut("/restaurants/edit/{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, string newName)
        {
            return Ok(await _restaurantRepo.UpdateRestaurant(id, newName));
        }

        [HttpGet("/restaurants/menu")]
        public async Task<ActionResult> GetRestaurantMenu(int id)
        {
            var menu = await _restaurantRepo.GetRestaurantMenu(id);
            if (menu == null)
            {
                return NotFound("either restaurant not exist or no food items yet");
            }
            return Ok(menu);
        }
    }
}
