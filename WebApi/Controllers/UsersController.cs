using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation.UserEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseAdminApiKey]
    [Authorize(Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new user", Description = "Creates a new user in the system.")]
        [SwaggerResponse(200, "User successfully created")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(UserValidationErrorExample))]
        public async Task<IActionResult> Create(AddUserForm userForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(userForm);

            var result = await _userService.CreateUserAsync(userForm);
            return result ? Ok() : BadRequest();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all users", Description = "Retrieves a list of all users.")]
        [SwaggerResponse(200, "Returns all users", typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsersAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by ID", Description = "Retrieves a user by their unique ID.")]
        [SwaggerResponse(200, "Returns the user", typeof(User))]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _userService.GetUserByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update a user", Description = "Updates an existing user with new information.")]
        [SwaggerResponse(200, "User successfully updated")]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]
        public async Task<IActionResult> Update(UpdateUserForm userForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(userForm);

            var result = await _userService.UpdateUserAsync(userForm);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a user", Description = "Deletes a user by their unique ID.")]
        [SwaggerResponse(200, "User successfully deleted")]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            return result ? Ok(result) : NotFound();
        }

    }
}
