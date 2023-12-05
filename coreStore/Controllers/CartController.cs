using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.Controllers
{
    public class PayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
