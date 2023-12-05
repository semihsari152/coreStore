using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductDetail { get; set; }
        public string ProductFeatures { get; set; }
        public string MainProductImage { get; set; } // Ana görsel için alan
        public List<ProductImage> AdditionalImages { get; set; } // Diğer görseller için liste
        public decimal ProductPrice { get; set; }
        public string ProductGender { get; set; }
        public DateTime ProductCreateDate { get; set; }
        public DateTime ProductUpdateDate { get; set; }
        public bool ProductFreeShippingInfo { get; set; }

        public int SubCategoryID { get; set; }
        public SubCategory SubCategory { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
        public ICollection<LikedProduct> LikedProducts { get; set; }

    }
}
