namespace Shop.Service
{
    using System;

    using Shop.DAL;
    using Shop.DAL.Infrastructure;

    using log4net;
    using AutoMapper;
    using Mapping;
    using System.Data.Entity.Infrastructure;
    using System.Text;
    using Library;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BLLBase
    {
        private string nat = ConfigurationSettings.AppSettings["NATPhysic"];

        private readonly IUnitOfWork _unitOfWork;

        private string _connectionString;
        private string ConnectionString
        {
            get { return this._connectionString ?? CGlobal.ConnectionString; }
            set { this._connectionString = value; }
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(BLLBase));

        public static IMapper mapper; 

        protected IDatabaseFactory<ShopEntities> DatabaseFactory { get; private set; }

        public DateTime Now
        {
            get {
                return _unitOfWork.Now;
            }
        }

        protected void DisposeCore()
        {
            if (this._unitOfWork != null) this._unitOfWork.Dispose();
            //base.DisposeCore();
        }

        protected bool CatchError(Exception ex)
        {
            this.RollBack();
            throw ex;
        }

        protected BLLBase(IUnitOfWork unitOfWork)
        {
            this.DatabaseFactory = unitOfWork.DatabaseFactory as IDatabaseFactory<ShopEntities>;
            this._unitOfWork = unitOfWork;
        }

        protected BLLBase(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.ConnectionString = connectionString;
            }

            this.ConnectionString = string.Format("metadata=res://*/InvoiceData.csdl|res://*/InvoiceData.ssdl|res://*/InvoiceData.msl;provider=System.Data.SqlClient;provider connection string='{0};MultipleActiveResultSets=True;App=EntityFramework;'", this.ConnectionString);
            this.DatabaseFactory = new DatabaseFactory<ShopEntities>(this.ConnectionString);
            this.DatabaseFactory.Get().Database.Log = i => log.Info(i);
            this._unitOfWork = new UnitOfWork(this.DatabaseFactory);

            if (!MappingProfile.Map) mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
        }

        protected BLLBase()
            : this((string)null)
        {
        }

        protected List<T> Query<T>(string queryString)
        {
            return this.Query<T>(queryString, null);
        }

        /// <summary>
        /// Query SQL string
        /// </summary>
        /// <typeparam name="T">return Data Type</typeparam>
        /// <param name="queryString">query string</param>
        /// <param name="parameters">
        /// context.Database.SqlQuery<EntityType>(
        /// "EXEC ProcName @param1, @param2", 
        /// new SqlParameter("param1", param1), 
        /// new SqlParameter("param2", param2)
        /// </param>
        /// <returns></returns>
        protected List<T> Query<T>(string queryString, params object[] parameters)
        {
            try
            {
                if (parameters == null)
                {
                    var query = this.DatabaseFactory.Get().Database.SqlQuery<T>(queryString);
                    var data = query.ToList();
                    return data;
                }
                else
                {
                    var query = this.DatabaseFactory.Get().Database.SqlQuery<T>(queryString, parameters);
                    var data = query.ToList();
                    return data;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
                return new List<T>();
            }
        }

        ~BLLBase()
        {
            DisposeCore();
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public void BeginWork()
        {
            this._unitOfWork.BeginTransaction();
        }

        protected void SaveChanges()
        {
            try
            {
                this._unitOfWork.Commit();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var error = string.Empty;
                foreach (var eve in ex.EntityValidationErrors)
                {
                    error = string.Format(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                    log.Error(error);
                    CGlobal.writelog(nat + "/logfile/", error);
                }

                throw new BusinessException(error);
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {ex?.InnerException?.InnerException?.Message}");

                foreach (var eve in ex.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }

                log.Error(sb.ToString());
                CGlobal.writelog(nat + "/logfile/", sb.ToString());

                throw new BusinessException(sb.ToString());
            }
            catch (Exception ex)
            {
                CGlobal.writelog(nat + "/logfile/", ex.TraceInformation());
                throw new Exception(ex.Message, ex);
            }
        }

        public void RollBack()
        {
            this._unitOfWork.RollBackTransaction();
        }

        public enum Order
        {
            /// <summary>
            /// Order Up
            /// </summary>
            Up,

            /// <summary>
            /// Order Down
            /// </summary>
            Down
        }
    }
}
