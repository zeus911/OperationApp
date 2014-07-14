using System.Collections.Generic;
using Nbugs.OperationApp.Models.System;
using System.Linq;

namespace Nbugs.IBLL.System
{
    public interface IUserService : IBaseService<User>
    {
        int CheckUser(User user);

        List<Permission> GetPermissions(int userId);

        void InsertUserRole(int UserId, int RoleId);

        void DeleteUserRole(int UserId, int RoleId);

        List<int> GetRolesByUserId(int UserId);
        IQueryable<User> GetUsers(int pageIndex, int pageSize, out int total, bool isAsc);
    }
}