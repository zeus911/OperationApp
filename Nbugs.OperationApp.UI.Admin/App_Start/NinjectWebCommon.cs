[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Nbugs.OperationApp.UI.Admin.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Nbugs.OperationApp.UI.Admin.App_Start.NinjectWebCommon), "Stop")]

namespace Nbugs.OperationApp.UI.Admin.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Nbugs.BLL.System;
    using Nbugs.DAL.Db144;
    using Nbugs.IBLL.System;
    using Nbugs.IDAL;
    using Nbugs.IDAL.Db144;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IModuleService>().To<ModuleService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleModuleActionService>().To<RoleModuleActionService>();
            kernel.Bind<IModuleActionsService>().To<ModuleActionsService>();
            kernel.Bind<IRoleModuleService>().To<RoleModuleService>();

            kernel.Bind<IDb144Session>().To<Db144Session>().Named("Db144Session");
            kernel.Bind<IDbContext>().To<Db144ContextFactory>().Named("Db144Context");
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IModuleRepository>().To<ModuleRepository>();
            kernel.Bind<IModuleActionsRepository>().To<ModuleActionsRepository>();
            kernel.Bind<IRoleModuleRepository>().To<RoleModuleRepository>();
            kernel.Bind<IRoleModuleActionRepository>().To<RoleModuleActionRepository>();
        }
    }
}