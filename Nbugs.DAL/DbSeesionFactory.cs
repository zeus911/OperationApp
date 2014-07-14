using Nbugs.DAL.Db144;
using System.Runtime.Remoting.Messaging;

namespace Nbugs.DAL
{
    public class DbSeesionFactory
    {
        public static Db144Session GetDb144Seesion()
        {
            Db144Session _Db144Seesion = (Db144Session)CallContext.GetData("Db144Seesion");
            if (_Db144Seesion == null)
            {
                _Db144Seesion = new Db144Session();
                CallContext.SetData("Db144Seesion", _Db144Seesion);
            }
            return _Db144Seesion;
        }
    }
}