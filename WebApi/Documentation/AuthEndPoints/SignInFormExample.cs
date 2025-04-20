namespace WebApi.Documentation.AuthEndPoints
{
    using Business.Dtos;
    using Swashbuckle.AspNetCore.Filters;

    public class SignInFormExample : IExamplesProvider<SignInForm>
    {
        public SignInForm GetExamples() => new()
        {
            Email = "user@example.com",
            Password = "P@ssw0rd!"
        };
    }
}
