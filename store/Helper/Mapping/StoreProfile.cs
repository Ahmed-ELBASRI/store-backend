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

            CreateMap<ClientRequestdto, Client>();
            CreateMap<Client, ClientResponsedto>();

            CreateMap<CommandRequestDto, Command>();
            CreateMap<Command, CommandResponseDto>();

            CreateMap<LigneCommandeRequestDto, LigneCommande>();
            CreateMap<LigneCommande, LigneCommandeResponseDto>();

        }
    }
}
