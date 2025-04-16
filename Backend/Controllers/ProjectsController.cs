using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        //Create
        [HttpPost]
        public async Task<IActionResult> Create(AddProjectForm projectForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(projectForm);

            var result = await _projectService.CreateProjectAsync(projectForm);

            return result ? Ok(result) : BadRequest();
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _projectService.GetProjectsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
                return BadRequest();

            var result = await _projectService.GetProjectByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectForm projectForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(projectForm);

            var result = await _projectService.UpdateProjectAsync(projectForm);
            return result ? Ok(result) : NotFound();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return BadRequest();

            var result = await _projectService.DeleteProjectAsync(id);

            return result ? Ok() : NotFound();
        }
    }
}
