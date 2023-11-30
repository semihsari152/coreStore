using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.Controllers
{
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
    }
}
