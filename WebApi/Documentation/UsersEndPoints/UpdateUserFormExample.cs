using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Documentation.UsersEndPoints
{
    public class UpdateUserFormExample : IExamplesProvider<UpdateUserForm>
    {
        public UpdateUserForm GetExamples()
        {
            return new UpdateUserForm
            {
                Id = "25204ae2-25e2-4e7d-8ea8-49c30bee3e33",
                FirstName = "Oliver",
                LastName = "Stolpe",
                Email = "oliver@domain.com",
                PhoneNumber = "0123456789",
                StreetName = "Vägen 1",
                PostalCode = "123 45",
                City = "Staden",
                JobTitle = "Junior developer",
                ImageUrl = "image.com",
                Role = "User"
            };
        }
    }
}
