using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nbugs.OperationApp.Models.System
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "角色名")]
        [Required(ErrorMessage = "角色名称不能为空")]
        public string Name { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }

        public virtual List<RoleModule> RoleModules { get; set; }
    }
}