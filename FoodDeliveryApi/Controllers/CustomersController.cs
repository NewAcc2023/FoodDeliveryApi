using FoodDeliveryApi.Contracts;
using FoodDeliveryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers
{
    [Route("/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            List<Customer> customer =(await _customerRepo.GetCustomers()).ToList();
            return Ok(customer);
        }

        [HttpPost("/customers/add")]
        public async Task<IActionResult> CreateCustomer(string firstName, string lastName)
        {
            return Ok(await _customerRepo.CreateCustomer(firstName, lastName));
        }
        [HttpPut("/customers/edit/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, string firstName, string lastName)
        {
            return Ok(await _customerRepo.UpdateCustomer(id, firstName, lastName));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer c = await _customerRepo.GetCustomer(id);
            if (c == null)
            {
                return NotFound("No such customer in database");
            }
            return Ok(c);
        }
    }
}
