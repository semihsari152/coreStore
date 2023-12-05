using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
