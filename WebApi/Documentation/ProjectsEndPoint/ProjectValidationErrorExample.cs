namespace WebApi.Documentation.ProjectsEndPoint
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class ProjectValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new ErrorMessage("Validation failed");
    }
}
