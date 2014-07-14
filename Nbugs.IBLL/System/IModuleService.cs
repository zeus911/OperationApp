using System.Collections.Generic;
using System.Linq;
using Nbugs.OperationApp.Models.System;

namespace Nbugs.IBLL.System
{
    public interface IModuleService : IBaseService<Module>
    {
        IQueryable<Module> GetList(int id);

        void InsertRoleModule();

        List<Module> GetMenuByUserIdAndModuleId(int UserId, int? ModuleId);
    }
}