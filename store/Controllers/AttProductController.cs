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
    public class AttProductController : ControllerBase
    {
        private readonly IAtt_ProduitService _Att_ProduitService;
        private readonly IMapper _mapper;

        public AttProductController(IAtt_ProduitService Att_ProduitService, IMapper mapper)
        {
            _Att_ProduitService = Att_ProduitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Att_ProduitResponseDto>>> GetProducts()
        {
            var attProducts = await _Att_ProduitService.GetAllAtt_Produit();
            var attProductsDto = _mapper.Map<IEnumerable<Att_ProduitResponseDto>>(attProducts);
            return Ok(attProductsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Att_ProduitResponseDto>> GetProduct(int id)
        {
            var attProduct = await _Att_ProduitService.GetAtt_Produit(id);
            if (attProduct == null)
            {
                return NotFound();
            }
            var attProductDto = _mapper.Map<Att_ProduitResponseDto>(attProduct);
            return Ok(attProductDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Att_ProduitRequestDto requestDto)
        {
            var attProduct = _mapper.Map<Att_Produit>(requestDto);
            await _Att_ProduitService.AddAtt_Produit(attProduct);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var attProduct = await _Att_ProduitService.GetAtt_Produit(id);
            if (attProduct == null)
            {
                return NotFound();
            }

            await _Att_ProduitService.DeleteAtt_Produit(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Att_ProduitRequestDto requestDto)
        {
            var attProduct = await _Att_ProduitService.GetAtt_Produit(id);
            if (attProduct == null)
            {
                return NotFound();
            }

            var updatedProduct = _mapper.Map<Att_Produit>(requestDto);
            await _Att_ProduitService.UpdateAtt_Produit(id, updatedProduct);
            return Ok();
        }
    }
}




