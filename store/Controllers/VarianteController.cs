using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using System.Text.Json;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VarianteController : ControllerBase
    {
        private readonly IVarianteService _varianteService;
        private readonly IMapper _mapper;

        public VarianteController(IVarianteService varianteService, IMapper mapper)
        {
            _varianteService = varianteService;
            _mapper = mapper;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<VarianteResponseDto>> GetVarianteById(int id, [FromBody] JsonElement data)
        {
            try
            {
                if (data.TryGetProperty("ConnectionString", out JsonElement connectionStringElement))
                {
                    string connectionString = connectionStringElement.GetString();
                    var connectionString2 = $"Data Source=.\\SQLEXPRESS;Initial Catalog={connectionString};Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;";
                    var variante = await _varianteService.GetVarianteByIdAsync(id,connectionString2);
                    if (variante == null)
                    {
                        return NotFound();
                    }
                    var varianteDto = _mapper.Map<VarianteResponseDto>(variante);
                    return Ok(varianteDto);
                }
                return BadRequest();
               
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
        public async Task<ActionResult<IEnumerable<VarianteResponseDto>>> GetAllVariantes()
        {
            try
            {
                var variantes = await _varianteService.GetAllVariantesAsync();
                var variantesDto = _mapper.Map<IEnumerable<VarianteResponseDto>>(variantes);
                return Ok(variantesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VarianteResponseDto>> CreateVariante(VarianteRequestDto varianteRequestDto)
        {
            try
            {
                var variante = _mapper.Map<Variante>(varianteRequestDto);
                var createdVariante = await _varianteService.CreateVarianteAsync(variante);
                var createdVarianteDto = _mapper.Map<VarianteResponseDto>(createdVariante);
                return CreatedAtAction(nameof(GetVarianteById), new { id = createdVariante.IdVariante }, createdVarianteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<VarianteResponseDto>> UpdateVariante(int id, VarianteRequestDto varianteRequestDto)
        //{
        //    try
        //    {
        //        var variante = _mapper.Map<Variante>(varianteRequestDto);
        //        var updatedVariante = await _varianteService.UpdateVarianteAsync(id, variante);
        //        var updatedVarianteDto = _mapper.Map<VarianteResponseDto>(updatedVariante);
        //        return Ok(updatedVarianteDto);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteVariante(int id)
        //{
        //    try
        //    {
        //        await _varianteService.DeleteVarianteAsync(id);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

        // Other action methods (UpdateVariante, DeleteVariante) remain the same
    }
}
