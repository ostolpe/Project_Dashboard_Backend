namespace WebApi.Documentation.AuthEndPoints
{
    using Business.Dtos;
    using Swashbuckle.AspNetCore.Filters;

    public class SignUpFormExample : IExamplesProvider<SignUpForm>
    {
        public SignUpForm GetExamples() => new()
        {
            FirstName = "Oliver",
            LastName = "Stolpe",
            Email = "user@example.com",
            Password = "P@ssw0rd!",
        };
    }
}
