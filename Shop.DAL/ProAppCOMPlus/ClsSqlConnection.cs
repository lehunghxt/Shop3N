using System;
using System.Data.SqlClient;
using System.Configuration;
namespace Shop.DAL.ProAppCOMPlus
{
    /// <summary>
    /// Summary description for ClsSqlConnection
    /// </summary>
    public class ClsSqlConnection : IDisposable
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        private string strConnection = String.Empty;
        private bool disposed = false;
        /// <summary>
        /// Create object connection
        /// </summary>
        /// <param name="strConnection">string connection</param>
        public ClsSqlConnection(string strConnection)
        {
            this.strConnection = strConnection;
            try
            {
                if (strConnection.Length != 0 && sqlConnection == null)
                    sqlConnection = new SqlConnection(strConnection);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        //public ClsSqlConnection()
        //{
            
        //    try
        //    {
        //        if (strConnection.Length == 0 && sqlConnection == null)
        //        {
        //            this.strConnection = ConfigurationSettings.AppSettings["ILoveUConnection"];
        //            sqlConnection = new SqlConnection(strConnection); 
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }

        //}
        ~ClsSqlConnection()
        {
            Dispose(false);
        }
        /// <summary>
        /// Open connection
        /// </summary>
        /// <returns>true if Open successfully</returns>
        public void SqlOpenConnection()
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    this.sqlConnection.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get command
        /// </summary>
        /// <returns>SqlCommand </returns>
        public SqlCommand GetSqlCommand()
        {
            try
            {
                if (sqlCommand == null)
                    sqlCommand = new SqlCommand(null, sqlConnection);
                return this.sqlCommand;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Dispose();
            }
        }
        /// <summary>
        /// Close connection
        /// </summary>
        /// <returns></returns>
        public void SqlCloseConnection()
        {
            try
            {
                if (sqlConnection != null)
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }



        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: free managed resources here
                    if (null != sqlConnection)
                        sqlConnection.Dispose();
                    if (null != sqlCommand)
                        sqlCommand.Dispose();
                }
                // TODO: free unmanaged resources here.
                // Set disposed flag:
            }
            disposed = true;
        }

        #endregion
    }
}