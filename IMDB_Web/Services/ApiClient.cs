using IMDB_EntityModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_Web.Services
{
    public class ApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private string ApiBaseUrl { get; set; }
        private string Token { get; set; }

        public ApiClient(IConfiguration configuration,
                         ILogger<ApiClient> logger,
                         IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            ApiBaseUrl = _configuration.GetSection("AppSettings:ApiBaseUrl").Value;
        }

        public async Task<ApiResponseModel> GetAsync(dynamic id, string endpoint)
        {
            ApiResponseModel model;

            try
            {
                var url = $"{ApiBaseUrl}/{endpoint}";
                if (id != null)
                    url = $"{url}?{id}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<object>(responseContent);
                model = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ApiResponseModel> PostAsync(dynamic input, string endpoint)
        {
            ApiResponseModel model;

            try
            {
                StringContent data = new StringContent("");
                if (input != null)
                {
                    var json = JsonConvert.SerializeObject(input);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }
                var url = $"{ApiBaseUrl}/{endpoint}";

                using var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                using var client = new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromMinutes(30)
                };

                var response = await client.PostAsync(url, data);
                var responseContent = await response.Content.ReadAsStringAsync();

                model = JsonConvert.DeserializeObject<ApiResponseModel>(responseContent);
                model.Code = (int)response.StatusCode;

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling endpoint: {endpoint}. Error Description: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ApiResponseModel> PostAsync(string endpoint)
        {
            return await PostAsync(null, endpoint);
        }

        public async Task<ApiResponseModel> GetAsync(string endpoint)
        {
            return await GetAsync(null, endpoint);
        }

    }
}
