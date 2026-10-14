using Microsoft.AspNetCore.Mvc;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Models;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels;

namespace Wba.PE02_Webshop.Web.ViewComponents
{
    [ViewComponent(Name = "FilterLinks")]
    public class FilterLinks : ViewComponent
    {

        private readonly ApplicationDbContext _applicatonDbContext;

        public FilterLinks(ApplicationDbContext applicationDbContext)
        {
            _applicatonDbContext = applicationDbContext;
        }

        public IViewComponentResult Invoke()
        {
            FilterLinksViewModel filterLinksViewModel = new FilterLinksViewModel()
            {
                FilterLinks = _applicatonDbContext.SportProducts.Select(sp => new LinkModel()
                {
                    Id = sp.Id,
                    Value = sp.Name,
                    Controller = "Sportproducts",
                    Action = "ShowProductInfo"
                }).ToList()
            };
            return View(filterLinksViewModel);
        }
    }
}
