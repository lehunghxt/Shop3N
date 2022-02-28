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
                product.UserId = CurrentUser.Id;
                product.CreateBy = (int)CurrentUser.Id;
                product.CreateDate = _productBLL.Now;
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
        // GET: Auth/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productBLL.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(int id, ProductModel product)
        {
            try
            {
                _productBLL.UpdateProduct(product);
                ViewBag.Succes = "Cập nhật thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = _productBLL.GetProductById(id);
            return View(product);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = _productBLL.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(int id, ProductModel product)
        {
            try
            {
                _productBLL.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return View();
            }
        }

    }
}