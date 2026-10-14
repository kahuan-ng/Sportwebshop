using Microsoft.AspNetCore.Mvc;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class CategoryUpdateCategoryViewModel : CategoryAddCategoryViewModel
    {
        [HiddenInput]
        public int CategoryId { get; set; }
    }
}
