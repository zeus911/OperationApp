using Nbugs.OperationApp.Models.System;

namespace Nbugs.IBLL.System
{
    public interface IRoleModuleService : IBaseService<RoleModule>
    {
        void UpdateRoleModule(int ModuleId, int RoleId);
    }
}