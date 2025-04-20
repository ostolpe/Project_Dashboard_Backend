using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Documentation.ProjectsEndPoint;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new project",
            Description = "Creates a new project with the provided data."
        )]
        [SwaggerRequestExample(typeof(AddProjectForm), typeof(AddProjectFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Project was created successfully.", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Project request contains invalid data.", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProjectValidationErrorExample))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Project already exists.", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProjectConflictErrorExample))]
        public async Task<IActionResult> Create(AddProjectForm projectForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(projectForm);

            var result = await _projectService.CreateProjectAsync(projectForm);

            return result ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all projects", Description = "Retrieves a list of all projects.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all projects", typeof(IEnumerable<Project>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _projectService.GetProjectsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a project by ID", Description = "Retrieves a project by its unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the project", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID", typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProjectNotFoundExample))]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _projectService.GetProjectByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update a project", Description = "Updates an existing project with the provided data.")]
        [SwaggerRequestExample(typeof(UpdateProjectForm), typeof(UpdateProjectFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Project updated successfully", typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProjectValidationErrorExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProjectNotFoundExample))]
        public async Task<IActionResult> Update(UpdateProjectForm projectForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(projectForm);

            var result = await _projectService.UpdateProjectAsync(projectForm);
            return result ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a project", Description = "Deletes a project by its unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Project successfully deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID", typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProjectNotFoundExample))]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return BadRequest();

            var result = await _projectService.DeleteProjectAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}
