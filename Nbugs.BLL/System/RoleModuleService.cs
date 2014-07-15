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
    public class RoleModuleService : BaseService<RoleModule>, IRoleModuleService
    {
        public RoleModuleService(IDb144Session _DbSession)
        {
            dbSession = _DbSession;
            currentRepository = _DbSession.RoleModuleRepository;
        }
        public void UpdateRoleModule(int ModuleId, int RoleId)
        {
            var prams = new SqlParameter[]{
                new SqlParameter("@ModuleId",ModuleId),
                new SqlParameter("@RoleId",RoleId)
            };
            dbSession.ExcuteSQL("exec UpdateRoleModule @ModuleId,@RoleID", prams);
        }
    }
}