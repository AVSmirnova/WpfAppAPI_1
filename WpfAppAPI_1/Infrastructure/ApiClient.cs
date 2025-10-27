using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfAppAPI_1.Model;
using WpfAppAPI_1.Models;

namespace WpfAppAPI_1.Infrastructure
{
    internal class ApiClient
    {
        // поле для передачи из конструктора ссылки на созданный нами экземпляр HttpClient 
        private readonly System.Net.Http.HttpClient _httpClient;

        Dictionary<string, string> tokenDictionary;

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

        public async Task<string> RegistrUser(User user)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/user/register", user);
            
            string content = "";
            if (response.IsSuccessStatusCode)
            { 
                content = await response.Content.ReadAsStringAsync(); 
            } 
            else
            {
                content = response.StatusCode.ToString();
            }
                return content;
        }

        public async Task<string> UserLogin(LoginUserRequestDto loginUserRequestDto)
        {
            string message = "";
            var response = await _httpClient.PostAsJsonAsync("/user/login", loginUserRequestDto);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                // Десериализация полученного JSON-объекта
                tokenDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(result);
                if (tokenDictionary != null)
                {
                    tokenDictionary.TryGetValue("token", out string token);
                    message = "Авторизация успешна" + token;
                }
            }
            else {   message= "Авторизация не прошла"; }
            return message;

        }
               
    }
}
