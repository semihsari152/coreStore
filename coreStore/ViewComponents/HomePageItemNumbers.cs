using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.ViewComponents
{
    public class HomePageItemNumbers : ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var value1 = c.Products.Count(x => x.ProductGender == "Erkek").ToString();
            ViewBag.v1 = value1;

            var value2 = c.Products.Count(x => x.ProductGender == "Kadın").ToString();
            ViewBag.v2 = value2;

            var value3 = c.Categories.Count(x => x.CategoryID == 4).ToString();
            ViewBag.v3 = value3;

            return View();
        }
    }
}
