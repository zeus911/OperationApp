namespace Nbugs.IDAL.Db144
{
    public interface IDb144Session : IDbSession
    {
        IUserRepository UserRepository { get; set; }

        IRoleRepository RoleRepository { get; set; }

        IModuleRepository ModuleRepository { get; set; }

        IRoleModuleRepository RoleModuleRepository { get; set; }

        IRoleModuleActionRespository RoleModuleRepsitory { get; set; }

        IModuleActionsRepository ModuleActionsRepository { get; set; }
    }
}