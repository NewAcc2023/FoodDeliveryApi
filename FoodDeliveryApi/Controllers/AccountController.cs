using Dapper;
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
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenManager _tokenManager;
        public AccountController(JwtTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginModel user)
        {
            if (!(await _tokenManager.IsUserInDatabase(user)))
            {
                return NotFound("no such user in database");
            }
            else
            {
                return Ok(await _tokenManager.GenerateToken(user));
            }
        }
    }
}
