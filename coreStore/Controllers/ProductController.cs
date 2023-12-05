using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace coreStore.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {

        ProductManager pm = new ProductManager(new EfProductRepository());
        Context c = new Context();

        public IActionResult Index()
        {
            var values = pm.GetList();
            return View(values);
        }

        public IActionResult ProductInfo(int id)
        {

            var values = pm.GetProductById(id);
            values = c.Products.Include(x => x.AdditionalImages).Include(p => p.ProductDetails).FirstOrDefault(p => p.ProductID == id);

            return View(values);
        }

        public IActionResult WomenProducts()
        {
            string gender = "Kadın";
            var values = pm.GetListByGender(gender);
            return View(values);
        }

        public IActionResult ManProducts()
        {
            string gender = "Erkek";
            var values = pm.GetListByGender(gender);
            return View(values);
        }

        
        public IActionResult LikeProduct(int productId)
        {
            // Kullanıcı giriş yapmış mı kontrol et
            if (User.Identity.IsAuthenticated)
            {
                // Kullanıcının beğenilerini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Kullanıcının bu ürünü daha önce beğenip beğenmediğini kontrol et
                var likedProduct = c.LikedProducts
                    .FirstOrDefault(lp => lp.UserID == userId && lp.ProductID == productId);

                if (likedProduct == null)
                {
                    // Kullanıcı daha önce bu ürünü beğenmediyse, beğenilenlere ekle
                    var newLikedProduct = new LikedProduct
                    {
                        UserID = userId,
                        ProductID = productId
                    };

                    c.LikedProducts.Add(newLikedProduct);
                    c.SaveChanges();

                    return Json(new { success = true, message = "Ürün beğenildi!" });
                }

                return Json(new { success = false, message = "Bu ürünü zaten beğendiniz!" });
            }

            // Kullanıcı giriş yapmamışsa, login sayfasına yönlendir
            return RedirectToAction("Login", "Login");
        }

    }
}

