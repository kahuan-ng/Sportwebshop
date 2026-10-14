using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.ViewModels;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels
{
    public class AdminOverviewViewModel
    {
        public ICollection<BaseViewModel> Objects { get; set; }
    }
}
