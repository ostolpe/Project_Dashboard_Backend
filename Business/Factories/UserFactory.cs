using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class UserFactory
    {
        public static UserEntity CreateUserEntity(AddUserForm userForm)
        {
            return new UserEntity
            {
                UserName = userForm.Email,
                FirstName = userForm.FirstName,
                LastName = userForm.LastName,
                Email = userForm.Email,
                JobTitle = userForm.JobTitle,
                ImageUrl = userForm.ImageUrl,
                Address = new UserAddressEntity
                {
                    StreetName = userForm.StreetName,
                    PostalCode = userForm.PostalCode,
                    City = userForm.City
                }
            };
        }

        public static User CreateUser(UserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email!,
                PhoneNumber = userEntity.PhoneNumber,
                StreetName = userEntity.Address?.StreetName,
                PostalCode = userEntity.Address?.PostalCode,
                City = userEntity.Address?.City,
                JobTitle = userEntity.JobTitle,
                ImageUrl = userEntity.ImageUrl
            };
        }

        public static void UpdateUser(UserEntity user, UpdateUserForm userForm)
        {
            if (user.FirstName != userForm.FirstName)
                user.FirstName = userForm.FirstName;

            if (user.LastName != userForm.LastName)
                user.LastName = userForm.LastName;

            if (user.Email != userForm.Email)
                user.Email = userForm.Email;

            if (user.PhoneNumber != userForm.PhoneNumber)
                user.PhoneNumber = userForm.PhoneNumber;

            if (user.Address?.StreetName != userForm.StreetName)
                user.Address!.StreetName = userForm.StreetName;

            if (user.Address?.PostalCode != userForm.PostalCode)
                user.Address!.PostalCode = userForm.PostalCode;

            if (user.Address?.City != userForm.City)
                user.Address!.City = userForm.City;

            if (user.JobTitle != userForm.JobTitle)
                user.JobTitle = userForm.JobTitle;

            if (user.ImageUrl != userForm.ImageUrl)
                user.ImageUrl = userForm.ImageUrl;
        }
    }
}
