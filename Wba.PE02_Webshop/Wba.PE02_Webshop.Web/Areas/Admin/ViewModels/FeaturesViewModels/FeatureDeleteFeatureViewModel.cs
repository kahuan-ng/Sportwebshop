using Microsoft.AspNetCore.Mvc;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.FeaturesViewModels
{
    public class FeatureDeleteFeatureViewModel
    {
        [HiddenInput]
        public int FeatureId { get; set; }
        public string Attribute { get; set; }
    }
}
