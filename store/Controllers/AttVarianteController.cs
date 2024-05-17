using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttVarianteController : ControllerBase
    {
        private readonly IAttVarianteService _attVarianteService;
        private readonly IMapper _mapper;

        public AttVarianteController(IAttVarianteService attVarianteService, IMapper mapper)
        {
            _attVarianteService = attVarianteService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttVarianteResponseDto>> GetAttVarianteById(int id)
        {
            try
            {
                var attVariante = await _attVarianteService.GetAttVarianteByIdAsync(id);
                if (attVariante == null)
                {
                    return NotFound();
                }
                var attVarianteDto = _mapper.Map<AttVarianteResponseDto>(attVariante);
                return Ok(attVarianteDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttVarianteResponseDto>>> GetAllAttVariantes()
        {
            try
            {
                var attVariantes = await _attVarianteService.GetAllAttVariantesAsync();
                var attVariantesDto = _mapper.Map<IEnumerable<AttVarianteResponseDto>>(attVariantes);
                return Ok(attVariantesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AttVarianteResponseDto>> CreateAttVariante(AttVarianteRequestDto attVarianteRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var attVariante = _mapper.Map<Att_Variante>(attVarianteRequestDto);
                var createdAttVariante = await _attVarianteService.CreateAttVarianteAsync(attVariante);
                var createdAttVarianteDto = _mapper.Map<AttVarianteResponseDto>(createdAttVariante);
                return CreatedAtAction(nameof(GetAttVarianteById), new { id = createdAttVariante.Id }, createdAttVarianteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AttVarianteResponseDto>> UpdateAttVariante(int id, AttVarianteRequestDto attVarianteRequestDto)
        {
            try
            {
                var attVariante = _mapper.Map<Att_Variante>(attVarianteRequestDto);
                var updatedAttVariante = await _attVarianteService.UpdateAttVarianteAsync(id, attVariante);
                var updatedAttVarianteDto = _mapper.Map<AttVarianteResponseDto>(updatedAttVariante);
                return Ok(updatedAttVarianteDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttVariante(int id)
        {
            try
            {
                await _attVarianteService.DeleteAttVarianteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
