using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(AddUserForm userForm);
        Task<bool> DeleteUserAsync(string userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string userId);
        Task<IEnumerable<User>?> GetUsersAsync();
        Task<bool> UpdateUserAsync(UpdateUserForm userForm);
    }

    public class UserService(IUserRepository userRepository, IMemoryCache cache, UserManager<UserEntity> userManager) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMemoryCache _cache = cache;
        private readonly UserManager<UserEntity> _userManager = userManager;
        private const string _cacheKey_All = "User_All";

        public async Task<bool> CreateUserAsync(AddUserForm userForm)
        {
            if (userForm == null)
                return false;

            var exists = await _userRepository.ExistsAsync(x => x.Email == userForm.Email);
            if (exists)
                return false;

            var entity = UserFactory.CreateUserEntity(userForm);

            //det kommer ju inte in något password från detta form. bör det sättas ett default/random password här och skickas med i create? 
            var result = await _userManager.CreateAsync(entity);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(entity, userForm.Role);

            if (result.Succeeded)
            {
                _cache.Remove(_cacheKey_All);
            }

            return result.Succeeded;
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            var user = new User();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
            {
                user = cachedItems?.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                    return user;
            }

            var entity = await _userRepository.GetAsync(x => x.Id == userId);
            if (entity == null)
                return null;

            await SetCache();

            user = UserFactory.CreateUser(entity);

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = new User();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
            {
                user = cachedItems?.FirstOrDefault(x => x.Id == email);
                if (user != null)
                    return user;
            }

            var entity = await _userRepository.GetAsync(x => x.Id == email);
            if (entity == null)
                return null;

            await SetCache();

            user = UserFactory.CreateUser(entity);
            return user;
        }

        public async Task<IEnumerable<User>?> GetUsersAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<User>? cachedItems))
                return cachedItems;

            var users = await SetCache();
            return users;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserForm userForm)
        {
            if (userForm == null)
                return false;

            var user = await _userRepository.GetAsync(x => x.Id == userForm.Id);
            if (user == null)
                return false;

            UserFactory.UpdateUser(user, userForm);

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (!currentRoles.Contains(userForm.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

                await _userManager.AddToRoleAsync(user, userForm.Role);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _cache.Remove(_cacheKey_All);
            }

            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            var user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                _cache.Remove(_cacheKey_All);

            return result.Succeeded;
        }

        public async Task<IEnumerable<User>> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entities = await _userRepository.GetAllAsync();

            var users = new List<User>();
            foreach (var entity in entities)
            {
                var roles = await _userManager.GetRolesAsync(entity);
                var user = UserFactory.CreateUser(entity);
                user.Role = roles.FirstOrDefault() ?? "User";
                users.Add(user);
            }

            users = users.OrderBy(x => x.FirstName).ToList();
            _cache.Set(_cacheKey_All, users, TimeSpan.FromMinutes(10));

            return users;
        }
    }
}
