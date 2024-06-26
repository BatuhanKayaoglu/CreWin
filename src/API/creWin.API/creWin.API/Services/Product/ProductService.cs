using AutoMapper;
using CreWin.Entity.Models;
using CreWin.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using creWin.API.Services.Categories;
using creWin.API.Services.EmailSender;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using static CreWin.Entity.Models.Product;
using System.Net.Http;

namespace Nowadays.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IUnitOfWork uow, IMapper mapper, IEmailSenderService emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductResponse> GetProducts()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/products/category/smartphones");
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch products");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            ProductResponse? products = JsonSerializer.Deserialize<ProductResponse>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return await Task.FromResult(products); 
        }


        public async Task<ProductResponse> SearchProductsByName(string productName)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/products/category/smartphones");
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch products");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            ProductResponse? products = JsonSerializer.Deserialize<ProductResponse>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var filteredProducts = products.Products
                .Where(p => p.Title.Contains(productName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return new ProductResponse { Products = filteredProducts };
        }


            public async Task<ProductResponse> GetProductsByCategory(string categoryName)
        {
            string requestUrl = $"https://dummyjson.com/products/category/{categoryName}";
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch products");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            ProductResponse? products = JsonSerializer.Deserialize<ProductResponse>(jsonResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return await Task.FromResult(products);
        }

    }
}
