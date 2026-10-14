using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wba.PE02_Webshop.Web.Service.Interfaces
{
    public interface IFormBuilder
    {
        public List<SelectListItem> GetBrands();
        public List<SelectListItem> GetCategories();
        public List<SelectListItem> GetFeatures();
    }
}
