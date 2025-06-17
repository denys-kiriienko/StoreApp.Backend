using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.BLL.Services;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRoleById(int id, [FromQuery] string role)
        {
            var result = await _userService.UpdateRoleByIdAsync(id, role);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _userService.DeleteUserByIdAsync(id);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
