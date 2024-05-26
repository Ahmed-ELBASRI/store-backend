using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Contract;
using store.Settings;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        private readonly IPaiementservice _paiementService;
        private readonly IMapper _mapper;
        private readonly StripeSettings _stripeSettings;

        public PaiementController(IPaiementservice paiementService, IMapper mapper, IOptions<StripeSettings> stripeSettings)
        {
            _paiementService = paiementService;
            _mapper = mapper;
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaiementResponsedto>>> GetPaiements()
        {
            var paiements = await _paiementService.GetPaiements();
            var paiementDtos = _mapper.Map<IEnumerable<PaiementResponsedto>>(paiements);
            return Ok(paiementDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaiementResponsedto>> GetPaiement(int id)
        {
            try
            {
                var paiement = await _paiementService.GetPaiement(id);
                var paiementDto = _mapper.Map<PaiementResponsedto>(paiement);
                return Ok(paiementDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaiementResponsedto>> CreatePaiement(PaiementRequestdto paiementRequestDto)
        {
            var paiement = _mapper.Map<Paiement>(paiementRequestDto);
            // Assuming you have a mechanism to set PaymentIntentId elsewhere
            paiement.PaymentIntentId = paiement.PaymentIntentId ?? Guid.NewGuid().ToString(); // Ensure PaymentIntentId is not null
            await _paiementService.CreatePaiement(paiement);
            var paiementDto = _mapper.Map<PaiementResponsedto>(paiement);

            return CreatedAtAction(nameof(GetPaiement), new { id = paiement.IdPaiement }, paiementDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaiementResponsedto>> UpdatePaiement(int id, PaiementRequestdto paiementRequestDto)
        {
            try
            {
                var existingPaiement = await _paiementService.GetPaiement(id);
                if (existingPaiement == null)
                {
                    return NotFound();
                }

                _mapper.Map(paiementRequestDto, existingPaiement);
                await _paiementService.UpdatePaiement(id, existingPaiement);
                var updatedPaiementDto = _mapper.Map<PaiementResponsedto>(existingPaiement);

                return Ok(updatedPaiementDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaiement(int id)
        {
            try
            {
                var paiement = await _paiementService.GetPaiement(id);
                if (paiement == null)
                {
                    return NotFound();
                }

                await _paiementService.DeletePaiement(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("create-payment-intent")]
        public async Task<ActionResult> CreatePaymentIntent(PaiementRequestdto paiementRequestDto)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(paiementRequestDto.Montant * 100), // Montant en cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            PaymentIntent intent = await service.CreateAsync(options);

            var paiement = _mapper.Map<Paiement>(paiementRequestDto);
            paiement.PaymentIntentId = intent.Id;
            await _paiementService.CreatePaiement(paiement);

            return Ok(new { clientSecret = intent.ClientSecret });
        }
    }
}


