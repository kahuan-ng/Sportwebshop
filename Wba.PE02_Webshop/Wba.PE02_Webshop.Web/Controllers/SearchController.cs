using Microsoft.AspNetCore.Mvc;
using System;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SearchController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult ProductsUnder10Euros()
        {
            var sportProducts = _applicationDbContext.SportProducts;

            if (sportProducts == null) return NotFound();

            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Price < 10)
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult ProductsBetween10And50Euros()
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Price >= 10 && sp.Price <= 50)
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult ProductsBetween50And100Euros()
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Price >= 50 && sp.Price <= 100)
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult ProductsAbove100Euro()
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                        .Where(sp => sp.Price > 100)
                                        .OrderBy(sp => sp.Price)
                                        .Select(sp => new SportproductShowProductViewModel
                                        {
                                            BaseId = sp.Id,
                                            BaseContent = sp.Name,
                                            ProductBrand = sp.Brand,
                                            ProductImage = sp.ProductImage,
                                            ProductPrice = sp.Price,
                                        }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult SearchByBrandAndCategory(string brandKey, string categoryKey)
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Brand.BrandName.Equals(brandKey) && sp.Type.Type.Equals(categoryKey))
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult SearchByColorAndAudience(string colorKey, string audienceKey)
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Features.Any(a => a.Attribute.Equals(colorKey))
                                                      && sp.Features.Any(a => a.Attribute.Equals(audienceKey)))
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult SearchByPriceRange(decimal min, decimal max)
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Price >= min && sp.Price <= max)
                                            .OrderBy(sp => sp.Price)
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }

        public IActionResult ShowPromoProducts()
        {
            var sportProducts = _applicationDbContext.SportProducts;
            if (sportProducts == null) return NotFound();
            SearchProductsFilteredViewModel searchProductsFilteredViewModel = new SearchProductsFilteredViewModel()
            {
                FilteredSportProducts = sportProducts
                                            .Where(sp => sp.Features.Any(a => a.AttributeCategory.Equals("Promo")))
                                            .Select(sp => new SportproductShowProductViewModel
                                            {
                                                BaseId = sp.Id,
                                                BaseContent = sp.Name,
                                                ProductBrand = sp.Brand,
                                                ProductImage = sp.ProductImage,
                                                ProductPrice = sp.Price,
                                            }).ToList()
            };
            return View(searchProductsFilteredViewModel);
        }
    }
}