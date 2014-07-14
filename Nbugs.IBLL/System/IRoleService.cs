using Nbugs.OperationApp.Models.System;
using Nbugs.IBLL;

namespace Nbugs.IBLL.System
{
    public interface IRoleService : IBaseService<Role>
    {
        void InsertRoleModule();
    }
}