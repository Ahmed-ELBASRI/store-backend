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
    [Route("api/[controller]")]
    [ApiController]
    public class RetoursController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IRetourservice _retourService;
        private readonly IMapper _mapper;


        public RetoursController(StoreDbContext context, IMapper mapper, IRetourservice retourService)
        {
            _context = context;
            _mapper = mapper;
            _retourService = retourService;
        }



        // GET: api/Retours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RetourResponsedto>>> GetRetours()
        {
            var retours = await _context.Retours.ToListAsync();
            var retourDtos = _mapper.Map<IEnumerable<RetourResponsedto>>(retours);
            return Ok(retourDtos);
        }

        // GET: api/Retours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RetourResponsedto>> GetRetour(int id)
        {
            var retour = await _context.Retours.FindAsync(id);

            if (retour == null)
            {
                return NotFound();
            }

            var retourDto = _mapper.Map<RetourResponsedto>(retour);
            return Ok(retourDto);
        }

        // PUT: api/Retours/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RetourResponsedto>> UpdateRetour(int id, RetourRequestdto retourRequestdto)
        {
            try
            {
                var retour = await _retourService.GetRetour(id); 
                if (retour == null)
                {
                    return NotFound(); 
                }
                _mapper.Map(retourRequestdto, retour);
                await _retourService.UpdateRetour(id, retour);
                var updatedRetourDto = _mapper.Map<RetourResponsedto>(retour);
                return Ok(updatedRetourDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }





        // POST: api/Retours
        [HttpPost]
        public async Task<ActionResult<RetourResponsedto>> PostRetour(RetourRequestdto retourDto)
        {
            var retour = _mapper.Map<Retour>(retourDto);
            _context.Retours.Add(retour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRetour", new { id = retour.Id }, _mapper.Map<RetourResponsedto>(retour));
        }

        // DELETE: api/Retours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetour(int id)
        {
            var retour = await _context.Retours.FindAsync(id);
            if (retour == null)
            {
                return NotFound();
            }

            _context.Retours.Remove(retour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool RetourExists(int id)
        //{
        //    return _context.Retours.Any(e => e.Id è== id);
        //}
    }
}
