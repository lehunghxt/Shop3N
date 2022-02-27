using System;
using System.Data;
using System.Collections;
namespace Shop.DAL.IProCOMPlus
{
    /// <summary>
    /// IGetObjValues use to get value of string, int, array list.
    /// </summary>
    public interface IGetObjValues
    {
        string GetValueString(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType);
        string[] GetValueArrayString(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType);
       
        int GetValueInt(string strProcedure, string[] arrParaNames, string[] arrValues,  CommandType CmdType);
        double GetValueDouble(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType);
        object GetValueObject(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType);
        object[] GetValueArrayObject(string strProcedure, string[] arrParaNames, string[] arrValues, CommandType CmdType);
        string GetAutoNo(string strProcedure, string[] arrParaNames, string[] arrValues, string autoNoColumn, string prefix, string numberOf, string oderByColumn, CommandType CmdType);
        void GetValue(string strProcedure, string[] arrParaNames, string[] arrValues, ref ArrayList arrList, CommandType CmdType);
        void GetValue(string strProcedure, string[] arrParaNames, string[] arrValues, ref ArrayList[] arrList, CommandType CmdType);

    }
}
