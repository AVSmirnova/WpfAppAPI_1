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
using System.Windows;
using System.Windows.Controls;
using WpfAppAPI_1.Model;
using WpfAppAPI_1.Model.Dto;
using WpfAppAPI_1.Models;

namespace WpfAppAPI_1.Infrastructure
{
    internal class ApiClient
    {
        // поле для передачи из конструктора ссылки на созданный нами экземпляр HttpClient 
        private readonly System.Net.Http.HttpClient _httpClient;

        Dictionary<string, string> tokenDictionary;

        static string token = "";

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

        public async Task<string> RegistrUserAsync(User user)
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

        public async Task<string> UserLoginAsync(LoginUserRequestDto loginUserRequestDto)
        {
            string message = "";
           
            var response = await _httpClient.PostAsJsonAsync("/user/login", loginUserRequestDto);

            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                
                // Десериализация полученного JSON-объекта
                tokenDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(result);
                if (tokenDictionary != null)
                {
                    tokenDictionary.TryGetValue("token", out token);

                    // создаем http-клиента с токеном 
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    }

                    message = "True";
                }
                
               
            }
            else {   message= result; }
            return message;

        }

        // получаем информацию о юзере
        public async Task<UserResponseDto> GetUserInfoAsync()
        {

            var userInfo = await _httpClient.GetFromJsonAsync<UserResponseDto>("/user/info");
            return userInfo ?? new UserResponseDto();

        }        

        // получаем информацию о всех доставках
        public async Task<List<DeliveryResponseDto>> GetDeliveriesAsync()
        {       
                var deliveries = await _httpClient.GetFromJsonAsync<List<DeliveryResponseDto>>("/deliveries");
                return deliveries ?? new List<DeliveryResponseDto>();
            
        }

        // получаем информацию о  доставках авторизованного пользователя
        public async Task<List<DeliveryResponseDto>> GetUserDeliveriesAsync()
        {
                var deliveries = await _httpClient.GetFromJsonAsync<List<DeliveryResponseDto>>("/deliveries/user/active");
                return deliveries ?? new List<DeliveryResponseDto>();

        }

        // получаем список продуктов по списку Id продуктов.

        public async Task<List<Product>> GetProductsByIds(List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
                return new List<Product>();

            var idsParam = string.Join(",", productIds);
            var url = $"/products?ids={idsParam}";
         

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)

            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(content, options);

            }

            throw new HttpRequestException($"Ошибка при получении продуктов: {response.StatusCode}"); 
            
            //var products = await _httpClient.GetFromJsonAsync<List<Product>>(url);
            //return products ?? new List<Product>();
        }



    }
}
