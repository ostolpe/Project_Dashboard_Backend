namespace WebApi.Documentation.ProjectsEndPoint
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class ProjectNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new ErrorMessage("Project not found.");
    }
}
