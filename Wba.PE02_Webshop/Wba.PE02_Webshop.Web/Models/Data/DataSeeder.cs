using Microsoft.EntityFrameworkCore;
using Wba.PE02_Webshop.Core.Entities;

namespace Wba.PE02_Webshop.Web.Models.Data
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var sportProducts = new SportProduct[]
            {
                new SportProduct
                {
                    Id = 1,
                    Name = "Adidas Men Hoody",
                    Price = 41.99m,
                    ProductImage = "Clothing/WBA_PE01_addidas_sporthoodie.png",
                    Rating = 4,
                    BrandId = 2,
                    CategoryId = 1,
                },

                new SportProduct
                {
                    Id = 2,
                    Name = "PUMA Woman Leggings",
                    Price = 29.99m,
                    ProductImage = "Clothing/Wba_PE01_img_broek.png",
                    Rating = 4,
                    BrandId = 3,
                    CategoryId = 1
                },

                new SportProduct
                {
                    Id = 3,
                    Name = "Nike Running Shoes",
                    Price = 199.99m,
                    ProductImage = "Clothing/WBA_PE01_img_nikeRunningShoes.png",
                    Rating = 4,
                    BrandId = 1,
                    CategoryId = 1
                },

                new SportProduct
                {
                    Id = 4,
                    Name = "Naak Boost Energygel",
                    Price = 1.99m,
                    ProductImage = "Nutrition/WBa_PE01_img_energiegel.png",
                    Rating = 1,
                    BrandId = 9,
                    CategoryId = 2
                },

                 new SportProduct
                {
                    Id = 5,
                    Name = "Lucivaal 400mg magnesiums pills",
                    Price = 7.99m,
                    ProductImage = "Nutrition/WBA_PE01_img_magnesiumpillen.png",
                    Rating = 4,
                    BrandId = 10,
                    CategoryId = 2

                },

                new SportProduct
                {
                    Id = 6,
                    Name = "Optimum Nutrition Gold Standard 2.27kg Whey Protein Powder",
                    Price = 63.99m,
                    ProductImage = "Nutrition/WBA_PE01_img_proteinpowder.png",
                    Rating = 5,
                    BrandId = 11,
                    CategoryId = 2
                },

                new SportProduct
                {
                    Id = 7,
                    Name = "5kg dumbbell",
                    Price = 9.99m,
                    ProductImage = "Equipment/WBA_PE01_img_db.png",
                    Rating = 3,
                    BrandId = 12,
                    CategoryId = 3
                },

                new SportProduct
                {
                    Id = 8,
                    Name = "Muscle Massage Gun",
                    Price = 81.99m,
                    ProductImage = "Equipment/WBa_PE01_img_massagemachine.png",
                    Rating = 5,
                    BrandId = 13,
                    CategoryId = 3
                },

                new SportProduct
                {
                    Id = 9,
                    Name = "2.7m jumprope",
                    Price = 4.99m,
                    ProductImage = "Equipment/WBa_PE01_img_springtouw.png",
                    Rating = 3,
                    BrandId = 14,
                    CategoryId = 3
                },
            };

            var brands = new Brand[]
            {
                new Brand { Id = 1, BrandName = "Nike", BrandLogo = "nike-logo.png" },
                new Brand { Id = 2, BrandName = "Adidas", BrandLogo = "Adidas_Logo.png" },
                new Brand { Id = 3, BrandName = "Puma", BrandLogo = "puma-logo.png" },
                new Brand { Id = 4, BrandName = "Reebok", BrandLogo = "Reebok-logo.png" },
                new Brand { Id = 5, BrandName = "Under Armour", BrandLogo = "UnderArmour-logo.png" },
                new Brand { Id = 6, BrandName = "New Balance", BrandLogo = "NewBalance-logo.png" },
                new Brand { Id = 7, BrandName = "Asics", BrandLogo = "Asics-logo.png" },
                new Brand { Id = 8, BrandName = "Fila", BrandLogo = "Fila-logo.png" },

                // Nutrition
                new Brand { Id = 9, BrandName = "Naak", BrandLogo = "Naak-logo.png" },
                new Brand { Id = 10, BrandName = "Lucavitaal", BrandLogo = "lucovitaal-logo.png" },
                new Brand { Id = 11, BrandName = "Optimum Nutrition", BrandLogo = "optimum-nutrition-logo.png" },

                // Equipment
                new Brand { Id = 12, BrandName = "Action", BrandLogo = "action-logo.png" },
                new Brand { Id = 13, BrandName = "Apherma", BrandLogo = "Apherma-Logo.png" },
                new Brand { Id = 14, BrandName = "Fitness Speed", BrandLogo = "fitnessspeed-logo.png" },

            };

            var categories = new Category[]
            {
                new Category { Id = 1, Type = "Clothing" },
                new Category { Id = 2, Type = "Nutrition" },
                new Category { Id = 3, Type = "Equipment" }
            };

            var features = new Feature[]
            {
                new Feature { Id = 1, Attribute = "Black", AttributeCategory = "Color" },
                new Feature { Id = 2, Attribute = "White", AttributeCategory = "Color" },
                new Feature { Id = 3, Attribute = "Red", AttributeCategory = "Color" },
                new Feature { Id = 4, Attribute = "Blue", AttributeCategory = "Color" },
                new Feature { Id = 5, Attribute = "Green", AttributeCategory = "Color" },
                new Feature { Id = 6, Attribute = "Pink", AttributeCategory = "Color" },
                new Feature { Id = 7, Attribute = "Purple", AttributeCategory = "Color" },
                new Feature { Id = 8, Attribute = "Orange", AttributeCategory = "Color" },
                new Feature { Id = 9, Attribute = "Yellow", AttributeCategory = "Color" },

                // Sizes
                new Feature { Id = 10, Attribute = "XS", AttributeCategory = "Size" },
                new Feature { Id = 11, Attribute = "S", AttributeCategory = "Size" },
                new Feature { Id = 12, Attribute = "M", AttributeCategory = "Size" },
                new Feature { Id = 13, Attribute = "L", AttributeCategory = "Size" },
                new Feature { Id = 14, Attribute = "XL", AttributeCategory = "Size" },
                new Feature { Id = 15, Attribute = "XXL", AttributeCategory = "Size" },

                // Target Audiences
                new Feature { Id = 16, Attribute = "Male", AttributeCategory = "Audience" },
                new Feature { Id = 17, Attribute = "Female", AttributeCategory = "Audience" },
                new Feature { Id = 18, Attribute = "Kids", AttributeCategory = "Audience" },

                // Nutrition features
                new Feature {Id = 19, Attribute = "Carbohydrates", AttributeCategory="Nutrition"},
                new Feature {Id = 20, Attribute = "Magnesium", AttributeCategory="Nutrition"},
                new Feature {Id = 21, Attribute = "Protein", AttributeCategory="Nutrition"},
                new Feature {Id = 22, Attribute = "Protein Powder", AttributeCategory="Nutrition"},
                new Feature {Id = 28, Attribute = "Supplements", AttributeCategory="Nutrition"},

                // Equipment features
                new Feature {Id = 23, Attribute = "Weights", AttributeCategory="Equipment"},
                new Feature {Id = 24, Attribute = "Machine", AttributeCategory="Equipment"},
                new Feature {Id = 25, Attribute = "Recovery", AttributeCategory="Equipment"},
                new Feature {Id = 26, Attribute = "Sport Equipment", AttributeCategory="Equipment"},
                new Feature {Id = 27, Attribute = "Cardio", AttributeCategory="Equipment"},

                new Feature {Id = 29, Attribute = "Promo", AttributeCategory="Promo"},
            };

            var sportProductFeatures = new[]
            {
                new { SportProductsId = 1, FeaturesId = 1 },
                new  { SportProductsId = 1, FeaturesId = 12 },
                new  { SportProductsId = 1, FeaturesId = 16 },
                new  { SportProductsId = 2, FeaturesId = 6 },
                new  { SportProductsId = 2, FeaturesId = 10 },
                new  { SportProductsId = 2, FeaturesId = 17 },
                new  { SportProductsId = 2, FeaturesId = 29 },
                new  { SportProductsId = 3, FeaturesId = 2 },
                new  { SportProductsId = 3, FeaturesId = 6 },
                new  { SportProductsId = 3, FeaturesId = 9 },
                new  { SportProductsId = 3, FeaturesId = 10 },
                new  { SportProductsId = 3, FeaturesId = 18 },
                new  { SportProductsId = 4, FeaturesId = 19 },
                new  { SportProductsId = 4, FeaturesId = 28 },
                new  { SportProductsId = 5, FeaturesId = 20 },
                new  { SportProductsId = 5, FeaturesId = 28 },
                new  { SportProductsId = 6, FeaturesId = 21 },
                new  { SportProductsId = 6, FeaturesId = 22 },
                new  { SportProductsId = 7, FeaturesId = 24 },
                new  { SportProductsId = 7, FeaturesId = 28 },
                new  { SportProductsId = 8, FeaturesId = 24 },
                new  { SportProductsId = 8, FeaturesId = 25 },
                new  { SportProductsId = 8, FeaturesId = 27 },
                new  { SportProductsId = 9, FeaturesId = 26 },
            };

            // Data invoegen in database
            modelBuilder.Entity<SportProduct>().HasData(sportProducts);
            modelBuilder.Entity<Brand>().HasData(brands);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Feature>().HasData(features);
            modelBuilder.Entity($"{nameof(Feature)}{nameof(SportProduct)}").HasData(sportProductFeatures);
        }
    }
}