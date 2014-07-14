using Nbugs.IDAL;
using Nbugs.IDAL.Db144;
using Ninject;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Nbugs.DAL.Db144
{
    public class Db144Session : IDb144Session
    {
        [Named("Db144Context")]
        public IDbContext DbContext { get; set; }

        [Inject]
        public IRoleRepository RoleRepository { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

        [Inject]
        public IModuleActionsRepository ModuleActionsRepository { get; set; }

        [Inject]
        public IModuleRepository ModuleRepository { get; set; }

        [Inject]
        public IRoleModuleRepository RoleModuleRepository { get; set; }

        [Inject]
        public IRoleModuleActionRespository RoleModuleRepsitory { get; set; }

        public int SaveChanges()
        {
            return DbContext.GetInstance.SaveChanges();
        }

        public void ExcuteSQL<T>(string str, ref List<T> lists, params SqlParameter[] prams)
        {
            lists = DbContext.GetInstance.Database.SqlQuery<T>(str, prams).ToList();
        }

        public int ExcuteSQL(string str, params SqlParameter[] prams)
        {
            return DbContext.GetInstance.Database.ExecuteSqlCommand(str, prams);
        }
    }
}