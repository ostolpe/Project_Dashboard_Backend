using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{

    public class AddClientFormExample : IExamplesProvider<AddClientForm>
    {
        public AddClientForm GetExamples()
        {
            return new AddClientForm
            {
                Name = "Acme Corporation",
                Email = "contact@acme.com",
                Phone = "012345689",
                ImageUrl = "ImageURL",
                StreetName = "Vägen 1",
                PostalCode = "123 45",
                City = "Staden",
                BillingReference = "Oliver Stolpe"
            };
        }
    }
}
