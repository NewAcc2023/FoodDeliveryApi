using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FoodDeliveryApi.Controllers
{
    [Route("/foodcategory")]
    [ApiController]
    public class FoodCategoriesController : ControllerBase
    {
        private readonly IFoodCategoryRepository _repo;

        public FoodCategoriesController(IFoodCategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("/foodcategory/{id}")]
        public async Task<IActionResult> GetCategories(int id)
        {
            List<FoodCategory> fc = (await _repo.GetCategories(id)).ToList();
            if (fc == null)
            {
                return NotFound("no such restaurant id");
            }
            return Ok(fc);
        }

        [HttpPost("/foodcategory/add/{id}")]
        [Authorize]
        public async Task<IActionResult> CreateCategory(int id, string categoryName)
        {
            return Ok(await _repo.CreateCategory(id, categoryName)); 
        }
        [HttpPut("/foodcategory/edit/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, string categoryName)
        {
            return Ok(await _repo.UpdateCategory(id, categoryName));
        }
    }
}
