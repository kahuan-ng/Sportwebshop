using Microsoft.AspNetCore.Mvc;
using System;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.CategoryViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var categories = _applicationDbContext.Categories;

            if (categories == null) return NotFound();

            CategoryIndexViewModel categoryIndexViewModel = new CategoryIndexViewModel()
            {
                categories = categories.Select(c => new BaseViewModel()
                {
                    BaseId = c.Id,
                    BaseContent = c.Type
                }).ToList()
            };

            return View(categoryIndexViewModel);
        }

        public IActionResult ShowCategoryProducts(string categoryKey)
        {
            var category = _applicationDbContext.Categories.FirstOrDefault(c => c.Type.Equals(categoryKey));

            { ViewData["Title"] = $"{categoryKey} products"; }

            var sportProducts = _applicationDbContext.SportProducts;

            if (category == null) return NotFound();

            CategoryShowCategoryProductsViewModel categoryShowCategoryProductsViewModel = new CategoryShowCategoryProductsViewModel()
            {
                CategoryProducts = sportProducts
                                    .Where(sp => sp.Type.Type.Equals(categoryKey))
                                    .OrderBy(sp => sp.Price)
                                     .Select(c => new SportproductShowProductViewModel()
                                     {
                                         ProductImage = c.ProductImage,
                                         BaseId = c.Id,
                                         BaseContent = c.Name,
                                         ProductBrand = c.Brand,
                                         ProductPrice = c.Price
                                     }).ToList()
            };
            return View(categoryShowCategoryProductsViewModel);
        }
    }
}