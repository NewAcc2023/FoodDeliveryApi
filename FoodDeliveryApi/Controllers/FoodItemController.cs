using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemRepository _repo;

        public FoodItemController(IFoodItemRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("/fooditems/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            List<FoodItem> fi =( await _repo.GetFoodItems(id)).ToList();
            if (fi == null)
            {
                return NotFound("no such category");
            }
           return Ok(fi);
        }

        [HttpGet("/fooditems/search/")]
        public async Task<IActionResult> Get(string query)
        {
            List<FoodItem> fi = (await _repo.GetFoodItemsBySearch(query)).ToList();
            if (fi == null)
            {
                return NotFound("no such food items");
            }
            return Ok(fi);
        }

        [HttpPut("/fooditems/edit/{id}")]
        public async Task<IActionResult> Edit(int id, string foodItemName, decimal foodItemPrice, int foodCategoryId)
        {
            return Ok(await _repo.UpdateFoodItem(id, foodItemName, foodItemPrice, foodCategoryId));
        }
    }
}
