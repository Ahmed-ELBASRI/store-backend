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

            CreateMap<produitRequestDto, Product>();

            CreateMap<ClientRequestdto, Client>();
            CreateMap<Client, ClientResponsedto>();
            CreateMap<ClientRequestLoginDto, Client>();
            CreateMap<ClientRequestRegisterDto, Client>();

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

            CreateMap<LigneCommande, LigneCommandeResponse2Dto>();

            CreateMap<PanierRequestDto, Panier>();
            CreateMap<Panier, PanierResponseDto>();

            CreateMap<CommandRequestDto, Command>();
            CreateMap<Command, CommandResponseDto>();

            CreateMap<LigneCommandeRequestDto, LigneCommande>();
            CreateMap<LigneCommande, LigneCommandeResponseDto>();

            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductRequestDto>();

            CreateMap<Att_ProduitRequestDto, Att_Produit>();
            CreateMap<Att_Produit, Att_ProduitResponseDto>();

            CreateMap<PhotoProduitRequestDto, PhotoProduit>();
            CreateMap<PhotoProduit, PhotoProduitResponseDto>();

            //CreateMap<ProductRequestDto, Product>()
            //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(dest => dest.QteStock, opt => opt.MapFrom(src => src.QteStock))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            //CreateMap<Product, ProductResponseDto>()
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.QteStock, opt => opt.MapFrom(src => src.QteStock))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            //CreateMap<Att_ProduitRequestDto, Att_Produit>()
            //  .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Produit))
            //  .ForMember(dest => dest.Produit, opt => opt.Ignore());

            //CreateMap<Att_Produit, Att_ProduitResponseDto>()
            //    .ForMember(dest => dest.Produit, opt => opt.MapFrom(src => src.ProductId));

        }
    }
}
