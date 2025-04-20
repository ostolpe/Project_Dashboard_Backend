namespace WebApi.Documentation.StatusesEndPoint
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class StatusNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() =>
            new("Status not found.");
    }
}
