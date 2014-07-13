using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nbugs.IDAL;
using Nbugs.IDAL.Db144;
using Nbugs.DAL.Db144;
using Ninject;

namespace Nbugs.DAL
{
    public static class NinjectCommon
    {
        public static void Init()
        {
            CreateKernel();
        }
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Settings.InjectNonPublic = true;
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<Db144ContextFactoy>().Named("Db144Context");
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IModuleRepository>().To<ModuleRepository>();
            kernel.Bind<IModuleActionsRepository>().To<ModuleActionsRepository>();
            kernel.Bind<IRoleModuleRepository>().To<RoleModuleRepository>();
            kernel.Bind<IRoleModuleActionRespository>().To<RoleModuleActionRepository>();

            kernel.Bind<IDbContext>().To<Db144ContextFactoy>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<UserRepository>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<RoleRepository>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<ModuleRepository>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<ModuleActionsRepository>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<RoleModuleActionRepository>();
            //kernel.Bind<IDbContext>().To<Db144ContextFactoy>().WhenInjectedInto<RoleModuleRepository>();
        }
    }
}
