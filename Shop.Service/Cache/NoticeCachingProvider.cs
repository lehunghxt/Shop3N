
namespace Einvoince.Service.Cache
{
    using DAL;
    using Domain;
    using Library;
    using System;
    using System.Collections.Generic;

    public class NoticeCachingProvider : RedisConnectorBase, ICachingProvider
    {
        #region Singleton 
        private static NoticeCachingProvider instance;

        private NoticeCachingProvider() { }

        public static NoticeCachingProvider Instance
        {
            get
            {
                if (instance == null)
                    return new NoticeCachingProvider();
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
            if (res != null)
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
        
        public void CacheTaxId(int Id, InvoiceMessage mes)
        {

        }
        public int GetInvoiceNumber(int NoticeID)
        {
            CacheNoticeModel notice = GetItem<CacheNoticeModel>($"Notice|{NoticeID.ToString()}");

            if (notice != null)
            {
                notice.CurrentNumber += 1;
                if (notice.CurrentNumber == notice.Max)
                {
                    new NoticeBLL().UpdateCurrentNumber(notice.Id, notice.Max, true);
                    DeleteItem($"Notice|{notice.Id.ToString()}");
                    return notice.CurrentNumber;
                }
                return notice.CurrentNumber;
            }
            
            return 0;
        }
        public void UpdateInvoiceNumber(CacheNoticeModel notice)
        {
            AddItem($"Notice|{notice.Id.ToString()}", notice);
        }

        public List<NoticeCustomerModel>GetCacheNotices(int customerId)
        {
            return GetItem<List<NoticeCustomerModel>>($"NoticeCustomer|{customerId}");
        }
        public void AddListNotice(int customerId, List<NoticeCustomerModel> notices)
        {
            AddItem($"NoticeCustomer|{customerId}", notices);
        }

        public class CacheNoticeModel
        {
            public int Id { get; set; }
            public int CurrentNumber { get; set; }
            public int Max { get; set; }
        }
    }
}
