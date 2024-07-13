using ProductWebApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProductWebApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        }

        public async Task CreateProductAsync(Product product)
        {
            await _httpClient.PostAsJsonAsync("api/products", product);
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/products/{id}");
        }
    }
}
