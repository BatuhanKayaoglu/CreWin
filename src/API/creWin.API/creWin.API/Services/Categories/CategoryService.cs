using AutoMapper;
using CreWin.Entity.Models;
using CreWin.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using creWin.API.Services.Categories;
using creWin.API.Services.EmailSender;
using System.Text.Json;

namespace Nowadays.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(IUnitOfWork uow, IMapper mapper, IEmailSenderService emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/products/categories");
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch categories");


            string responsejson = await response.Content.ReadAsStringAsync();
            List<Category> categoryData = JsonSerializer.Deserialize<List<Category>>(responsejson.Trim());
            return categoryData;
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string categoryName)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/products/categories");
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch categories");

            string responseJson = await response.Content.ReadAsStringAsync();
            IEnumerable<Category> categories = JsonSerializer.Deserialize<List<Category>>(responseJson);

            if (!string.IsNullOrEmpty(categoryName))    
                throw new Exception("Category name is not found");       

            categories = categories.Where(c => c.name.Contains(categoryName, StringComparison.OrdinalIgnoreCase));
            return await Task.FromResult(categories);
        }

    }
}
