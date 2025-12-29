using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.NewPrice,
                    opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.OldPrice,
                    opt => opt.MapFrom(src => src.OldPrice))
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Category,
                    opt => opt.Ignore()).ReverseMap();


            CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
    }
}
