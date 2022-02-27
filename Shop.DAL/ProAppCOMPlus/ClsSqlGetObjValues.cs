using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Shop.DAL.ProAppCOMPlus
{
    /// <summary>
    /// ClsSqlGetObjValues use to get value of string, int, array list.
    /// </summary>
    [Serializable]
    public class ClsSqlGetObjValues : IDisposable,IProCOMPlus.IGetObjValues
    {
        private ClsSqlConnection clsConn = null;
        private SqlCommand sqlCommand = null;
        private bool disposed = false;

		public ClsSqlGetObjValues(string strConnection)
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
        ~ClsSqlGetObjValues()
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
                    //if (null != sqlCommand)
                    //    sqlCommand.Dispose();
                }
                // TODO: free unmanaged resources here.
                // Set disposed flag:
            }
            disposed = true;
        }

        #endregion

        #region IGetObjValues Members

        public string GetValueString(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
            string result = String.Empty;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");
                       
            try
            {
                result = GetValueObject(strProcedure, arrParaNames, arrValues, CmdType).ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
           
            return result;       
            
        }

        public string[] GetValueArrayString(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
           
             string[] result;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");
                       
            try
            {
                result = (string[])GetValueArrayObject(strProcedure, arrParaNames, arrValues, CmdType);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
           
            return result;       
        }

        public int GetValueInt(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
            int result;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                result = Convert.ToInt32(GetValueObject(strProcedure, arrParaNames, arrValues, CmdType));
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return result;       
        }

        public double GetValueDouble(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
            double result;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                result = Convert.ToDouble(GetValueObject(strProcedure, arrParaNames, arrValues, CmdType));
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return result;       
        }

        public object GetValueObject(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
            object result = null;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedure;
                for (int i = 0; i < arrParaNames.Length; i++)
                {
                    sqlCommand.Parameters.Add("@" + arrParaNames[i], arrValues[i]);//With VS2003 use method Add()
                }
                clsConn.SqlOpenConnection();
                result = sqlCommand.ExecuteScalar();
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
            return result;
        }

        public object[] GetValueArrayObject(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType)
        {
            SqlDataReader objReader = null;
            if (arrParaNames.Length != arrValues.Length)
                throw new ArgumentException("The Array Parameter Names and Array Parameter Values is not equal", "arrParaNames or arrValues");

            try
            {
                sqlCommand = clsConn.GetSqlCommand();
                sqlCommand.CommandType = CmdType;
                sqlCommand.CommandText = strProcedure;
                for (int i = 0; i < arrParaNames.Length; i++)
                {
                    sqlCommand.Parameters.Add("@" + arrParaNames[i], arrValues[i]);//With VS2003 use method Add()
                }
                clsConn.SqlOpenConnection();
                objReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
                object[] strValue = new object[objReader.FieldCount];
                if (objReader.Read())
                {
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        strValue[i] = objReader.GetValue(i);
                    }
                } 
                return strValue;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objReader.Close();
                clsConn.SqlCloseConnection();
                this.Dispose();
            }
           
        }
        /// <summary>
        /// Generate automaticlly string
        /// Ex: GetAutoNo("tbOrders","OrderNo","ODR/","4","OrderID")
        /// Return value probaly is ORD/0124
        /// </summary>
        /// <param name="strProcedure">Store Procedure Name</param>
        /// <param name="arrParaNames">Array of Parameter</param>
        /// <param name="arrValues">Array of Value for Parameter</param>
        /// <param name="autoNoColumn">Column name you want to generate</param>
        /// <param name="prefix">Is Prefix string</param>
        /// <param name="numberOf"></param>
        /// <param name="oderByColumn">Number of charater to take out</param>
        /// <param name="CmdType">Type of Command Object</param>
        /// <returns>AutoNo</returns>
        public string GetAutoNo(string strProcedure, string[] arrParaNames, string[] arrValues, string autoNoColumn, string prefix, string numberOf, string oderByColumn, CommandType CmdType)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void GetValue(string strProcedure, string[] arrParaNames, string[] arrValues, ref ArrayList arrList, CommandType CmdType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void GetValue(string strProcedure, string[] arrParaNames, string[] arrValues, ref ArrayList[] arrList, CommandType CmdType)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
      
    }
}
