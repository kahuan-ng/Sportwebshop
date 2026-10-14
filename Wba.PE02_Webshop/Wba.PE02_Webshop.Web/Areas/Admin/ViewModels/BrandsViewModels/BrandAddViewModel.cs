using System.ComponentModel.DataAnnotations;
using Wba.PE02_Webshop.Web.ViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.BrandsViewModels
{
    public class BrandAddViewModel
    {
        [Required]
        public string BrandName { get; set; }
        public IFormFile BrandLogo { get; set; }
    }
}
