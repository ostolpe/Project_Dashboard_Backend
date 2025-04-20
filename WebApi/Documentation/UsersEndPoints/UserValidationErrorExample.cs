using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class UserValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples()
        {
            return new ErrorMessage("Validation failed: Email and Password are required.");
        }
    }
}
