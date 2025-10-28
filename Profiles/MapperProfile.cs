using AutoMapper;
using InventoryApi.Data;
using InventoryApi.Dtos;

namespace InventoryApi.Profiles  // Adjust if in subfolder
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));  // Only map non-null values for partial updates
        }
    }
}