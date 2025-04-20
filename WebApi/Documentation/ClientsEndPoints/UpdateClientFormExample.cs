using Business.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class UpdateClientFormExample : IExamplesProvider<UpdateClientForm>
    {
        public UpdateClientForm GetExamples()
        {
            return new UpdateClientForm
            {
                Id = "12345",
                Name = "Acme Corporation International",
                Email = "support@acme.com",
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
