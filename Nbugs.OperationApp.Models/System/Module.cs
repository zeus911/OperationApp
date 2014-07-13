using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nbugs.OperationApp.Models.System
{
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "菜单名")]
        [Required(ErrorMessage = "菜单名称不能为空")]
        public string Name { get; set; }

        [Display(Name = "上级菜单")]
        public int ParentId { get; set; }

        [Display(Name = "链接")]
        public string Url { get; set; }

        [Display(Name = "图片地址")]
        public string Icon { get; set; }

        [Display(Name = "排序")]
        public int? Sort { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }

        [Display(Name = "是否生效")]
        public bool Enable { get; set; }

        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [Display(Name = "是否最后一项")]
        public bool IsLast { get; set; }

        public virtual List<ModuleAction> ModuleActions { get; set; }

        public virtual List<RoleModule> RoleModules { get; set; }
    }
}