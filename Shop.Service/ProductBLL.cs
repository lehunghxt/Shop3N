using System.Collections.Generic;
using Shop.Domain;
using System.Data;
using Shop.DAL;
using System.Linq;
using System;
using System.Data.Entity;
using Shop.Service.Cache;

namespace Shop.Service
{
    public class ProductBLL : BLLBase
    {

        private ItblProductDAL _productDAL;

        public ProductBLL(string connectionString = "") : base(connectionString)
        {
            _productDAL = new tblProductDAL(this.DatabaseFactory);
        }

        public List<ProductModel> ListProduct()
        {
            var query = _productDAL.GetAll().ToList();
            var data = mapper.Map<List<tblProduct>, List<ProductModel>>(query);
            return data;
        }
        public ProductModel GetProductById(long id)
        {
            var query = _productDAL.GetAll().FirstOrDefault(k => k.Id == id);
            var data = mapper.Map<tblProduct, ProductModel>(query);
            return data;
        }
        public long AddProduct(ProductModel data)
        {
            var product = mapper.Map<ProductModel, tblProduct>(data);
            _productDAL.Add(product);
            this.SaveChanges();
            return product.Id;
        }
        public long UpdateProduct(ProductModel data)
        {
            var product = mapper.Map<ProductModel, tblProduct>(data);
            _productDAL.Update(product);
            this.SaveChanges();
            return product.Id;
        }
        public void DeleteProduct(long id)
        {
            var query = _productDAL.GetAll().FirstOrDefault(k => k.Id == id);
            if (query == null) throw new BusinessException("Không tìm thấy sản phẩm");
            _productDAL.Delete(query);
            this.SaveChanges();
        }

    }
}