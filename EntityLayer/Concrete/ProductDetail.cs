using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductDetail
    {
        [Key]
        public int ProductDetailID { get; set; }
        public short ProductStock { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductDetailStatus { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
