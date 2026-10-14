using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.BrandViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
public class BrandController : Controller
{
    private readonly ApplicationDbContext _applicationDbContext;
        public BrandController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var brands = _applicationDbContext.Brands;

            if (brands == null) return NotFound();

            BrandIndexViewModel brandIndexViewModel = new BrandIndexViewModel()
            {
                Brands = brands.Select(b => new BrandShowBrandViewModel()
                {
                    BaseId = b.Id,
                    BaseContent = b.BrandName,
                    BrandLogo = b.BrandLogo
                }).ToList()
            };
            return View(brandIndexViewModel);
        }
        // Iets
        public IActionResult ShowBrandProducts(string brandKey)
        {
            var sportProducts = _applicationDbContext.SportProducts;
            var brand = _applicationDbContext.Brands.FirstOrDefault(b => b.BrandName.Equals(brandKey));

            { ViewData["Title"] = $"{brandKey} products"; }

            if (brand == null) return NotFound();

            BrandShowBrandProductsViewModel brandShowBrandProductsViewModel = new BrandShowBrandProductsViewModel()
            {
                BrandProducts = sportProducts
                            .Include(sp => sp.Brand)
                            .Include(sp => sp.Type)
                            .Include(sp => sp.Features)
                                .Where(sp => sp.Brand.BrandName.Equals(brandKey))
                                .OrderBy(sp => sp.Price)
                                .Select(sp => new SportproductShowProductViewModel()
                                {
                                    BaseId = sp.Id,
                                    BaseContent = sp.Name,
                                    ProductBrand = sp.Brand,
                                    ProductPrice = sp.Price,
                                    ProductImage = sp.ProductImage,
                                }).ToList()
            };

            return View(brandShowBrandProductsViewModel);
        }
    }
}