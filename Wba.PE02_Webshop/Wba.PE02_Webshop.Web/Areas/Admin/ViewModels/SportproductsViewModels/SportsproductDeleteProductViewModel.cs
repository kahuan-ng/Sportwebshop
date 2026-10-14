using Microsoft.AspNetCore.Mvc;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.SportproductsViewModels
{
    public class SportsproductDeleteProductViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string productName { get; set; }

        public string Image { get; set; }
    }
}
