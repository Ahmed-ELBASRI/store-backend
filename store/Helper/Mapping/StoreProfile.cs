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
            //CreateMap<ProductRequestDto, Product>();
            //CreateMap<Product, ProductResponseDto>();

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

            //CreateMap<ProductRequestDto, Product>();
            //CreateMap<Product, ProductRequestDto>();

            CreateMap<Att_ProduitRequestDto, Att_Produit>();
            CreateMap<Att_Produit, Att_ProduitResponseDto>();

            CreateMap<PhotoProduitRequestDto, PhotoProduit>();
            CreateMap<PhotoProduit, PhotoProduitResponseDto>();

            CreateMap<ProductRequestDto, Product>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.QteStock, opt => opt.MapFrom(src => src.QteStock));
                //.ForMember(dest => dest.PPs, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Image)
                //    ? new List<PhotoProduit>()
                //    : new List<PhotoProduit> { new PhotoProduit { UrlImage = src.Image } }));

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.PPs.FirstOrDefault() != null ? src.PPs.FirstOrDefault().UrlImage : string.Empty))
                .ForMember(dest => dest.QteStock, opt => opt.MapFrom(src => src.QteStock));




            CreateMap<Att_ProduitRequestDto, Att_Produit>();


            CreateMap<Att_Produit, Att_ProduitResponseDto>()
               .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Produit.Name));

               //.ForMember(dest => dest.Produit, opt => opt.Ignore());

        }
    }
}
