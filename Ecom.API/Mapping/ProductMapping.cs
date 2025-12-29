using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            // Add Product
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Photos,
                    opt => opt.MapFrom(src => src.Photos));

            // Photos
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
