using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.BrandsViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.SportproductsViewModels;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.BrandViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbcontext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult BrandsAdminOverview()
        {
            var brands = _applicationDbcontext.Brands;

            BrandAdminOverviewViewModel brandAdminOverviewViewModel = new BrandAdminOverviewViewModel()
            {
                Brands = brands.Select(b => new  BrandShowBrandViewModel()
                 {
                     BaseId = b.Id,
                     BaseContent = b.BrandName,
                     BrandLogo = b.BrandLogo
                 }).ToList()
            };
            return View(brandAdminOverviewViewModel);
        }

        #region Add Brand
        [HttpGet]
        public IActionResult AddBrand()
        {
            BrandAddViewModel brandAddViewModel = new BrandAddViewModel();

            return View(brandAddViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult AddBrand(BrandAddViewModel brandAddViewModel)
        {
            if (!ModelState.IsValid) return View(brandAddViewModel);

            Brand newBrand = new Brand()
            {
                BrandName = brandAddViewModel.BrandName,
            };

           if (brandAddViewModel.BrandLogo != null)
            {
                var filename = brandAddViewModel.BrandLogo.FileName;

                // Foto file toevoegen en aanmaken
                var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Brands");
                var pathToFile = Path.Combine(pathToDirectory, filename);

                if (!Directory.Exists(pathToDirectory))
                {
                    Directory.CreateDirectory(pathToDirectory);
                }

                using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create)) 
                {
                    brandAddViewModel.BrandLogo.CopyTo(fileStream);
                }

                newBrand.BrandLogo = filename;
            }

            else
            {
                newBrand.BrandLogo = "../NoImage.png";
            }


            _applicationDbcontext.Brands.Add(newBrand);

           try
            {
                _applicationDbcontext.SaveChanges();
            }

            catch (DbUpdateException DbuE)
            {
                Console.WriteLine(DbuE.Message);
            }
            
            return RedirectToAction(nameof(BrandsAdminOverview));
        }
        #endregion

        #region Update Brand
        [HttpGet]
        public IActionResult UpdateBrand(int id)
        {
            var brand = _applicationDbcontext.Brands.FirstOrDefault(b => b.Id.Equals(id));

            if (brand == null) return NotFound();

            BrandUpdateBrandViewModel brandUpdateBrandViewModel = new BrandUpdateBrandViewModel()
            {
                BrandName = brand.BrandName,
                BrandId = brand.Id
            };

            return View(brandUpdateBrandViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult UpdateBrand(BrandUpdateBrandViewModel brandUpdateBrandViewModel)
        {

            if (!ModelState.IsValid) return View(brandUpdateBrandViewModel);

            var updatedBrand = _applicationDbcontext.Brands.FirstOrDefault(b => b.Id.Equals(brandUpdateBrandViewModel.BrandId));

            updatedBrand.BrandName = brandUpdateBrandViewModel.BrandName;

            if (brandUpdateBrandViewModel.BrandLogo != null)
            {
                var filename = $"{brandUpdateBrandViewModel.BrandLogo.FileName}";
                var pathToDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Brands");

                if (!Directory.Exists(pathToDirectory))
                {
                    Directory.CreateDirectory(pathToDirectory);
                }

                var pathToFile = Path.Combine(pathToDirectory, filename);

                using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
                {
                    brandUpdateBrandViewModel.BrandLogo.CopyTo(fileStream);
                    if (!String.IsNullOrEmpty(updatedBrand.BrandLogo) && updatedBrand.BrandLogo != "pokeball.png")
                    {
                        System.IO.File.Delete(Path.Combine(pathToDirectory, updatedBrand.BrandLogo));
                    }
                    ;
                }
                updatedBrand.BrandLogo = filename;
            }

            _applicationDbcontext.Brands.Update(updatedBrand);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
            }


            return RedirectToAction(nameof(BrandsAdminOverview));
        }
        #endregion

        #region Delete Brand

        [HttpGet]
        public IActionResult ConfirmDeleteBrand(int id)
        {
            var brand = _applicationDbcontext.Brands.FirstOrDefault(b => b.Id.Equals(id));

            if (brand == null) return NotFound();

            BrandDeleteBrandViewModel brandDeleteBrandViewModel = new BrandDeleteBrandViewModel()
            {
                BrandId = brand.Id,
                BrandName = brand.BrandName
            };

            return View(brandDeleteBrandViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBrand(BrandDeleteBrandViewModel brandDeleteBrandViewModel)
        {
            var brand = _applicationDbcontext.Brands.FirstOrDefault(b => b.Id.Equals(brandDeleteBrandViewModel.BrandId));

            if (brand == null) return NotFound();

            _applicationDbcontext.Brands.Remove(brand);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(BrandsAdminOverview));
        }
        #endregion
    }
}