using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientConflictErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples()
        {
            return new ErrorMessage
            {
                Message = "A client with the specified identifier already exists."
            };
        }
    }
}
