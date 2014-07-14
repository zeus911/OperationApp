using Nbugs.IBLL.System;
using Nbugs.IDAL.Db144;
using Nbugs.OperationApp.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nbugs.BLL.System
{
    public class RoleModuleActionService:BaseService<RoleModuleAction>,IRoleModuleActionService
    {
        public RoleModuleActionService(IDb144Session _DbSession)
        {
            dbSession = _DbSession;
            currentRepository = _DbSession.RoleModuleActionRepository;
        }
    }
}
