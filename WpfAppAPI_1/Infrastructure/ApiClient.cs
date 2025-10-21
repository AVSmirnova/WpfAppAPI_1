using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WpfAppAPI_1.Models;

namespace WpfAppAPI_1.Infrastructure
{
    internal class ApiClient
    {
        // поле для передачи из конструктора ссылки на созданный нами экземпляр HttpClient 
        private readonly System.Net.Http.HttpClient _httpClient;

        public ApiClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // получаем список категорий
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("/categories");
            return categories ?? new List<Category>();
        }

        // получаем список продуктов
        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("/products");
            return products ?? new List<Product>();
        }
    }
}
