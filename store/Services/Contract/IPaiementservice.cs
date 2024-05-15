using AutoMapper;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using System;

namespace store.Services.Contract
{
    public interface IPaiementservice
    {
        Task <IEnumerable<Paiement>> GetPaiements();
        Task <Paiement> GetPaiement(int id);
        Task CreatePaiement(Paiement paiement);
        Task <Paiement> UpdatePaiement(int id, Paiement paiement);
        Task DeletePaiement(int id);




    }
}
