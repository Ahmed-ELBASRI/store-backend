using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;
using System.Text.Json;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoVarianteController : ControllerBase
    {
        private readonly IPhotoVarianteService _photoVarianteService;
        private readonly IMapper _mapper;

        public PhotoVarianteController(IPhotoVarianteService photoVarianteService, IMapper mapper)
        {
            _photoVarianteService = photoVarianteService;
            _mapper = mapper;
        }

        [HttpPost("{varianteId}")]
        public async Task<ActionResult<IEnumerable<PhotoVarianteResponseDto>>> GetPhotosByVarianteId(int varianteId,[FromBody] JsonElement data)
        {
            try
            {
                if (data.TryGetProperty("ConnectionString", out JsonElement connectionStringElement))
                {
                    string connectionString = connectionStringElement.GetString();
                    var connectionString2 = $"Data Source=.\\SQLEXPRESS;Initial Catalog={connectionString};Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;";
                    var photos = await _photoVarianteService.GetPhotosByVarianteIdAsync(varianteId,connectionString2);
                    var photoDtos = _mapper.Map<IEnumerable<PhotoVarianteResponseDto>>(photos);
                    return Ok(photoDtos);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<PhotoVarianteRequestDto>> GetPhotoVarianteById(int id)
        {
            try
            {
                var photoVariante = await _photoVarianteService.GetPhotosByPhotoIdAsync(id);
                var photoVarianteDto = _mapper.Map<PhotoVarianteResponseDto>(photoVariante);
                return Ok(photoVarianteDto);
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


        //[HttpPost("{varianteId}")]
        //public async Task<ActionResult<PhotoVarianteResponseDto>> UploadPhoto(int varianteId, [FromBody] PhotoVarianteRequestDto photoVarianteRequestDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // If model state is invalid, return bad request with model state errors
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var photoVariante = _mapper.Map<PhotoVariante>(photoVarianteRequestDto);
        //        var uploadedPhoto = await _photoVarianteService.UploadPhotoAsync(varianteId, photoVariante.UrlImage);
        //        var uploadedPhotoDto = _mapper.Map<PhotoVarianteResponseDto>(uploadedPhoto);
        //        return CreatedAtAction(nameof(GetPhotosByVarianteId), new { varianteId }, uploadedPhotoDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

        [HttpDelete("{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            try
            {
                await _photoVarianteService.DeletePhotoAsync(photoId);
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
        [HttpPut("{photoId}")]
        public async Task<ActionResult> UpdatePhotoUrl(int photoId,PhotoVariante newPhotoVariante)
        {
            try
            {
                var photoVariante = _mapper.Map<PhotoVariante>(newPhotoVariante);
                if (photoVariante == null)
                {
                    return NotFound();
                }
                await _photoVarianteService.UpdatePhotoUrlAsync(photoId, photoVariante);
                return Ok();
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
