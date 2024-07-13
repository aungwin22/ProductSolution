using Microsoft.AspNetCore.Mvc;
using ProductWebApp.Models;
using ProductWebApp.Services;
using System.Threading.Tasks;

namespace ProductWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductsController> _logger;


        public ProductsController(ProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
           Product product = new Product();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Id is already set in the form, no need to generate a new one
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Log validation errors
                foreach (var modelStateKey in ModelState.Keys)
                {
                    
                    var modelStateVal = ModelState[modelStateKey];
                    if (modelStateVal != null)
                    foreach (var error in modelStateVal.Errors)
                    {
                        _logger.LogError($"Validation error in key '{modelStateKey}': {error.ErrorMessage}");
                    }
                }
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(id, product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
