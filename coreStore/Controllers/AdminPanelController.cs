using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace coreStore.Controllers
{
    public class AdminPanelController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductRepository());
        ProductDetailManager pdm = new ProductDetailManager(new EfProductDetailRepository());
        SubCategoryManager scm = new SubCategoryManager(new EfSubCategoryRepository());
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyProducts()
        {
            var values = pm.GetList();
            return View(values);
        }

        public IActionResult MyProductsDetails(int id)
        {
            var values = pm.GetProductById(id);
            values = c.Products.Include(p => p.ProductDetails).FirstOrDefault(p => p.ProductID == id);

            return View(values);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> subcategoryvalues = (from x in scm.GetList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.SubCategoryName,
                                                          Value = x.SubCategoryID.ToString()
                                                      }).ToList();
            ViewBag.pv = subcategoryvalues;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p, IFormFile MainProductImage, List<IFormFile> AdditionalImages)
        {
            if (MainProductImage != null && MainProductImage.Length > 0)
            {
                p.MainProductImage = ResmiKaydet(MainProductImage);
            }

            if (AdditionalImages != null && AdditionalImages.Count > 0)
            {
                p.AdditionalImages = new List<ProductImage>();

                for (int i = 0; i < Math.Min(AdditionalImages.Count, 3); i++)
                {
                    var ekResim = new ProductImage
                    {
                        ImagePath = ResmiKaydet(AdditionalImages[i])
                    };
                    p.AdditionalImages.Add(ekResim);
                }
            }


            p.ProductCreateDate = DateTime.Now;
            p.ProductUpdateDate = DateTime.Now;

            p.ProductFreeShippingInfo = Request.Form["ProductFreeShippingInfo"] == "on";
            
            pm.TAdd(p);

            return RedirectToAction("Index");
        }

        private string ResmiKaydet(IFormFile resim)
        {
            if (resim != null && resim.Length > 0)
            {
                // Resim için benzersiz bir dosya adı oluştur
                var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(resim.FileName);

                // Resmin kaydedileceği yolu belirt (ihtiyaca göre ayarla)
                var dosyaYolu = Path.Combine("wwwroot/images", dosyaAdi);

                // Resmi belirtilen yola kaydet
                using (var dosyaAkisi = new FileStream(dosyaYolu, FileMode.Create))
                {
                    resim.CopyTo(dosyaAkisi);
                }

                return dosyaAdi; // Veritabanında saklanacak dosya adını döndür
            }

            return null;
        }


        [HttpGet]
        public IActionResult AddProductStock(int ProductID)
        {
            TempData["TempProductID"] = ProductID;
            return View();
        } 

        [HttpPost]
        public IActionResult AddProductStock(ProductDetail pd,Product p)
        {
            pd.ProductID = (int)TempData["TempProductID"];
            p.ProductUpdateDate = DateTime.Now;
            pdm.TAdd(pd);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            List<SelectListItem> subcategoryvalues = (from x in scm.GetList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.SubCategoryName,
                                                          Value = x.SubCategoryID.ToString()
                                                      }).ToList();
            ViewBag.pv = subcategoryvalues;

            var value = pm.GetById(id);

            // Cinsiyet ve Ücretsiz Kargo bilgilerini view'a iletir
            if (value != null)
            {
                ViewBag.SelectedGender = value.ProductGender;
                ViewBag.SelectedFreeShipping = value.ProductFreeShippingInfo;
            }

            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
            pm.TUpdate(p);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id)
        {
            var value = pm.GetById(id);
            pm.TDelete(value);

            return RedirectToAction("Index");
        }
    }
}
