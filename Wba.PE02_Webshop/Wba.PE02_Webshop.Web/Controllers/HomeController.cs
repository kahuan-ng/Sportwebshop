using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.HomeViewModels;
using Wba.PE02_Webshop.Web.ViewModels.BrandViewModels;

namespace Wba.PE02_Webshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var sportProducts = _applicationDbContext.SportProducts;
            var brands = _applicationDbContext.Brands;

            if (sportProducts == null) return NotFound();


            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                Brands = sportProducts
                    .GroupBy(sp => sp.Brand)
                    .OrderByDescending(b => b.Where(sp => sp.Rating.HasValue)
                        .Select(sp => sp.Rating.Value)
                        .Average())
                    .Take(6)

                    .Select(b => new BrandShowBrandViewModel()
                    {
                        BaseId = b.Key.Id,
                        BaseContent = b.Key.BrandName,
                        BrandLogo = b.Key.BrandLogo
                    }).ToList()
            };

            return View(homeIndexViewModel);
        }

        // In Controllers/HomeController.cs
        public IActionResult GoToViewComponent()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
