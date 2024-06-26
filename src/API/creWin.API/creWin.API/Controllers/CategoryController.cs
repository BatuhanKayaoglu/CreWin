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



namespace creWin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;

        public CategoryController(IUnitOfWork uow, IMapper mapper, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllCategoriesAsync();
            return categories != null ? Ok(categories) : NotFound();        
        }

        [HttpGet]
        [Route("SearchCategories")]
        public async Task<IActionResult> SearchCategory(string categoryName)
        {
            IEnumerable<Category> categories = await _categoryService.GetCategoryByName(categoryName);
            return categories != null ? Ok(categories) : NotFound();
        }
    }
}
