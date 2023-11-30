using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.ViewComponents
{
    public class HomePageDiscountCountdown :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
