using System;
using System.Data;
using System.Data.SqlClient;

namespace Shop.DAL.ProAppCOMPlus
{
/// <summary>
/// Summary description for ClsSqlGetDataValues
/// </summary>
[Serializable]
    public class ClsSqlGetDataValues : IDisposable, IProCOMPlus.IGetDataValues
    {
        private ClsSqlConnection clsConn = null;
        private SqlCommand sqlCommand = null;
        private bool disposed = false;
       
	public ClsSqlGetDataValues(string strConnection)
	{
		try
		{
			if (clsConn == null)
				clsConn = new ClsSqlConnection(strConnection);
		}
		catch (SqlException ex)
		{
			throw ex;
		}
	}
        ~ClsSqlGetDataValues()
        {
            Dispose(false);
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
                    if (null != clsConn)
                        clsConn.Dispose();
                   
                }
                // TODO: free unmanaged resources here.
                // Set disposed flag:
            }
            disposed = true;
        }

        #endregion

        #region IGetDataValues Members

        public DataSet GetDataSet(string strProcedure, string[] arrParaNames, object[] arrValues, SqlDbType[] _SqlDbType, CommandType CmdType)
        {

            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedure;
                for (int i = 0; i < arrParaNames.Length; i++)
                {
                    //if (_SqlDbType[i]!=SqlDbType.DateTime)
                        sqlCommand.Parameters.Add("@" + arrParaNames[i], _SqlDbType[i]).Value = (arrValues[i] != null ? arrValues[i] : System.DBNull.Value);
                   // else
                   //     sqlCommand.Parameters.Add("@" + arrParaNames[i], _SqlDbType[i]).Value = (((DateTime)arrValues[i]).Year != 1 ? arrValues[i] : System.DBNull.Value);
                    
                }
                clsConn.SqlOpenConnection();
                sqlCommand.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter())
                {
                    adap.SelectCommand = sqlCommand;
                    using (DataSet result = new DataSet())
                    {
                        adap.Fill(result); return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                clsConn.SqlCloseConnection();
                this.Dispose();
            }

        }

        #endregion

        #region IGetDataValues Members
        public DataSet GetDataSet(string strProcedure, CommandType CmdType)
        {
            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedure;               
                clsConn.SqlOpenConnection();
              //  sqlCommand.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter())
                {
                    adap.SelectCommand = sqlCommand;
                    using (DataSet result = new DataSet())
                    {
                        adap.Fill(result);
                        return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                clsConn.SqlCloseConnection();
                this.Dispose();
            }
        }

        public DataTable GetDataTable(string strProcedure, CommandType CmdType)
        {
            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedure;
                clsConn.SqlOpenConnection();
                sqlCommand.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter())
                {
                    adap.SelectCommand = sqlCommand;
                    using (DataTable result = new DataTable())
                    {
                        adap.Fill(result);
                        return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                clsConn.SqlCloseConnection();
                this.Dispose();
            }
        }

        #endregion
    }
}
