using System;
using System.Data;

namespace Shop.DAL.IProCOMPlus
{
    /// <summary>
    /// Summary description for IGetDataValues
    /// </summary>
    public interface IGetDataValues
    {
        DataSet GetDataSet(string strProcedure, string[] arrParaNames, object[] arrValues, SqlDbType[] _SqlDbType, CommandType CmdType);
    }
}
