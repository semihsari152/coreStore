using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class LikedProduct
    {
        [Key]
        public int LikedProductID { get; set; }
        public string UserID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
