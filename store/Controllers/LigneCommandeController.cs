using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LigneCommandeController : ControllerBase
    {
        private readonly ILigneCommandeService _lignecommandService;
        private readonly IMapper _mapper;

        public LigneCommandeController(ILigneCommandeService lignecommandService, IMapper mapper)
        {
            _lignecommandService = lignecommandService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigneCommandeResponseDto>>> GetAllLigneCommandes()
        {
            try
            {
                var lignecommande = await _lignecommandService.GetAllLigneCommandes();
                var lignecommandDto = _mapper.Map<IEnumerable<LigneCommandeResponseDto>>(lignecommande);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var json = JsonSerializer.Serialize(lignecommandDto, options);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Lignecommande");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LigneCommandeResponseDto>> GetLigneCommandeById(int id)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var lignecommande = _mapper.Map<LigneCommandeResponseDto>(await _lignecommandService.GetLigneCommandeById(id));
                if (lignecommande == null)
                {
                    return NotFound();
                }

                return Ok(lignecommande);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Lignecommande");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLigneCommande(LigneCommandeRequestDto requestDto)
        {
            try
            {
                var lignecommande = _mapper.Map<LigneCommande>(requestDto);
                await _lignecommandService.AddLigneCommande(lignecommande);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating LigneCommande");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLigneCommande(int id)
        {

            var Lignecommande = _mapper.Map<LigneCommandeResponseDto>(await _lignecommandService.GetLigneCommandeById(id));
            if (Lignecommande == null)
            {
                return NotFound();
            }
            await _lignecommandService.DeleteLigneCommande(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLigneCommande(int id, LigneCommandeRequestDto newLigneCommande)
        {

            var lignecommande = _mapper.Map<LigneCommande>(newLigneCommande);
            if (lignecommande == null)
            {
                return NotFound();
            }
            await _lignecommandService.UpdateLigneCommande(id, lignecommande);
            return Ok();
        }
    }
}
