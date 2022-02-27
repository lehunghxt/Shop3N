
namespace Einvoince.Service.Cache
{
    using DAL;
    using Domain;
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceCachingProvider : RedisConnectorBase, ICachingProvider
    {
        private string InvoiceKey = "Invoice|{0}";
        private string InvoiceMessageKey = "InvoiceMessage|{0}";
        #region Singleton 
        private static InvoiceCachingProvider instance;

        private InvoiceCachingProvider() { }

        public static InvoiceCachingProvider Instance
        {
            get
            {
                if (instance == null)
                    return new InvoiceCachingProvider();
                return instance;
            }
        }
        #endregion
        public virtual void AddItem(string key, object value)
        {
            TimeSpan? duration = null;

            if (value.GetType() == typeof(string)) base.SetValue(key, value.ToString(), duration);
            else
            {
                var json = JsonHelper.SerializeObject(value);
                base.SetValue(key, json, duration);
            }
        }

        public T GetItem<T>(string key) where T: class
        {
            var res = base.GetValue(key);
            if (!string.IsNullOrWhiteSpace(res))
                return JsonHelper.DeserializeObject<T>(res);
            return default(T);
        }

        public virtual string GetItem(string key)
        {
            return base.GetValue(key);
        }
        public virtual void DeleteItem(string key)
        {
            base.DeleteKey(key);
        }

        public new bool CheckKey(string key)
        {
            return base.CheckKey(key);
        }
        
        public void CacheInvoiceMessage(int Id, InvoiceMessage mes)
        {
            AddItem(string.Format(InvoiceMessageKey, Id), mes);
        }
        public InvoiceMessage GetCacheInvoiceMessage(long Id)
        {
            return GetItem<InvoiceMessage>(string.Format(InvoiceMessageKey, Id));
        }
        public List<tblIvoice> GetInvoiceListCache(List<tblIvoice> models)
        {
            models = models.Select(e =>
            {
                if (e.InvoiceMessage == null)
                {
                    e.InvoiceMessage = GetCacheInvoiceMessage(e.Id);
                }
                return e;
            }).ToList();
            return models;
        }
        public InvoiceModel GetCacheInvoice(long Id)
        {
            return GetItem<InvoiceModel>(string.Format(InvoiceKey, Id));
        }
        public void CacheInvoice(long Id, InvoiceModel invoice)
        {
            AddItem(string.Format(InvoiceKey, Id), invoice);
        }
    }
}
