using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync (string? sort, int? CategoryId)
        {
            var query = context.Products.Include(m => m.Category)
                .Include(m => m.Photos)
                .AsNoTracking();

            //filtering by category Id
            if (CategoryId.HasValue)
                query = query.Where(m => m.CategoryId == CategoryId);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        query = query.OrderBy(m => m.NewPrice);
                        break;
                    case "PriceDes":
                        query = query.OrderByDescending(m => m.NewPrice);
                        break;
                    default:
                        query = query.OrderBy(m => m.Name);
                        break;

                }
            }
            //var products = await query.ToListAsync();
            var result = mapper.Map<List<ProductDTO>>(query);
            return result;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var product = mapper.Map<Product>(productDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var ImagePath = await imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var FindProduct = await context.Products.Include(m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateProductDTO.Id);
            if (FindProduct is null)
            {
                return false;
            }
            mapper.Map(updateProductDTO, FindProduct);
            var FindPhoto = await context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();
            foreach (var item in FindPhoto)
            {
                await imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.RemoveRange(FindPhoto);
            var ImagePath = await imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
            var photo = ImagePath.Select(path=> new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id,
            }).ToList();

           await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(m => m.ProductId == product.Id).ToListAsync();
            foreach (var item in photo)
            {
                await imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
