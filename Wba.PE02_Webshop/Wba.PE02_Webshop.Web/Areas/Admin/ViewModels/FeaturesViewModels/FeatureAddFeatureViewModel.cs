using System.ComponentModel.DataAnnotations;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.FeaturesViewModels
{
    public class FeatureAddFeatureViewModel
    {
        [Required]
        public string Attribute { get; set; }

        [Required]
        public string AttributeCategory { get; set; }
    }
}
