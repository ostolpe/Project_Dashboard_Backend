namespace WebApi.Documentation.StatusesEndPoint
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class StatusInvalidIdExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() =>
            new("ID must be either 1 or 2.");
    }
}
