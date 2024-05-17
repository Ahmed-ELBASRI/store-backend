using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;

namespace store.Controllers
{ 
 [Route("api/[controller]")]
[ApiController]
public class PhotoProduitsController : ControllerBase
{
        private readonly IPhotoProduitService _photoProduitService;
        private readonly IMapper _mapper;

        public PhotoProduitsController(IPhotoProduitService photoProduitService, IMapper mapper)
        {
            _photoProduitService = photoProduitService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoProduitResponseDto>> GetPhotoProduitById(int id)
        {
            try
            {
                var photoProduit = await _photoProduitService.GetPhotoProduitByIdAsync(id);
                return Ok(photoProduit);
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

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<PhotoProduitResponseDto>>> GetPhotosByProductId(int productId)
        {
            try
            {
                var photos = await _photoProduitService.GetPhotosByProductIdAsync(productId);
                return Ok(photos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PhotoProduitResponseDto>> UploadPhoto([FromBody] PhotoProduitRequestDto photoProduitRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var photoProduit = _mapper.Map<PhotoProduit>(photoProduitRequestDto);
                var photoProduitResponseDto = await _photoProduitService.UploadPhotoAsync(photoProduit.ProductId, photoProduit.UrlImage);
                return CreatedAtAction(nameof(UploadPhoto), new { id = photoProduitResponseDto.PhotoProduitId }, photoProduitResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoUrl(int id, PhotoProduit newphotoProduit)
        {
            try
            {
                if (newphotoProduit.ProductId == null)
                {
                    return BadRequest("ProductId is required.");
                }

                var photoProduit = _mapper.Map<PhotoProduit>(newphotoProduit);
                if (photoProduit == null)
                {
                    return NotFound();
                }

                await _photoProduitService.UpdatePhotoUrlAsync(id, photoProduit);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoProduit(int id)
        {
            try
            {
                await _photoProduitService.DeletePhotoProduitAsync(id);
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