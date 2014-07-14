using Nbugs.IBLL.System;
using Nbugs.IDAL;
using Nbugs.IDAL.Db144;
using Nbugs.OperationApp.Models.System;
using Ninject;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Nbugs.BLL.System
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IDb144Session _DbSession)
        {
            dbSession =_DbSession;
            currentRepository = _DbSession.UserRepository;
        }
        //private IDb144Session dbSession { get; set; }
        //private IBaseRepository<User> currentRepository
        //{
        //    get
        //    {
        //        return dbSession.UserRepository;
        //    }
        //}      
        public enum ErrorCode : int
        {
            UserNotExist = 1,
            PassWordError = 2,
            Ok = 3
        }

        public int CheckUser(User user)
        {
            var loginUser = currentRepository.LoadEntities(u => u.LoginName == user.LoginName).FirstOrDefault();
            if (loginUser == null)
                return (int)ErrorCode.UserNotExist;
            if (loginUser.Pwd != user.Pwd)
                return (int)ErrorCode.PassWordError;
            return (int)ErrorCode.Ok;
        }

        public List<Permission> GetPermissions(int userId)
        {
            SqlParameter[] paras = new SqlParameter[]{
            new SqlParameter("@userId",userId)};
            List<Permission> result = new List<Permission>();
            dbSession.ExcuteSQL("exec dbo.GetPermissions @userId", ref result, paras);
            return result;
        }

        public void InsertUserRole(int UserId, int RoleId)
        {
            SqlParameter[] paras = new SqlParameter[]{
            new SqlParameter("@UserId",UserId),
            new SqlParameter("@RoleId",RoleId)};
            dbSession.ExcuteSQL("exec InsertUserRole @UserId,@RoleId", paras);
        }

        public void DeleteUserRole(int UserId, int RoleId)
        {
            SqlParameter[] paras = new SqlParameter[]{
            new SqlParameter("@UserId",UserId),
            new SqlParameter("@RoleId",RoleId)};
            dbSession.ExcuteSQL("exec DeleteUserRole @UserId,@RoleId", paras);
        }

        public List<int> GetRolesByUserId(int UserId)
        {
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@UserId",UserId)
            };
            List<int> result = new List<int>();
            dbSession.ExcuteSQL("exec GetRolesByUserId @UserId", ref result, para);
            return result;
        }
    }
}