using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICartService : IGenericService<CartItem>
    {
        void AddToCart(int productId, string size, string color, int quantity);
        List<CartItem> GetCartItems();
    }
}
