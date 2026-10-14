using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.PE02_Webshop.Core.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string Attribute { get; set; }
        public string AttributeCategory { get; set; }
        public ICollection<SportProduct> SportProducts { get; set; }

    }
}
