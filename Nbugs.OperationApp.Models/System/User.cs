using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nbugs.OperationApp.Models.System
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "登录名不能为空")]
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "请输入正确的Eamil地址")]
        public string Email { get; set; }

        [Display(Name = "手机号码")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "请输入正确的手机号码！")]
        public string Mobile { get; set; }

        public string QQ { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }

        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
    }
}