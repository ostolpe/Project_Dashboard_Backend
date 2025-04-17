using Business.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public interface IAuthService
    {
        Task<SignInResult> SignInAsync(SignInForm form);
        Task<IdentityResult> SignUpAsync(SignUpForm form);
    }

    public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        public async Task<IdentityResult> SignUpAsync(SignUpForm form)
        {
            var user = new UserEntity
            {
                UserName = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
            };

            user.Address = new UserAddressEntity
            {
                UserId = user.Id
            };

            var result = await _userManager.CreateAsync(user, form.Password);

            if (result.Succeeded && !string.IsNullOrEmpty(form.Role))
                await _userManager.AddToRoleAsync(user, form.Role);

            return result;
        }

        public async Task<SignInResult> SignInAsync(SignInForm form)
        {
            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
            return result;
        }
    }
}
