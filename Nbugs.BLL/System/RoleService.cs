using Nbugs.IBLL.System;
using Nbugs.IDAL.Db144;
using Nbugs.OperationApp.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nbugs.BLL.System
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        public RoleService(IDb144Session _DbSession)
        {
            dbSession = _DbSession;
            currentRepository = _DbSession.RoleRepository;
        }
        public void InsertRoleModule()
        {
            dbSession.ExcuteSQL("exec dbo.InsertRoleModule", null);
        }
    }
}
