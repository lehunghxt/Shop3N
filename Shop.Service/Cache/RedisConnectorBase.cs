using Library;
using StackExchange.Redis;
using System;

namespace Shop.Service
{
    public class RedisConnectorBase
    {
        private static IDatabase cache;
        private static readonly string ConnectionString = "192.168.10.3:6379";
        static RedisConnectorBase()
        {
            RedisConnectorBase.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConnectionString);
            });
            cache = RedisConnectorBase.Connection.GetDatabase();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }



        public string GetValue(string Key)
        {
            if (!CheckKey(Key)) return null;
            return cache.StringGet(Key);
        }

        public void SetValue(string Key, string Value, TimeSpan? Duration = null)
        {
            cache = RedisConnectorBase.Connection.GetDatabase();
            Duration = Duration != null ? Duration : TimeSpan.FromMinutes(60);
            cache.StringSet(Key, Value, Duration);
        }
        public void DeleteKey(string Key)
        {
            cache = RedisConnectorBase.Connection.GetDatabase();
            cache.KeyDelete(Key);
        }
        public bool CheckKey(string Key)
        {
            cache = RedisConnectorBase.Connection.GetDatabase();
            return cache.KeyExists(Key);
        }

        public T Get<T>(string Key)
        {
            if (CheckKey(Key))
                return JsonHelper.DeserializeObject<T>(GetValue(Key));
            return default(T);
        }
        protected void SetSystemValue(string Key, object Value)
        {
            cache = RedisConnectorBase.Connection.GetDatabase();
            if (Value.GetType() != typeof(string))
                cache.StringSet(Key, JsonHelper.SerializeObject(Value));
            else cache.StringSet(Key, Value.ToString());
        }
        protected void DeleteKeyLike(string pattern)
        {
            var server = RedisConnectorBase.Connection.GetServer(ConnectionString);
            if (server != null)
            {
                cache = RedisConnectorBase.Connection.GetDatabase();
                foreach (var key in server.Keys(pattern: pattern))
                    cache.KeyDelete(key);
            }
        }
    }
}