using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //for future methods
        Task<bool> AddAsync(AddProductDTO productDTO);
    }
}
