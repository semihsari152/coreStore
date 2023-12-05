using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly List<CartItem> _cartItems;

        public CartManager()
        {
            _cartItems = new List<CartItem>();
        }

        public void AddToCart(int productId, string size, string color, int quantity)
        {
            // Bu noktada ürünü veritabanından alabilir veya başka bir kaynaktan temin edebilirsiniz
            // Burada sadece örnek olması için basit bir şekilde ekliyoruz
            _cartItems.Add(new CartItem
            {
                ProductId = productId,
                Size = size,
                Color = color,
                Quantity = quantity
            });
        }

        public CartItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        public List<CartItem> GetList()
        {
            throw new NotImplementedException();
        }

        public void TAdd(CartItem t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(CartItem t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(CartItem t)
        {
            throw new NotImplementedException();
        }
    }
}
