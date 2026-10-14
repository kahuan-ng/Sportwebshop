using Microsoft.AspNetCore.Mvc.Rendering;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.Service.Interfaces;

namespace Wba.PE02_Webshop.Web.Service
{
    public class FormBuilder : IFormBuilder
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FormBuilder(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<SelectListItem> GetBrands()
        {
            return _applicationDbContext.Brands.Select(b => new SelectListItem()
            {
                Text = b.BrandName,
                Value = b.Id.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetCategories()
        {
            return _applicationDbContext.Categories.Select(c => new SelectListItem()
            {
                Text = c.Type,
                Value = c.Id.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetFeatures()
        {
            return _applicationDbContext.Features.Select(a => new SelectListItem()
            {
                Text = a.Attribute,
                Value = a.Id.ToString()
            }).ToList();
        }
    }
}
