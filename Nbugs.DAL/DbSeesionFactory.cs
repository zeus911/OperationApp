using Nbugs.DAL.Db144;
using System.Runtime.Remoting.Messaging;

namespace Nbugs.DAL
{
    public class DbSeesionFactory
    {
        public static Db144Seesion GetDb144Seesion()
        {
            Db144Seesion _Db144Seesion = (Db144Seesion)CallContext.GetData("Db144Seesion");
            if (_Db144Seesion == null)
            {
                _Db144Seesion = new Db144Seesion();
                CallContext.SetData("Db144Seesion", _Db144Seesion);
            }
            return _Db144Seesion;
        }
    }
}