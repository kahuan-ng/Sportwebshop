using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.PE02_Webshop.Core.Entities
{
    public class SportProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0 , (double)decimal.MaxValue)] // Data annotation om een bereik te zetten
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
        [Range(0, 5)] // Review range
        public int? Rating { get; set; }

        // Relaties opzetten met andere entiteiten
        public Brand Brand { get; set; }

        public int? BrandId { get; set; }

        public Category Type { get; set; }
        public int? CategoryId { get; set; }

        public ICollection<Feature> Features { get; set; }
    }
}
