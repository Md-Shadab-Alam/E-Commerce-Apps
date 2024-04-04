using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using E_commerce_API.Dtos;
using E_commerce_API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;


namespace E_commerce_API.Controllers
{
    public class ProductsController : BaseApiController
    {
        //Commenting because, Now I am using Generic repository
        // private readonly IProductRepository _repo;
        //public ProductsController(IProductRepository repo)
        //{
        //    _repo = repo;
        //}

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> ProductTypeRepo) 
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = ProductTypeRepo;

        }

        [HttpGet]
        //public async Task<ActionResult<List<Product>>> GetProducts()
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(
           [FromQuery] ProductSpecParams productPrams)

        {
            //var products = await _repo.GetProductsAsync();
            var spec = new ProductWithTypesAndBrandsSpecification(productPrams);

            var products = await _productsRepo.ListAsync(spec);
            //return Ok(products);
            return products.Select(product => new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name,
            }).ToList();
        }
        
        [HttpGet("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        // //public async Task<ActionResult<Product>> GetProduct(int id)
         public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // return await _repo.GetProductByIdAsync(id);

            //return await _productsRepo.GetByIdAsync(id);
            var spec = new ProductWithTypesAndBrandsSpecification(id);
           // return await _productsRepo.GetEntityWithSpec(spec);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if(product == null) return NotFound(new ApiResponse(404));
            return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name,
            };



        }

        [HttpGet("brand")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            // var brand = await _repo.GetProductBrandAsync();
            var brand = await _productBrandRepo.ListAllAsync();
            return Ok(brand);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }

}
