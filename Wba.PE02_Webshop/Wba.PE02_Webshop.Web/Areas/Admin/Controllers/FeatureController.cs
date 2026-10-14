using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.CategoryViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.FeaturesViewModels;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;

        public FeatureController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbcontext = applicationDbContext;
        }

        public IActionResult FeaturesAdminOverview()
        {
            var features = _applicationDbcontext.Features;

            if (features == null) return NotFound();

            FeaturesAdminOverviewViewModel featuresAdminOverviewViewModel = new FeaturesAdminOverviewViewModel()
            {
                Features = features.Select(f => new FeatureAdminOverviewModelViewModel()
                {
                    BaseId = f.Id,
                    BaseContent = f.Attribute,
                    AttributeCategory = f.AttributeCategory
                }).ToList()
            };

            return View(featuresAdminOverviewViewModel);
        }

        #region Add Feature
        [HttpGet]
        public IActionResult AddFeature()
        {
            FeatureAddFeatureViewModel featureAddFeatureViewModel = new FeatureAddFeatureViewModel();

            return View(featureAddFeatureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFeature(FeatureAddFeatureViewModel featureAddFeatureViewModel)
        {
            if (!ModelState.IsValid) return View(featureAddFeatureViewModel);

            Feature newFeature = new Feature()
            {
                Attribute = featureAddFeatureViewModel.Attribute,
                AttributeCategory = featureAddFeatureViewModel.AttributeCategory
            };

            _applicationDbcontext.Features.Add(newFeature);

            try
            {
                _applicationDbcontext.SaveChanges();
            }

            catch (DbUpdateException DbuE)
            {
                Console.WriteLine(DbuE.Message);
            }

            return RedirectToAction(nameof(FeaturesAdminOverview));
        }
        #endregion

        #region Update Feature
        [HttpGet]
        public IActionResult UpdateFeature(int id )
        {
            var feature = _applicationDbcontext.Features.FirstOrDefault(f => f.Id.Equals(id));

            if (feature == null) return NotFound();

            FeatureUpdateFeatureViewModel featureUpdateFeatureViewModel = new FeatureUpdateFeatureViewModel()
            {
                FeatureId = feature.Id,
                Attribute = feature.Attribute,
                AttributeCategory = feature.AttributeCategory
            };
            return View(featureUpdateFeatureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFeature(FeatureUpdateFeatureViewModel featureUpdateFeatureViewModel)
        {
            var updatedFeature = _applicationDbcontext.Features.FirstOrDefault(f => f.Id.Equals(featureUpdateFeatureViewModel.FeatureId));

            if (updatedFeature == null) return NotFound();

            updatedFeature.Attribute = featureUpdateFeatureViewModel.Attribute;
            updatedFeature.AttributeCategory = featureUpdateFeatureViewModel.AttributeCategory;

            _applicationDbcontext.Features.Update(updatedFeature);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
            }

            return RedirectToAction(nameof(FeaturesAdminOverview));
        }
        #endregion

        #region Delete Feature

        [HttpGet]
        public IActionResult ConfirmDeleteFeature(int id)
        {
            var feature = _applicationDbcontext.Features.FirstOrDefault(f => f.Id.Equals(id));

            if (feature == null) return NotFound();

            FeatureDeleteFeatureViewModel featureDeleteFeatureViewModel = new FeatureDeleteFeatureViewModel()
            {
                FeatureId = feature.Id,
                Attribute = feature.Attribute
            };

            return View(featureDeleteFeatureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFeature(FeatureDeleteFeatureViewModel featureDeleteFeatureViewModel)
        {
            var feature = _applicationDbcontext.Features.FirstOrDefault(f => f.Id.Equals(featureDeleteFeatureViewModel.FeatureId));

            if (feature == null) return NotFound();

            _applicationDbcontext.Features.Remove(feature);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(FeaturesAdminOverview));
        }
        #endregion

    }
}
