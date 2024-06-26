using AutoMapper;
using creWin.API.Services.Categories;
using CreWin.Entity.Models;
using CreWin.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using static CreWin.Entity.Models.Product;

namespace creWin.API.Services.Categories
{
    public interface IProductService
    {
        Task<ProductResponse> GetProducts();
        Task<ProductResponse> GetProductsByCategory(string categoryName);
        Task<ProductResponse> SearchProductsByName(string productName);
    }
}

