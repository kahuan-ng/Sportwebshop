using Wba.PE02_Webshop.Core.Entities;

namespace Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels
{
    public class SportproductShowProductViewModel : BaseViewModel
    {
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int? ProductRating { get; set; }
        public Brand ProductBrand { get; set; }
        public Category ProductCategory { get; set; }
        public IEnumerable<Feature> ProductFeatures { get; set; }

    }
}
