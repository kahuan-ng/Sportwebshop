using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.SportproductsViewModels
{
    public class SportproductsAddNewProductViewModel
    {
        [Required]
        [Display(Name = "Product name")]
        public string SportproductName { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage ="Gelieve een geldige prijs in te vullen.")] // Data annotation om een bereik te zetten
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Rating")]
        public int? Rating { get; set; }

        // Kiezen van een lijst
        public List<SelectListItem> Brands { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int SelectedBrand { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int SelectedCategory { get; set; }

        public List<SelectListItem> Features { get; set; }

        [Required]
        [Display(Name = "Feature")]
        public List<int> SelectedFeatures { get; set; }

        public IFormFile Image { get; set; }

    }
}
