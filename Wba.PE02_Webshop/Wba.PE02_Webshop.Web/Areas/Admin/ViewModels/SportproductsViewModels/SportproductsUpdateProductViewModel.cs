using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Wba.PE02_Webshop.Web.Areas.Admin.ViewModels.SportproductsViewModels
{
    public class SportproductsUpdateProductViewModel : SportproductsAddNewProductViewModel
    {
        // Geef de id mee om te info te wijzigen  
        [HiddenInput]
        public int Id { get; set; }
    }
}
