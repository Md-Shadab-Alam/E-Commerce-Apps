using Core.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class ProductRepository : IProductRepository
	{
		private readonly StoreContext _context;
		public ProductRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products
				.Include(p => p.ProductType)
				.Include(p => p.ProductBrand)
				//  .FindAsync(id);				bcz it does not accept IQueryable
				//	.SingleOrDefaultAsync();	//if it finds more than one entity i hte list, it will show an exception
				.FirstOrDefaultAsync(p => p.Id == id);			//return an entity as soon as it find it in the list
		}
		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{


			return await _context.Products
				.Include(p=>p.ProductType)          //Navigation Property
                .Include(p=>p.ProductBrand)
				.ToListAsync();
		}
		
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

		public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
		{
			return await _context.ProductTypes.ToListAsync();
		}
    }
}
