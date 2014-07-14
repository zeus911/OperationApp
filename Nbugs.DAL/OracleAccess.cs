using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nbugs.IDAL;
using Nbugs.Common;
using System.Data;
using System.Configuration;

namespace Nbugs.DAL
{
    public class OracleAccess:IDataAccess
    {
        public OracleAccess()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["orcl"].ConnectionString;
        }
        public string ConnStr { get; set; }


        public void ExcuteProc<T>(string procName, System.Data.Common.DbParameter[] prams, ref List<T> result) where T : class, new()
        {
            DataSet ds = new DataSet();
           OracleHelper.RunProc(procName,ref ds,ConnStr,prams);
           if (ds.Tables.Count > 0)
           {
               result = ConvertHelper<T>.ConvertToList(ds.Tables[0]);
           }
        }

        public void ExcuteProc(string procName, System.Data.Common.DbParameter[] prams)
        {
            OracleHelper.RunProc(procName,ConnStr,prams);
        }

        public void ExcuteSQL<T>(string str, System.Data.Common.DbParameter[] prams, ref List<T> result) where T : class, new()
        {
            DataSet ds = new DataSet();
            OracleHelper.RunSQL(str, ref ds, ConnStr, prams);
            if(ds.Tables.Count>0)
            {
                result = ConvertHelper<T>.ConvertToList(ds.Tables[0]);
            }
        }

        public void ExcuteSQL(string str, System.Data.Common.DbParameter[] prams)
        {
            OracleHelper.RunSQL(str,ConnStr,prams);
        }

        public string ExcuteSQLScalar(string str, System.Data.Common.DbParameter[] prams)
        {
            return OracleHelper.RunSQLExecuteScalar(str,ConnStr,prams);
        }
    }
}
