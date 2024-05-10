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
    public class ProductController : ControllerBase
    {
        private readonly IProduitService _produitService;
        private readonly IMapper _mapper;


        public ProductController(IProduitService produitService, IMapper mapper)
        {
            _produitService = produitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProudcts()
        {
            var proudcts =  _mapper.Map<IEnumerable<ProductResponseDto>>(await _produitService.GetAllProducts());
            return Ok(proudcts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProudct(int id)
        {

            var proudcts = _mapper.Map<ProductResponseDto>(await _produitService.GetProductById(id));
                if(proudcts==null)
                {
                   return NotFound();
                }

            return Ok(proudcts);
        }

 
        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductRequestDto requestDto)
        {
            var proudct = _mapper.Map<Product>(requestDto);
            await _produitService.AddProduct(proudct);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var proudcts = _mapper.Map<ProductResponseDto>(await _produitService.GetProductById(id));
            if (proudcts == null)
            {
                return NotFound();
            }




            await _produitService.DeleteProduct(id);
          return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductRequestDto requestDto)
        {
            var proudcts = _mapper.Map<ProductResponseDto>(await _produitService.GetProductById(id));
            if (proudcts == null)
            {
                return NotFound();
            }
            var proudct = _mapper.Map<Product>(requestDto);
            await _produitService.UpdateProduct(id, proudct);
            return Ok();
        }


    }
  
    }



 