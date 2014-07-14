using Nbugs.IBLL.System;
using Nbugs.IDAL.Db144;
using Nbugs.OperationApp.Models.System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nbugs.BLL.System
{
    public class ModuleService:BaseService<Module>,IModuleService
    {
        public ModuleService(IDb144Session _DbSession)
        {
            dbSession = _DbSession;
            currentRepository = _DbSession.ModuleRepository;
        }
        public IQueryable<Module> GetList(int id)
        {
            return currentRepository.LoadEntities(u => u.ParentId == id && u.Id > 0);
        }
        public List<Module> GetMenuByUserIdAndModuleId(int UserId, int? ModuleId)
        {
            var sql = "exec GetMenuById @UserId,@ModuleId";
            var prams = new SqlParameter[]{
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@ModuleId",ModuleId)
            };
            List<Module> result = new List<Module>();
            dbSession.ExcuteSQL(sql,ref result, prams);
            return result;
        }
        public void InsertRoleModule()
        {
            dbSession.ExcuteSQL("exec InsertRoleModule", null);
        }
    }
}
