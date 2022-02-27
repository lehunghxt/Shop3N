using Shop.Domain;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Cache
{
    public class ConfigCachingProvider : RedisConnectorBase
    {
        private static readonly string ConfigKey = "ConfigDefault";
        private static readonly string ConfigCustomerKey = "ConfigCustomer|{0}";
        #region Singleton 
        private static ConfigCachingProvider instance;

        private ConfigCachingProvider() { }

        public static ConfigCachingProvider Instance
        {
            get
            {
                if (instance == null)
                    return new ConfigCachingProvider();
                return instance;
            }
        }
        #endregion
    }
}
