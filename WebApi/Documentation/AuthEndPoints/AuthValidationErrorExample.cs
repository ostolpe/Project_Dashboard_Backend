namespace WebApi.Documentation.AuthEndPoints
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class AuthValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new("One or more validation errors occurred.");
    }
}
