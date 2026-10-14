using System.ComponentModel.DataAnnotations;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class CategoryAddCategoryViewModel
    {
        [Required]
        public string Category { get; set; }
    }
}
