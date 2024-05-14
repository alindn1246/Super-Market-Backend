
using HussieniSuperMarket.Data;
using HussieniSuperMarket.Models;
using HussieniSuperMarket.Models.AuthDTO;
using HussieniSuperMarket.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HussieniSuperMarket.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        protected ResponseDto _response;
        public AuthAPIController( AppDbContext db, IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _authService = authService;
            _response = new();
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errormessage = await _authService.Register(model);
            if ( !string.IsNullOrEmpty(errormessage))
            {
                _response.IsSuccess = false;
                _response.Message = errormessage;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User==null)
            {
                _response.IsSuccess=false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result= loginResponse;
            return Ok(_response);
           
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encountered";
                return BadRequest(_response);
            }
            
            return Ok(_response);

        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");

            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found");
            }

            return Ok(customers);
        }

        [HttpGet("customersCount")] // Changed the endpoint to reflect that it returns count
        public async Task<IActionResult> GetCustomersCount()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");

            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found");
            }

            return Ok(customers.Count); // Returning the count of customers
        }


    }
}
