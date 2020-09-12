using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProducts(ProductSpecParams productParams)
        {
            var result = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand)
                .Where(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
             (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)).OrderBy(x => x.Name)
                .ToListAsync();
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        result = result.OrderBy(x => x.Price).ToList();
                        break;
                    case "priceDesc":
                        result = result.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            result = result.Skip(productParams.PageSize * (productParams.PageIndex - 1)).Take(productParams.PageSize).ToList();
            return result;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<int> GetProcuctsCount(ProductSpecParams productParams)
        {
            return await _context.Products.Where(x =>
              (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
              (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)).CountAsync();
        }
    }
}
