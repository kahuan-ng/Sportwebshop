using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
    public class SportProductsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public SportProductsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {

            var sportProducts = _applicationDbContext.SportProducts;

            if (sportProducts == null) return NotFound();
                
            SportProductIndexViewModel sportProductIndexViewModel = new SportProductIndexViewModel()
            {
                Sportproducts = sportProducts.Select(sp => new SportproductShowProductViewModel()
                {
                    BaseId = sp.Id,
                    BaseContent = sp.Name,
                    ProductBrand = sp.Brand, 
                    ProductCategory = sp.Type,
                    ProductImage = sp.ProductImage,
                    ProductRating = sp.Rating,
                    ProductPrice = sp.Price,
                    ProductFeatures = sp.Features.Select(f => new Feature())
                }).ToList()
            };

            return View(sportProductIndexViewModel);
        }

        public IActionResult ShowProductInfo(int id)
        {
            var product = _applicationDbContext.SportProducts
                        .Include(sp => sp.Brand)
                        .Include(sp => sp.Type)
                        .Include(sp => sp.Features)
                        .FirstOrDefault(sp => sp.Id.Equals(id));

            if (product == null) return NotFound();

            SportproductShowProductViewModel sportproductShowProductViewModel = new SportproductShowProductViewModel()
            {
                ProductImage = product.ProductImage,
                BaseId = product.Id,
                BaseContent = product.Name,
                ProductBrand = product.Brand,
                ProductCategory = product.Type,
                ProductPrice = product.Price,
                ProductRating = product.Rating,
                ProductFeatures = product.Features.Select(f => new Feature()
                {
                    Id = f.Id,
                    Attribute = f.Attribute,
                    AttributeCategory = f.AttributeCategory
                })
            };
            return View(sportproductShowProductViewModel);
        }
    }
}