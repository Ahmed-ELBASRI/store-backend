using AutoMapper;
using Microsoft.OpenApi.Writers;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;
using store.Services.Implementation;

namespace store.Helper.Mapping
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<ProductRequestDto, Product>();

            CreateMap<Product, ProductResponseDto>();

            CreateMap<ClientRequestdto, Client>();

            CreateMap<Client, ClientResponsedto>();

            CreateMap<Retour, RetourResponsedto>();

            CreateMap<RetourRequestdto, Retour>();

            CreateMap<Paiement, PaiementResponsedto>();

            CreateMap<PaiementRequestdto, Paiement>();


            CreateMap<VarianteRequestDto, Variante>();

            CreateMap<Variante, VarianteResponseDto>();

            CreateMap<AttVarianteRequestDto, Att_Variante>();

            CreateMap<Att_Variante, AttVarianteResponseDto>();

            CreateMap<PhotoVarianteRequestDto, PhotoVariante>();

            CreateMap<PhotoVariante, PhotoVarianteResponseDto>();

            CreateMap<LignePanierRequestDto, LignePanier>();

            CreateMap<LignePanier, LignePanierResponseDto>();

            CreateMap<PanierRequestDto, Panier>();

            CreateMap<Panier, PanierResponseDto>();





            CreateMap<CommandRequestDto, Command>();

            CreateMap<Command, CommandResponseDto>();

            CreateMap<LigneCommandeRequestDto, LigneCommande>();

            CreateMap<LigneCommande, LigneCommandeResponseDto>();


        }
    }
}
