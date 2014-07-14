using Nbugs.DAL.Db144;
using Nbugs.IDAL.Db144;
using Ninject;

namespace Nbugs.BLL
{
    public class NinjectCommon
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
            kernel.Bind<IDb144Session>().To<Db144Session>().Named("Db144Session");
        }
    }
}