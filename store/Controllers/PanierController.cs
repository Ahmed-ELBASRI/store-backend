using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using store.Services.Implementation;

namespace store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanierController : ControllerBase
    {

        private readonly IPanierService _panierService;
        private readonly IMapper _mapper;

        public PanierController(IPanierService panierService, IMapper mapper)
        {
            _panierService = panierService;
            _mapper = mapper;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<PanierResponseDto>> GetPanier(int id, [FromBody] JsonElement data)
        {
            try
            {

                if (data.TryGetProperty("ConnectionString", out JsonElement connectionStringElement))
                {
                    string connectionString = connectionStringElement.GetString();
                    var connectionString2 = $"Data Source=.\\SQLEXPRESS;Initial Catalog={connectionString};Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;";
                    var panier = await _panierService.GetPanier(id,connectionString2);
                    if (panier == null)
                    {
                        return NotFound();
                    }
                    var panierDto = _mapper.Map<PanierResponseDto>(panier);
                    return Ok(panierDto);
                }
                return BadRequest();
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PanierResponseDto>>> GetAllPaniers()
        {
            try
            {
                var paniers = await _panierService.GetAllPaniers();
                var panierDtos = _mapper.Map<IEnumerable<PanierResponseDto>>(paniers);
                return Ok(panierDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("client/{clientId}")]
        public async Task<ActionResult<PanierResponseDto>> GetPanierByClientId(int clientId, [FromBody] JsonElement data)
        {
            try
            {
               
                if (data.TryGetProperty("ConnectionString", out JsonElement connectionStringElement))
                {
                    string connectionString = connectionStringElement.GetString();
                    var connectionString2 = $"Data Source=.\\SQLEXPRESS;Initial Catalog={connectionString};Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true;";
                    var panier = await _panierService.GetPanierByClientId(clientId,connectionString2);
                    if (panier == null)
                    {
                        return NotFound();
                    }
                    var panierDto = _mapper.Map<PanierResponseDto>(panier);
                    return Ok(panierDto);

                   

                 
                }
                else
                {
                    return BadRequest(new { message = "ConnectionString property is missing" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving commands");
            }
         
            
        }

        [HttpPost]
        public async Task<IActionResult> AddPanier(PanierRequestDto panierRequestDto)
        {
            try
            {
                var panier = _mapper.Map<Panier>(panierRequestDto);
                var addedPanier = await _panierService.AddPanier(panier);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "An error occurred while saving the entity changes. See the inner exception for details.");
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdatePanier(int id, PanierRequestDto panierRequestDto)
        //{
        //    try
        //    {
        //        var existingPanier = await _panierService.GetPanier(id);
        //        if (existingPanier == null)
        //        {
        //            return NotFound();
        //        }

        //        var panier = _mapper.Map(panierRequestDto, existingPanier);
        //        await _panierService.UpdatePanier(id, panier);
        //        return NoContent();
        //    }
        //    catch (ArgumentException)
        //    {
        //        return BadRequest("ID mismatch");
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound("Panier not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            try
            {
                await _panierService.DeletePanier(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Panier not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
