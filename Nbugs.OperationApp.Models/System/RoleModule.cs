using System.Collections.Generic;

namespace Nbugs.OperationApp.Models.System
{
    public class RoleModule
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int ModuleId { get; set; }

        public bool IsValid { get; set; }

        public virtual List<RoleModuleAction> RoleModuleActions { get; set; }

        public Module Module { get; set; }
    }
}