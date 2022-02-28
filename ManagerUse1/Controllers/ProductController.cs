using Shop.Domain;
using Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerUse1.Controllers
{
    public class ProductController : BaseController
    {
        private ProductBLL _productBLL;
        public ProductController()
        {
            _productBLL = new ProductBLL();
        }
        // GET: Product
        public ActionResult Index()
        {
            var products = _productBLL.ListProduct();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            try
            {
                _productBLL.AddProduct(product);
                ViewBag.Success = "Đã thêm sản phẩm.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                var product = _productBLL.GetProductById(Id);
                return View(product);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ProductModel product)
        {
            try
            {
                _productBLL.UpdateProduct(product);
                ViewBag.Succes = "Cập nhật thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}