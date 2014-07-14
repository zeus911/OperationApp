using System.ComponentModel.DataAnnotations;

namespace Nbugs.OperationApp.Models.System
{
    public class ModuleAction
    {
        public int Id { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "模块Id不能为空")]
        public int ModuleId { get; set; }

        [Display(Name = "操作码")]
        [Required(ErrorMessage = "操作码不能为空")]
        public string KeyCode { get; set; }

        public virtual Module Module { get; set; }
    }
}