namespace Shop.DAL.Infrastructure
{
    using System;
    using System.Data;

    public interface IUnitOfWork : IDisposable
    {
        IDatabaseFactory DatabaseFactory { get; }

        IDbConnection Connection { get; }

        DateTime Now { get; }

        void BeginTransaction();
        void Commit();
        void RollBackTransaction();
    }
}
