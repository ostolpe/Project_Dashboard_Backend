namespace WebApi.Documentation.ProjectsEndPoint
{
    using Business.Dtos;
    using Swashbuckle.AspNetCore.Filters;

    public class UpdateProjectFormExample : IExamplesProvider<UpdateProjectForm>
    {
        public UpdateProjectForm GetExamples() => new UpdateProjectForm
        {
            Id = "proj-123",
            ImageUrl = "image.com",
            Name = "Website Redesign Phase 2",
            Description = "Add e-commerce integration.",
            Budget = 99999,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(2),
            UserId = "3a46a079-ed2f-458b-b654-81ffadf34f72",
            ClientId = "64719c72-9dbc-4770-956c-5e75fccc42e6",
            StatusId = 1
        };
    }
}
