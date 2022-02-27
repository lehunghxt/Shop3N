namespace Shop.Service.Cache
{
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class UserCachingProvider : RedisConnectorBase
    {
        private string InvoiceKey = "Invoice|{0}";
        private string InvoiceMessageKey = "InvoiceMessage|{0}";
        #region Singleton 
        private static UserCachingProvider instance;

        private UserCachingProvider() { }

        public static UserCachingProvider Instance
        {
            get
            {
                if (instance == null)
                    return new UserCachingProvider();
                return instance;
            }
        }
        #endregion

        public virtual string GetItem(string key)
        {
            return base.GetValue(key);
        }
        public string[] GetUserRoles(int Id)
        {
            var value = GetItem($"UserRoles|{Id}")?.Split(',');
            if (value != null && value.Count() > 0)
                CacheRoles(Id, string.Join(",",value));
            return value;
        }
        public void CacheRoles(int Id, string roles)
        {
            string key = $"UserRoles|{Id}";
            SetValue(key, roles);
        }
        public void ResetAllRoles()
        {
            DeleteKeyLike("UserRoles|*");
        }
    }
}
