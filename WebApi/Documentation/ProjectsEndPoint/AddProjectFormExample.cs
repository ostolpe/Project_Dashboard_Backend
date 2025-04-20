namespace WebApi.Documentation.ProjectsEndPoint
{
    using Business.Dtos;
    using Swashbuckle.AspNetCore.Filters;

    public class AddProjectFormExample : IExamplesProvider<AddProjectForm>
    {
        public AddProjectForm GetExamples() => new AddProjectForm
        {
            //ImageUrl = "image.com",
            Name = "Website Redesign",
            Description = "Full redesign of corporate website.",
            Budget = 9999999,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            UserId = "3a46a079-ed2f-458b-b654-81ffadf34f72",
            ClientId = "64719c72-9dbc-4770-956c-5e75fccc42e6"
        };
    }
}
