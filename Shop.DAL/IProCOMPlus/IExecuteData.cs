using System;
using System.Data;

namespace Shop.DAL.IProCOMPlus
{
    /// <summary>
    /// Summary description for IExecuteData
    /// </summary>
    public interface IExecuteData
    {
        bool ExecProcedure(string strProcedure, string[] arrParaNames, object[] arrValues, System.Data.SqlDbType[] arrType, SqlDbType SQLTypeOut, CommandType CmdType, ref long OuId);
    }
}
