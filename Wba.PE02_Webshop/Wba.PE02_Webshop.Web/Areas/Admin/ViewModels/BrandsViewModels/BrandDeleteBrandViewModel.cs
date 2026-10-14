using Microsoft.AspNetCore.Mvc;
namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.BrandsViewModels
{
    public class BrandDeleteBrandViewModel
    {
        [HiddenInput]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
