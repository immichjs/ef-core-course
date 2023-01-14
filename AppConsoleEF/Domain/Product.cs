using AppConsoleEF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleEF.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string CodeBar { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public bool Status{ get; set; }
    }
}
