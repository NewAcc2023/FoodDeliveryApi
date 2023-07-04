using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FoodDeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodOrderController : ControllerBase
    {
        private readonly IFoodOrderRepository _foodOrderRepo;

        public FoodOrderController(IFoodOrderRepository repo)
        {
            _foodOrderRepo = repo;
        }

        [HttpPost("/foodorders/add")]
        public async Task<IActionResult> CreateFoodOrder(int customerId, int deliveryAddressId, int driverId, int orderStatusId, int restaurantId, decimal deliveryFee, int totalAmount, DateTime orderDate, DateTime deliveryDate)
        {

            return Ok(await _foodOrderRepo.CreateFoodOrder(customerId, deliveryAddressId, driverId, orderStatusId, restaurantId, deliveryFee, totalAmount, orderDate, deliveryDate));
        }

        [HttpPut("/foodorders/setdriver/{id}")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> SetDriver(int id, int driverId)
        {
            return Ok(await _foodOrderRepo.SetDriverFoodOrder(id, driverId));
        }

        [HttpPut("/foodorders/cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            return Ok(await _foodOrderRepo.CancelFoodOrder(id));
        }
    }
}
