using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpPost]
        public async Task<IActionResult> Create(AddClientForm clientForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(clientForm);

            var result = await _clientService.CreateClientAsync(clientForm);

            return result ? Ok(result) : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clientService.GetClientsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _clientService.GetClientByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClientForm clientForm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _clientService.UpdateClientAsync(clientForm);

            return result ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _clientService.DeleteClientAsync(id);

            return result ? Ok(result) : NotFound();
        }
    }
}
