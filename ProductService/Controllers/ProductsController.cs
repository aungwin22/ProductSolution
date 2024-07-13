using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Models;
using ProductService.Services;
using System.Collections.Generic;
using MongoDB.Bson;


namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService.Services.ProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductService.Services.ProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            try
            {
                return _productService.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting products.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            try
            {
                var product = _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the product with id {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            try
            {
                //Generate a new ID if not provided
                if (string.IsNullOrEmpty(product.Id))
                {
                    // product.Id = ObjectId.GenerateNewId().ToString();
                    product.Id = Guid.NewGuid().ToString();
                }

                _productService.Create(product);

                return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Product productIn)
        {
            try
            {
                var product = _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                _productService.Update(id, productIn);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product with id {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var product = _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                _productService.Remove(product.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product with id {ProductId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
