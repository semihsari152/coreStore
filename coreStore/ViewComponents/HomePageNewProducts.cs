using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.ViewComponents
{
    public class HomePageNewProducts : ViewComponent
    {
        Context c = new Context();


        public IViewComponentResult Invoke()
        {
            var values = c.Products.OrderByDescending(x => x.ProductCreateDate).Skip(Math.Max(0, c.Products.Count() - 8)).ToList();

            return View(values);
        }
    }
}
