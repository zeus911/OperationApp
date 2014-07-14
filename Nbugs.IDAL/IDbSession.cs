using Nbugs.IDAL.Db144;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Nbugs.IDAL
{
    public interface IDbSession
    {
        IDbContext DbContext { get; set; }
        int SaveChanges();

        int ExcuteSQL(string sql, params SqlParameter[] prams);

        void ExcuteSQL<T>(string sql, ref List<T> lists, params SqlParameter[] prams);
    }
}