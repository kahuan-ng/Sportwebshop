using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.FeatureViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
    public class FeatureController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FeatureController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var features = _applicationDbContext.Features;

            if (features == null) return NotFound();

            FeatureIndexViewModel featureIndexViewModel = new FeatureIndexViewModel()
            {
                Features = features.Select(f => new BaseViewModel()
                {
                    BaseId = f.Id,
                    BaseContent = f.Attribute
                }).ToList()
            };

            return View(featureIndexViewModel);
        }

        public IActionResult ShowAttributeProducts(string attributeKey)
        {
            var attribute = _applicationDbContext.Features.FirstOrDefault(a => a.Attribute == attributeKey);

            if (attribute == null) return NotFound();

            { 
                ViewData["Title"] = $"{attribute.AttributeCategory} - {attributeKey} products";
            }

            var sportProducts = _applicationDbContext.SportProducts;
            FeatureShowAttributeProductsViewModel featureShowAttributeProductsViewModel = new FeatureShowAttributeProductsViewModel()
            {
                AttributeProducts = sportProducts
                                    .Where(sp => sp.Features.Any(a => a.Attribute.Equals(attributeKey)))
                                    .OrderBy(sp => sp.Price)
                                    .Select(sp => new SportproductShowProductViewModel()
                                    {
                                        BaseId = sp.Id,
                                        BaseContent = sp.Name,
                                        ProductBrand = sp.Brand,
                                        ProductPrice = sp.Price,
                                        ProductImage = sp.ProductImage,
                                        ProductRating = sp.Rating
                                    }).ToList()
            };
            return View(featureShowAttributeProductsViewModel);
        }
    }
}
