using AutoMapper;
using creWin.API.Services.Categories;
using CreWin.Entity.Models;
using CreWin.Infrastructure.IRepositories;

namespace creWin.API.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetCategoryByName(string categoryName);
    }
}

