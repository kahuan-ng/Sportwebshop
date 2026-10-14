using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.CategoryViewModels;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbcontext = applicationDbContext;
        }

        public IActionResult CategoriesAdminOverview()
        {
            var categories = _applicationDbcontext.Categories;

            if (categories == null) return NotFound();

            AdminOverviewViewModel adminOverviewViewModel = new AdminOverviewViewModel()
            {
                Objects = categories.Select(c => new BaseViewModel(){
                    BaseId = c.Id,
                    BaseContent = c.Type
                }).ToList()
            };

            return View(adminOverviewViewModel);
        }

        #region Add Category

        [HttpGet]
        public IActionResult AddCategory()
        {
            CategoryAddCategoryViewModel categoryAddCategoryViewModel = new CategoryAddCategoryViewModel();

            return View(categoryAddCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(CategoryAddCategoryViewModel categoryAddCategoryViewModel)
        {

            if (!ModelState.IsValid) return View(categoryAddCategoryViewModel);

            Category newCategory = new Category()
            {
                Type = categoryAddCategoryViewModel.Category
            };

            _applicationDbcontext.Categories.Add(newCategory);

            try
            {
                _applicationDbcontext.SaveChanges();
            }

            catch (DbUpdateException DbuE)
            {
                Console.WriteLine(DbuE.Message);
            }

            return RedirectToAction(nameof(CategoriesAdminOverview));
        }
        #endregion

        #region Update Category
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _applicationDbcontext.Categories.FirstOrDefault(c => c.Id.Equals(id));

            if (category == null) return NotFound();

            CategoryUpdateCategoryViewModel categoryUpdateCategoryViewModel = new CategoryUpdateCategoryViewModel()
            {
                CategoryId = category.Id,
                Category = category.Type
            };
            return View(categoryUpdateCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(CategoryUpdateCategoryViewModel categoryUpdateCategoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryUpdateCategoryViewModel);

            var updatedCategory = _applicationDbcontext.Categories.FirstOrDefault(c => c.Id.Equals(categoryUpdateCategoryViewModel.CategoryId));

            updatedCategory.Type = categoryUpdateCategoryViewModel.Category;

            _applicationDbcontext.Categories.Update(updatedCategory);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
            }

            return RedirectToAction(nameof(CategoriesAdminOverview));
        }
        #endregion

        #region Delete Category

        [HttpGet]
        public IActionResult ConfirmDeleteCategory(int id)
        {
            var category = _applicationDbcontext.Categories.FirstOrDefault(c => c.Id.Equals(id));

            if (category == null) return NotFound();

            CategoryDeleteCategoryViewModel categoryDeleteCategoryViewModel = new CategoryDeleteCategoryViewModel()
            {
                CategoryId = category.Id,
                Category = category.Type
            };

            return View(categoryDeleteCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(CategoryDeleteCategoryViewModel categoryDeleteCategoryViewModel)
        {
            var category = _applicationDbcontext.Categories.FirstOrDefault(c => c.Id.Equals(categoryDeleteCategoryViewModel.CategoryId));

            if (category == null) return NotFound();

            _applicationDbcontext.Categories.Remove(category);

            try { _applicationDbcontext.SaveChanges(); }

            catch (DbUpdateException DbuE)
            {
                Debug.WriteLine(DbuE.Message);
                return View("Error", new ErrorViewModel());
            }

            return RedirectToAction(nameof(CategoriesAdminOverview));
        }
        #endregion
    }
}
