using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Helper.Data;
using store.Models;
using store.Services.Contract;
using store.Services.Implementation;
using Stripe;
using Stripe.Checkout;
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

            // Créer un paiement avec Stripe
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(paiementRequestDto.Montant * 100), // Montant en centimes
                Currency = "eur", // Devise
                PaymentMethodTypes = new List<string> { "card" }, // Types de méthode de paiement autorisés
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            // Enregistrer le paiement dans votre base de données avec l'ID du paiement Stripe
            var paiement = _mapper.Map<Paiement>(paiementRequestDto);
            paiement.StripePaymentIntentId = paymentIntent.Id;
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





        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },
            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "http://localhost:4242/success",
                CancelUrl = "http://localhost:4242/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
