using Business.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class UserNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples()
        {
            return new ErrorMessage("User not found with the specified ID.");
        }
    }
}
