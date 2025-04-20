using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Documentation.StatusesEndPoint;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class StatusesController(IStatusService statusService) : ControllerBase
    {
        private readonly IStatusService _statusService = statusService;

        [HttpGet]
        [SwaggerOperation(Summary = "Get all statuses", Description = "Retrieves a list of all statuses.")]
        [SwaggerResponse(200, "Returns all statuses", typeof(IEnumerable<Status>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _statusService.GetStatusesAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get status by ID", Description = "Retrieves a status by its unique ID. Only IDs 1 and 2 are valid.")]
        [SwaggerResponse(200, "Returns the status", typeof(Status))]
        [SwaggerResponse(400, "Invalid ID", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(StatusInvalidIdExample))]
        [SwaggerResponse(404, "Status not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(StatusNotFoundExample))]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1 || id > 2)
                return BadRequest();

            var result = await _statusService.GetStatusByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }
    }
}
