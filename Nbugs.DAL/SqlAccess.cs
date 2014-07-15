using Nbugs.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Nbugs.Common;

namespace Nbugs.DAL
{
    public abstract class SqlAccess:IDataAccess
    {
        public string ConnStr
        {
            get;
            set;
        }
        public SqlAccess()
        {
            SetConnStr();
        }

        protected abstract void SetConnStr();

        public void ExcuteProc<T>(string procName, System.Data.Common.DbParameter[] prams, ref List<T> result) where T : class, new()
        {
            DataSet ds = new DataSet();
            SqlHelper.RunProc(procName, ref ds, ConnStr, prams);
            if(ds.Tables.Count>0)
            {
                result = ConvertHelper<T>.ConvertToList(ds.Tables[0]);
            }
        }

        public void ExcuteProc(string procName, System.Data.Common.DbParameter[] prams)
        {
            SqlHelper.RunProc(procName,ConnStr,prams);
        }

        public void ExcuteSQL<T>(string str, System.Data.Common.DbParameter[] prams, ref List<T> result) where T : class, new()
        {
            DataSet  ds = new DataSet();
            SqlHelper.RunSQL(str, ref ds, ConnStr, prams);
            if(ds.Tables.Count>0)
            {
                result = ConvertHelper<T>.ConvertToList(ds.Tables[0]);
            }
        }

        public void ExcuteSQL(string str, System.Data.Common.DbParameter[] prams)
        {
            SqlHelper.RunSQL(str,ConnStr,prams);
        }

        public string ExcuteSQLScalar(string str, System.Data.Common.DbParameter[] prams)
        {
            return SqlHelper.RunSQLExecuteScalar(str,ConnStr,prams);
        }
    }
}
