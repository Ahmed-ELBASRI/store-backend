using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        private readonly IPaiementservice _paiementService;
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public PaiementController(StoreDbContext context, IPaiementservice paiementService, IMapper mapper)
        {
            _paiementService = paiementService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaiementResponsedto>>> GetPaiements()
        {
            var paiements = await _context.Paiements.ToListAsync();
            var paiementDtos = _mapper.Map<IEnumerable<PaiementResponsedto>>(paiements);
            return Ok(paiementDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaiementResponsedto>> GetPaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);

            if (paiement == null)
            {
                return NotFound();
            }

            var paiementDto = _mapper.Map<PaiementResponsedto>(paiement);
            return Ok(paiementDto);
        }

        [HttpPost]
        public async Task<ActionResult<PaiementResponsedto>> CreatePaiement(PaiementRequestdto paiementRequestDto)
        {
            var existingPaiement = await _context.Paiements.FirstOrDefaultAsync(p => p.CommandeId == paiementRequestDto.CommandeId);
            if (existingPaiement != null)
            {
                return Conflict("A payment with the same CommandeId already exists.");
            }

            var paiement = _mapper.Map<Paiement>(paiementRequestDto);
            _context.Paiements.Add(paiement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaiement), new { id = paiement.IdPaiement }, _mapper.Map<PaiementResponsedto>(paiement));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<PaiementResponsedto>> UpdatePaiement(int id, PaiementRequestdto paiementRequestdto)
        {
            try
            {
                var paiement = await _paiementService.GetPaiement(id);
                if (paiement == null)
                {
                    return NotFound();
                }
                _mapper.Map(paiementRequestdto, paiement);
                await _paiementService.UpdatePaiement(id, paiement);
                var updatedPaiementdto = _mapper.Map(paiementRequestdto, paiement);
                return Ok(updatedPaiementdto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaiement(int id)
        {
            var paiement = await _context.Paiements.FindAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }

            _context.Paiements.Remove(paiement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
