using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wba.PE02_Webshop.Web.Models.Data;
using Wba.PE02_Webshop.Web.Service;
using Wba.PE02_Webshop.Web.Service.Interfaces;

namespace Wba.PE02_Webshop.Web
{
    public class Program
    {

        // test
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to container
            builder.Services.AddScoped<IFormBuilder, FormBuilder>();

            builder.Services.AddDbContext<ApplicationDbContext>(
               options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebshopDb"))
               );
            // Add services to the container.
            builder.Services.AddControllersWithViews();
              
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            // Admin Area

            #region Overviews routes
            // Sportproducts
            app.MapControllerRoute(
                name: "SportproductsAdminOverview",
                pattern: "{area:exists}/Overview/Sportproducts",
                defaults: new  { Controller = "SportProducts", Action = "SportproductsAdminOverview" } 
                );

            // Brands
            app.MapControllerRoute(
                name: "BrandsAdminOverview",
                pattern: "{area:exists}/Overview/Brands",
                defaults: new { Controller = "Brand", Action = "BrandsAdminOverview" }
                );

            // Categories
            app.MapControllerRoute(
                name: "CategoriesAdminOverview",
                pattern: "{area:exists}/Overview/Categories",
                defaults: new { Controller = "Category", Action = "CategoriesAdminOverview" }
                );

            // Features
            app.MapControllerRoute(
                name: "FeaturesAdminOverview",
                pattern: "{area:exists}/Overview/Features",
                defaults: new { Controller = "Feature", Action = "FeaturesAdminOverview" }
                );
            #endregion

            #region Add functions routes
            app.MapControllerRoute(
               name: "AddSportproduct",
               pattern: "{area:exists}/CRUD/AddSportProduct",
               defaults: new { Controller = "SportProducts", Action = "AddSportproduct" }
               );

            app.MapControllerRoute(
                name: "AddBrand",
               pattern: "{area:exists}/CRUD/AddBrand",
                defaults: new { Controller = "Brand", Action = "AddBrand" }
                );

            app.MapControllerRoute(
                name: "AddCategory",
               pattern: "{area:exists}/CRUD/AddCategory",
                defaults: new { Controller = "Category", Action = "AddCategory" }
                );

            
            app.MapControllerRoute(
                name: "AddFeature",
                pattern: "{area:exists}/CRUD/AddFeature",
                defaults: new { Controller = "Feature", Action = "AddFeature" }
                );
            #endregion

            #region Update functions routes
            app.MapControllerRoute(
               name: "UpdateSportproduct",
               pattern: "{area:exists}/CRUD/UpdateSportproduct/{id:int}",
               defaults: new { Controller = "SportProducts", Action = "UpdateSportproduct" }
               );

            app.MapControllerRoute(
                name: "UpdateBrand",
               pattern: "{area:exists}CRUD/UpdateBrand/{id:int}",
                defaults: new { Controller = "Brand", Action = "UpdateBrand" }
                );

            app.MapControllerRoute(
                name: "UpdateCategory",
               pattern: "{area:exists}/CRUD/UpdateCategory/{id:int}",
                defaults: new { Controller = "Category", Action = "UpdateCategory" }
                );


            app.MapControllerRoute(
                name: "UpdateFeature",
                pattern: "{area:exists}/CRUD/UpdateFeature{id:int}",
                defaults: new { Controller = "Feature", Action = "UpdateFeature" }
                );
            #endregion

            #region Delete functions routes
            #region 1. Confirm Delete
            app.MapControllerRoute(
               name: "ConfirmDeleteSportproduct",
               pattern: "{area:exists}/CRUD/ConfirmDeleteSportproduct/{id:int}",
               defaults: new { Controller = "SportProducts", Action = "ConfirmDeleteSportproduct" }
               );

            app.MapControllerRoute(
                name: "ConfirmDeleteBrand",
               pattern: "{area:exists}/CRUD/ConfirmDeleteBrand/{id:int}",
                defaults: new { Controller = "Brand", Action = "ConfirmDeleteBrand" }
                );

            app.MapControllerRoute(
                name: "ConfirmDeleteCategory",
               pattern: "{area:exists}/CRUD/ConfirmDeleteCategory/{id:int}",
                defaults: new { Controller = "Category", Action = "ConfirmDeleteCategory" }
                );


            app.MapControllerRoute(
                name: "ConfirmDeleteFeature",
                pattern: "{area:exists}/CRUD/ConfirmDeleteFeature{id:int}",
                defaults: new { Controller = "Feature", Action = "ConfirmDeleteFeature" }
                );
            #endregion

            #region 2. Delete functions
            app.MapControllerRoute(
              name: "DeleteSportproduct",
              pattern: "{area:exists}/CRUD/DeleteSportproduct",
              defaults: new { Controller = "SportProducts", Action = "DeleteSportproduct" }
              );

            app.MapControllerRoute(
                name: "DeleteBrand",
               pattern: "{area:exists}/CRUD/DeleteBrand",
                defaults: new { Controller = "Brand", Action = "DeleteBrand" }
                );

            app.MapControllerRoute(
                name: "DeleteCategory",
               pattern: "{area:exists}/CRUD/DeleteCategory",
                defaults: new { Controller = "Category", Action = "DeleteCategory" }
                );


            app.MapControllerRoute(
                name: "DeleteFeature",
                pattern: "{area:exists}/CRUD/DeleteFeature",
                defaults: new { Controller = "Feature", Action = "DeleteFeature" }
                );

            #endregion
            #endregion

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );

            //Public area
            // Home routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}") // Standaard geldt dit voor elke controller
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "privacy",
                pattern: "privacy",
                defaults: new { controller = "Home", action = "Privacy" });

            // Brand routes
            app.MapControllerRoute(
                name: "brandIndex",
                pattern: "brand",
                defaults: new { controller = "Brand", action = "Index" });

            app.MapControllerRoute(
                name: "brandProducts",
                pattern: "brand/{brandKey}",
                defaults: new { controller = "Brand", action = "ShowBrandProducts" });

            // Category routes
            app.MapControllerRoute(
                name: "categoryIndex",
                pattern: "category",
                defaults: new { controller = "Category", action = "Index" });

            app.MapControllerRoute(
                name: "categoryProducts",
                pattern: "category/{categoryKey}",
                defaults: new { controller = "Category", action = "ShowCategoryProducts" });

            // Feature routes
            app.MapControllerRoute(
               name: "featureIndex",
               pattern: "feature",
               defaults: new { controller = "Feature", action = "Index" });

            app.MapControllerRoute(
                name: "attributeProducts",
                pattern: "feature/{attributeKey}",
                defaults: new { controller = "Feature", action = "ShowAttributeProducts" });

            // Search routes
            app.MapControllerRoute(
                name: "productsUnder10",
                pattern: "search/under10",
                defaults: new { controller = "Search", action = "ProductsUnder10Euros" });

            app.MapControllerRoute(
                name: "productsBetween10And50",
                pattern: "search/10to50",
                defaults: new { controller = "Search", action = "ProductsBetween10And50Euros" });

            app.MapControllerRoute(
                name: "productsBetween50And100",
                pattern: "search/50to100",
                defaults: new { controller = "Search", action = "ProductsBetween50And100Euros" });

            app.MapControllerRoute(
                name: "productsAbove100",
                pattern: "search/above100",
                defaults: new { controller = "Search", action = "ProductsAbove100Euro" });

            // De extra routes
            app.MapControllerRoute(
                name: "searchBrandCategory",
                pattern: "search/productsByBrandAndCategory/{brandKey}/{categoryKey}",
                defaults: new { controller = "Search", action = "SearchByBrandAndCategory" });

            app.MapControllerRoute(
                name: "searchColorAudience",
                pattern: "search/productsByColorAndAudience/{colorKey}/{audienceKey}",
                defaults: new { controller = "Search", action = "SearchByColorAndAudience" });

            app.MapControllerRoute(
                name: "searchPriceRange",
                pattern: "search/productsByPriceRange/{min:decimal}/{max:decimal}",
                defaults: new { controller = "Search", action = "SearchByPriceRange" });

            // SportProducts routes
            app.MapControllerRoute(
                name: "sportProductsIndex",
                pattern: "sportproducts",
                defaults: new { controller = "SportProducts", action = "Index" });

            app.MapControllerRoute(
                name: "sportProductInfo",
                pattern: "sportproduct/{id}",
                defaults: new { controller = "SportProducts", action = "ShowProductInfo" });
            app.Run();
        }
    }
}