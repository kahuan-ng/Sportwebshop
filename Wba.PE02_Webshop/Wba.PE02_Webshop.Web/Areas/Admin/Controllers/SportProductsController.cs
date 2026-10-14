using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wba.PE02_Webshop.Core.Entities;
// ViewModels
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.SportproductsViewModels;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.Service.Interfaces;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SportProductsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;
        private readonly IFormBuilder _formBuilder;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SportProductsController(ApplicationDbContext applicationDbContext, IFormBuilder formBuilder, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbcontext = applicationDbContext;
            _formBuilder = formBuilder;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult SportproductsAdminOverview()
        {
            var sportproducts = _applicationDbcontext.SportProducts;

            if (sportproducts == null) return NotFound();

            AdminOverviewViewModel adminOverviewViewModel = new AdminOverviewViewModel()
            {
                Objects = sportproducts.Select(sp => new BaseViewModel()
                {
                    BaseId = sp.Id,
                    BaseContent = sp.Name,
                }).ToList()
            };

            return View(adminOverviewViewModel);
        }

        #region Add Sportproduct

        [HttpGet]
        public IActionResult AddSportproduct()
        {
            SportproductsAddNewProductViewModel sportproductsAddNewProductViewModel = new SportproductsAddNewProductViewModel()
            {
                Brands = _formBuilder.GetBrands(),
                Categories = _formBuilder.GetCategories(),
                Features = _formBuilder.GetFeatures()
            };

            return View(sportproductsAddNewProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSportproduct(SportproductsAddNewProductViewModel sportproductsAddNewProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                sportproductsAddNewProductViewModel.Brands = _formBuilder.GetBrands();
                sportproductsAddNewProductViewModel.Categories = _formBuilder.GetCategories();
                sportproductsAddNewProductViewModel.Features = _formBuilder.GetFeatures();

                return View(sportproductsAddNewProductViewModel);
            }

            var selectedFeatures = _applicationDbcontext.Features.Where(a => sportproductsAddNewProductViewModel.SelectedFeatures.Contains(a.Id)).ToList();

            SportProduct newSportproduct = new SportProduct()
            {
                Name = sportproductsAddNewProductViewModel.SportproductName,
                Price = sportproductsAddNewProductViewModel.Price,
                Rating = sportproductsAddNewProductViewModel.Rating,
                BrandId = sportproductsAddNewProductViewModel.SelectedBrand,
                CategoryId = sportproductsAddNewProductViewModel.SelectedCategory,
                Features = selectedFeatures                
            };

            if (sportproductsAddNewProductViewModel.Image != null)
            {
                var filename = $"{sportproductsAddNewProductViewModel.Image.FileName}";
                var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(pathToDirectory))
                {
                    Directory.CreateDirectory(pathToDirectory);
                }

                var pathToFile = Path.Combine(pathToDirectory, filename);

                using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
                {
                    sportproductsAddNewProductViewModel.Image.CopyTo(fileStream);
                }

                newSportproduct.ProductImage = filename;
            }

            else
            {
                newSportproduct.ProductImage = "NoImage.png";
            }

            _applicationDbcontext.SportProducts.Add(newSportproduct);

            try
            {
                _applicationDbcontext.SaveChanges();
            }

            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(SportproductsAdminOverview));
        }

        #endregion

        #region Update Product
        [HttpGet]
        public IActionResult UpdateSportproduct(int id)
        {
            var sportproduct = _applicationDbcontext.SportProducts
                .Include(sp => sp.Brand)
                .Include(sp => sp.Type)
                .Include(sp => sp.Features)
                .FirstOrDefault(sp => sp.Id.Equals(id));

            if (sportproduct == null) return NotFound();

            SportproductsUpdateProductViewModel sportproductsUpdateProductViewModel = new SportproductsUpdateProductViewModel()
            {
                Id = sportproduct.Id,
                SportproductName = sportproduct.Name,
                Price = sportproduct.Price,
                Rating = sportproduct.Rating,
                SelectedBrand = sportproduct.BrandId ?? 0,
                SelectedCategory = sportproduct.CategoryId ?? 0,
                SelectedFeatures = sportproduct.Features.Select(a => a.Id).ToList(),
                // FIll selectboxs
                Brands = _formBuilder.GetBrands(),
                Categories = _formBuilder.GetCategories(),
                Features = _formBuilder.GetFeatures()
            };
            return View(sportproductsUpdateProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSportproduct(SportproductsUpdateProductViewModel sportproductsUpdateProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                sportproductsUpdateProductViewModel.Brands = _formBuilder.GetBrands();
                sportproductsUpdateProductViewModel.Categories = _formBuilder.GetBrands();
                sportproductsUpdateProductViewModel.Features = _formBuilder.GetFeatures();

                return View(sportproductsUpdateProductViewModel);
            }

            // student nemen
            var updatedSportproduct = _applicationDbcontext.SportProducts
                .Include(sp => sp.Features)
                .FirstOrDefault(sp => sp.Id.Equals(sportproductsUpdateProductViewModel.Id));

            // Product updaten
            updatedSportproduct.Name = sportproductsUpdateProductViewModel.SportproductName;
            updatedSportproduct.Price = sportproductsUpdateProductViewModel.Price;
            updatedSportproduct.Rating = sportproductsUpdateProductViewModel.Rating;
            updatedSportproduct.BrandId = sportproductsUpdateProductViewModel.SelectedBrand;
            updatedSportproduct.CategoryId = sportproductsUpdateProductViewModel.SelectedCategory;
            updatedSportproduct.Features = _applicationDbcontext.Features
                                                            .Where(a => sportproductsUpdateProductViewModel.SelectedFeatures.Contains(a.Id)).ToList();

            if (sportproductsUpdateProductViewModel.Image != null)
            {
                var filename = $"{sportproductsUpdateProductViewModel.Image.FileName}";
                var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(pathToDirectory))
                {
                    Directory.CreateDirectory(pathToDirectory);
                }

                var pathToFile = Path.Combine(pathToDirectory, filename);

                using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
                {
                    sportproductsUpdateProductViewModel.Image.CopyTo(fileStream);
                    if (!String.IsNullOrEmpty(updatedSportproduct.ProductImage) && updatedSportproduct.ProductImage != "NoImage.png")
                    {
                        System.IO.File.Delete(Path.Combine(pathToDirectory, updatedSportproduct.ProductImage));
                    };
                }
                updatedSportproduct.ProductImage = filename;
            }

                // Updaten
                _applicationDbcontext.SportProducts.Update(updatedSportproduct);

                try { _applicationDbcontext.SaveChanges(); }

                catch (DbUpdateException DbuE)
                {
                    Debug.WriteLine(DbuE.Message);
                }

                return RedirectToAction(nameof(SportproductsAdminOverview));
        }
        #endregion

        #region Delete Product
        [HttpGet]
        public IActionResult ConfirmDeleteSportproduct(int id)
        {
            var sportproduct = _applicationDbcontext.SportProducts.FirstOrDefault(sp => sp.Id.Equals(id));

            if (sportproduct == null) return NotFound();

            SportsproductDeleteProductViewModel sportsproductDeleteProductViewModel = new SportsproductDeleteProductViewModel()
            {
                Id = id,
                productName = sportproduct.Name
            };

            return View(sportsproductDeleteProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSportproduct(SportsproductDeleteProductViewModel sportsproductDeleteProductViewModel)
        {
            var sportproduct = _applicationDbcontext.SportProducts.FirstOrDefault(sp => sp.Id.Equals(sportsproductDeleteProductViewModel.Id));

            if (sportproduct == null) return NotFound();

            if (!String.IsNullOrEmpty(sportproduct.ProductImage) && sportproduct.ProductImage != "NoImage.png")
            {
                var pathToImage = Path.Combine(_webHostEnvironment.WebRootPath, "images", sportproduct.ProductImage);
                if (System.IO.File.Exists(pathToImage))
                {
                    System.IO.File.Delete(pathToImage);
                }
            }

            _applicationDbcontext.SportProducts.Remove(sportproduct);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(SportproductsAdminOverview));
        }
        #endregion
    }
}