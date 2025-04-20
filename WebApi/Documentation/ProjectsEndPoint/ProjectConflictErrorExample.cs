namespace WebApi.Documentation.ProjectsEndPoint
{
    using Business.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class ProjectConflictErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new ErrorMessage("Project already exists.");
    }
}
