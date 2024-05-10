using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;


namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientservice _clientService;
        private readonly IMapper _mapper;
       // private readonly IProduitService _produitService;

        public ClientController(IClientservice clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
           /// _produitService = produitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientResponsedto>>> GetClients()
        {
            try
            {
                var clients = await _clientService.GetAllClient();
                var clientDtos = _mapper.Map<IEnumerable<ClientResponsedto>>(clients);
                return Ok(clientDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving clients");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponsedto>> GetClient(int id)
        {
            try
            {
                var client = await _clientService.GetClient(id);
                if (client == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ClientResponsedto>(client));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving client");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientRequestdto requestDto)
        {
            try
            {
                var client = _mapper.Map<Client>(requestDto);
                await _clientService.AddClient(client);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Client");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DescativateClient(int id)
        {
            try
            {
                await _clientService.DesactivateClient(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Desactiving product");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientRequestdto newClient)
        {
            try
            {
                var client = _mapper.Map<Client>(newClient);
                await _clientService.UpdateClient(id, client);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating Client");
            }
        }
    }
}