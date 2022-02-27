namespace Shop.Service.Cache
{
    public interface ICachingProvider
    {
        void AddItem(string key, object value);
        string GetItem(string key);
        void DeleteItem(string key);
    }
}