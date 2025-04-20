using Business.Dtos;
using Business.Managers;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Documentation.AuthEndPoints;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthController(IAuthService authService, ITokenManager tokenManager, UserManager<UserEntity> userManager) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly UserManager<UserEntity> _userManager = userManager;

        [HttpPost("signup")]
        [SwaggerOperation(Summary = "Register a new user", Description = "Creates a new user account with email and password.")]
        [SwaggerRequestExample(typeof(SignUpForm), typeof(SignUpFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "User registered successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(AuthValidationErrorExample))]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(form);

            var result = await _authService.SignUpAsync(form);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest();
        }

        [HttpPost("signin")]
        [SwaggerOperation(Summary = "Authenticate a user", Description = "Validates user credentials and returns a JWT token.")]
        [SwaggerRequestExample(typeof(SignInForm), typeof(SignInFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Authentication successful", typeof(TokenResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(AuthValidationErrorExample))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(AuthUnauthorizedExample))]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(form);

            var result = await _authService.SignInAsync(form);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(form.Email);
                if (user == null)
                {
                    return NotFound();
                }

                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user!.Id),
                    new(JwtRegisteredClaimNames.Name, user.UserName!),
                    new(JwtRegisteredClaimNames.Email, user.Email!)
                };

                if (isAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }

                var token = _tokenManager.GenerateJwtToken(claims);

                return Ok(new { token });
            }

            return Unauthorized("Invalid email or password.");
        }
    }
}
