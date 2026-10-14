using Microsoft.AspNetCore.Mvc;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.BrandsViewModels
{
    public class BrandUpdateBrandViewModel : BrandAddViewModel
    {
        [HiddenInput]
        public int BrandId { get; set; }
    }
}
