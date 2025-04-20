using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation.ClientEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Extensions.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseAdminApiKey]
    [Authorize(Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClientsController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;


        [HttpPost]

        [Consumes("multipart/form-data")]
        [SwaggerOperation(
           Summary = "Create a new Client",
           Description = "Only Admins can create clients. This will require an API‑key 'X‑ADM‑API‑KEY' in the header."
       )]
        [SwaggerRequestExample(typeof(AddClientForm), typeof(AddClientFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Client was created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Client request contains invalid data.", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ClientValidationErrorExample))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Client already exists.", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ClientConflictErrorExample))]
        public async Task<IActionResult> Create([FromForm] AddClientForm clientForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorMessage("Validation failed"));

            var created = await _clientService.CreateClientAsync(clientForm);
            return created
                ? Ok(created)
                : Conflict(new ErrorMessage("Client already exists."));
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get all clients", Description = "Retrieves a list of all clients.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all clients", typeof(IEnumerable<Client>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clientService.GetClientsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a client by ID", Description = "Retrieves a client by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the client", typeof(Client))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID", typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ClientNotFoundExample))]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new ErrorMessage("ID must be provided."));

            var client = await _clientService.GetClientByIdAsync(id);
            return client is not null
                ? Ok(client)
                : NotFound(new ErrorMessage($"Client not found with ID '{id}'."));
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update a client", Description = "Updates an existing client with the provided data.")]
        [SwaggerRequestExample(typeof(UpdateClientForm), typeof(UpdateClientFormExample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Client updated successfully", typeof(Client))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ClientValidationErrorExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ClientNotFoundExample))]
        public async Task<IActionResult> Update([FromForm] UpdateClientForm clientForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorMessage("Validation failed"));

            var updated = await _clientService.UpdateClientAsync(clientForm);
            return updated
                ? Ok(updated)
                : NotFound(new ErrorMessage("Client not found."));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a client", Description = "Deletes a client by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Client successfully deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID", typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ClientNotFoundExample))]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new ErrorMessage("ID must be provided."));

            var deleted = await _clientService.DeleteClientAsync(id);
            return deleted
                ? Ok()
                : NotFound(new ErrorMessage("Client not found."));
        }
    }
}
