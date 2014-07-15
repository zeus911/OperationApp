using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nbugs.OperationApp.Models.System
{
    public class Account
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}