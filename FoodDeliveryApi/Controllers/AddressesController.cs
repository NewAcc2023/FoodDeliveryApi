using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [Route("/addresses")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;

        public AddressesController(IAddressRepository addressRepo)
        {
            _addressRepo = addressRepo;
        }

        [HttpPut("/addresses/edit/{id}")]
        public async Task<IActionResult> UpdateAddress(int id, int houseNumber, string streetName, string city, string postalCode)
        {
            string result = await _addressRepo.UpdateAddress(id, houseNumber, streetName, city, postalCode);
            return Ok(result);
        }
        [HttpGet("/addresses/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            Address address = await _addressRepo.GetAddress(id);
            if (address == null)
            {
                return NotFound("No such address in database");
            }
            return Ok(address);
        }

    }
}
