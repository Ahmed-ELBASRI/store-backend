using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LignePanierController : ControllerBase
    {
        private readonly ILignePanierService _lignePanierService;
        private readonly IMapper _mapper;

        public LignePanierController(ILignePanierService lignePanierService, IMapper mapper)
        {
            _lignePanierService = lignePanierService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LignePanierResponseDto>> GetLignePanier(int id)
        {
            try
            {
                var lignePanier = await _lignePanierService.GetLignePanier(id);
                if (lignePanier == null)
                {
                    return NotFound();
                }
                var lignePanierDto = _mapper.Map<LignePanierResponseDto>(lignePanier);
                return Ok(lignePanierDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LignePanierResponseDto>>> GetAllLignePaniers()
        {
            try
            {
                var lignePaniers = await _lignePanierService.GetAllLignePaniers();
                var lignePanierDtos = _mapper.Map<IEnumerable<LignePanierResponseDto>>(lignePaniers);
                return Ok(lignePanierDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }       
        [HttpPost]
        public async Task<IActionResult> AddLignePanier(LignePanierRequestDto lignePanierRequestDto)
        {
            try
            {
                var lignepanier = _mapper.Map<LignePanier>(lignePanierRequestDto);
                var addedLignePanier = await _lignePanierService.AddLignePanier(lignepanier);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "An error occurred while saving the entity changes. See the inner exception for details.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLignePanier(int id, LignePanier lignePanier)
        {
            try
            {
                await _lignePanierService.UpdateLignePanier(id, lignePanier);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("ID mismatch");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("LignePanier not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLignePanier(int id)
        {
            try
            {
                await _lignePanierService.DeleteLignePanier(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("LignePanier not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("lignespanier/panier/{panierId}")]
        public async Task<ActionResult<List<LignePanierResponseDto>>> GetLignesPanierByPanierId(int panierId)
        {
            try
            {
                var lignesPanier = await _lignePanierService.GetLignesPanierByPanierId(panierId);

                if (lignesPanier == null)
                {
                    return NotFound();
                }

                var lignesPanierDtoList = _mapper.Map<List<LignePanierResponseDto>>(lignesPanier);

                return Ok(lignesPanierDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
