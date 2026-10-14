using Microsoft.AspNetCore.Mvc;
using Wba.PE02_Webshop.Core.Entities;
using Wba.PE02_Webshop.Web.Areas.Admin.ViewModels;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.ViewModels.SportproductViewModels;

namespace Wba.PE02_Webshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _applicationDbcontext;

        public AdminController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbcontext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}