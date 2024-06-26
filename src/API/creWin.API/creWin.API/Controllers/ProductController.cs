using AutoMapper;
using creWin.API.Exceptions;
using creWin.API.Pagination;
using creWin.API.Services;
using Microsoft.AspNetCore.Mvc;
using CreWin.Common.ResponseViewModel;
using CreWin.Common.ViewModels;
using CreWin.Entity.Models;
using CreWin.Infrastructure.IRepositories;
using System.Text;
using creWin.API.Services.Categories;
using System.Text.Json;
using creWin.API.Services.Auth;
using creWin.API.Services.Token;
using Microsoft.AspNetCore.Http;
using static CreWin.Entity.Models.Product;
using System.Net.Http;



namespace creWin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(IUnitOfWork uow, IMapper mapper, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _uow = uow;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }


        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<ProductResponse>> SearchProductsByName(string productName)
        {
            ProductResponse products = await _productService.SearchProductsByName(productName);
            return Ok(products);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ProductResponse> GetProductsData()
        {
            ProductResponse products = await _productService.GetProducts();
            return products;
        }

        [HttpGet]   
        [Route("GetProductsByCategory")]
        public async Task<ProductResponse> GetProductsByCategory(string categoryName)
        {
            ProductResponse products = await _productService.GetProducts();
            return products;
        }
    }
}
