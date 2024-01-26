using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PT.Domain.Entities;
using PT.Services.Interfaces;

namespace ProductTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        private IProductService productService;

        public ProductController(IProductRepository productRepository, IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }

        /// <summary>
        /// Method to get the product by id
        /// </summary>
        /// <param name="productId">product related with the provided id </param>
        /// <returns>item or null</returns>
        [HttpGet("GetById/{productId}")]        
        public async Task<IActionResult> GetById(Guid? productId)
        {
            try
            {
                if (productId == null || productId == new Guid())
                {
                    return BadRequest("You must provide a product id");
                }

                return Ok(await this.productService.GetById(productId.Value));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method to create a product
        /// </summary>
        /// <param name="product">Product to be created on the repository</param>
        /// <returns>success or failure</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("You must provide a product");
                }

                return Ok(await this.productService.Insert(product));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method to update a product
        /// </summary>
        /// <param name="product">Produc to be updated on the repository</param>
        /// <returns>success or failure</returns>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("You must provide a product");
                }

                return Ok(await this.productService.Update(product));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
