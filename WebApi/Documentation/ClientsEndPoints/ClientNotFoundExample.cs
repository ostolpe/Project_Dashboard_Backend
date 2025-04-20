using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples()
        {
            return new ErrorMessage
            {
                Message = "Client not found with ID '12345'."
            };
        }
    }
}
