using System;
using System.Data;
using System.Data.SqlClient;


namespace Shop.DAL.ProAppCOMPlus
{
    /// <summary>
    /// Summary description for ClsSqlExecuteData
    /// </summary>
    [Serializable]
    public class ClsSqlExecuteData : IDisposable, DAL.IProCOMPlus.IExecuteData
    {
        private ClsSqlConnection clsConn = null;
        private SqlCommand sqlCommand = null;
        private bool disposed = false;
        
		public ClsSqlExecuteData(string strConnection)
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
        ~ClsSqlExecuteData()
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
        #region IExecuteData Members
        /// <summary>
        /// Insert or Upadate or Delete Database
        /// </summary>
        /// <param name="strProName"></param>
        /// <param name="arrPara"></param>
        /// <param name="arrValues"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public DataSet ExecProcedureGetds(string strProcedurewrite, string strProcedureread, string[] arrParaNameswrite, string[] arrValueswrite, string[] arrParaNamesread, string[] arrValuesread, CommandType CmdType, ref int result)
        {
            DataSet ds = new DataSet();
            // int result = -1;
            if (arrParaNameswrite.Length != arrValueswrite.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");
            if (arrParaNamesread.Length != arrValuesread.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedurewrite;
                for (int i = 0; i < arrParaNameswrite.Length; i++)
                {
                    sqlCommand.Parameters.Add("@" + arrParaNameswrite[i], arrValueswrite[i]);//With VS2003 use method Add()
                }

                clsConn.SqlOpenConnection();
                result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    //sqlCommand.Dispose();
                    sqlCommand.Parameters.Clear();
                    SqlDataAdapter da = new SqlDataAdapter();
                    sqlCommand.CommandText = strProcedureread;

                    for (int j = 0; j < arrParaNamesread.Length; j++)
                    {
                        sqlCommand.Parameters.Add("@" + arrParaNamesread[j], arrValuesread[j]);//With VS2003 use method Add()
                    }
                    da.SelectCommand = sqlCommand;
                    da.Fill(ds);
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
            return ds;
        }
        public bool ExecProcedure(string strProcedure, string[] arrParaNames, object[] arrValues, System.Data.SqlDbType[] arrType, SqlDbType SQLTypeOut,CommandType CmdType,ref long OuId)
        {
            
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");
                long nQuery = 0;
                try
                {
                
                    sqlCommand = clsConn.GetSqlCommand();
                    sqlCommand.CommandType = CmdType;
                    sqlCommand.CommandText = strProcedure;
                    for (int i = 0; i < arrParaNames.Length; i++)
                    {
                            sqlCommand.Parameters.Add("@" + arrParaNames[i], arrType[i]).Value = (arrValues[i] != null ? arrValues[i] : System.DBNull.Value);
                    }
                    if (OuId != -1)
                    {
                        SqlParameter p = sqlCommand.Parameters.Add("@Id", SQLTypeOut);
                        p.Direction = ParameterDirection.Output;
                        clsConn.SqlOpenConnection();
                        nQuery = sqlCommand.ExecuteNonQuery();                    
                        OuId = Convert.ToInt64(p.Value);
                    }
                    else
                    {
                        clsConn.SqlOpenConnection();
                        nQuery = sqlCommand.ExecuteNonQuery();
                    }
                
                
                }
                catch (SqlException ex)
                {
                    return false;
                }
                finally
                {
                    clsConn.SqlCloseConnection();
                    this.Dispose();
                }
                return nQuery <= 0 ? false : true;
            } 

        #endregion
    }
}
