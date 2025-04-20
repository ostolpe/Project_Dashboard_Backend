using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples()
        {
            return new ErrorMessage
            {
                Message = "One or more validation errors occurred.",

            };
        }
    }
}
