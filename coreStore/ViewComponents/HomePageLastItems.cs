using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace coreStore.ViewComponents
{
    public class HomePageLastItems : ViewComponent
    {
        Context c = new Context();
       

        public IViewComponentResult Invoke()
        {
            var values = c.Products.OrderBy(x => x.ProductCreateDate).Skip(Math.Max(0, c.Products.Count() - 3)).ToList();

            return View(values);
        }
    }
}
