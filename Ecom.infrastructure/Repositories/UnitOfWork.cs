using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IPhotoRepository _photoRepository;
        public UnitOfWork (AppDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if(_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                if(_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }
        public IPhotoRepository PhotoRepository
        {
            get
            {
                if(_photoRepository == null)
                {
                    _photoRepository = new PhotoRepository(_context);
                }
                return _photoRepository;
            }
        }
    }
}
