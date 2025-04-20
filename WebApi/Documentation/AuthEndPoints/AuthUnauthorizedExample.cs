namespace WebApi.Documentation.AuthEndPoints
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class AuthUnauthorizedExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new("Invalid email or password.");
    }
}
