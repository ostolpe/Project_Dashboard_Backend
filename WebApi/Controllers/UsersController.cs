using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Create(AddUserForm userForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(userForm);

            var result = await _userService.CreateUserAsync(userForm);
            return result ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsersAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _userService.GetUserByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserForm userForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(userForm);

            var result = await _userService.UpdateUserAsync(userForm);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return result ? Ok(result) : NotFound();
        }

    }
}
