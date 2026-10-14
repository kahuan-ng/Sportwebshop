using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.ViewModels.CategoryViewModels
{
    public class CategoryShowCategoryProductsViewModel
    {
        public ICollection<SportproductShowProductViewModel> CategoryProducts { get; set; }
    }
}