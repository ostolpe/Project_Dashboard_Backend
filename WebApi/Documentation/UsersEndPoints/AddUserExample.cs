using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class AddUserFormExample : IExamplesProvider<AddUserForm>
    {
        public AddUserForm GetExamples()
        {
            return new AddUserForm
            {
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
