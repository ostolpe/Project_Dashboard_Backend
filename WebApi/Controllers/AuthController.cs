﻿using Business.Dtos;
using Business.Managers;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ITokenManager tokenManager, UserManager<UserEntity> userManager) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly UserManager<UserEntity> _userManager = userManager;

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(form);

            var result = await _authService.SignUpAsync(form);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(form);

            var result = await _authService.SignInAsync(form);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(form.Email);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user!.Id),
                    new(JwtRegisteredClaimNames.Name, user.UserName!),
                    new(JwtRegisteredClaimNames.Email, user.Email!)
                };

                var token = _tokenManager.GenerateJwtToken(claims);

                return Ok(new { token });
            }

            return Unauthorized("Invalid email or password.");
        }
    }
}
