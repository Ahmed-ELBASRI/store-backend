using AutoMapper;
using store.Dtos.Request;
using store.Dtos.Responce;
using store.Models;

namespace store.Helper.Mapping
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<ProductRequestDto, Product>();

            CreateMap<Product, ProductResponseDto>();

            CreateMap<CategorieRequestDto, Categorie>();

            CreateMap<Categorie, CategorieResponseDto>();

            CreateMap<ClientRequestdto, Client>();

            CreateMap<Client, ClientResponsedto>();
        }
    }
}
