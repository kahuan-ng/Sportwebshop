using Microsoft.AspNetCore.Mvc;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.FeaturesViewModels
{
    public class FeatureUpdateFeatureViewModel : FeatureAddFeatureViewModel
    {
        [HiddenInput]
        public int FeatureId { get; set; }
    }
}
