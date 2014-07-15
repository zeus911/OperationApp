using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Nbugs.DAL.Oracle
{
    public class OracleAccessFactory
    {
        public static OracleAccess GetInstance
        {
            get
            {
                OracleAccess orclAccess = (OracleAccess)CallContext.GetData("OracleAccess");
                if(orclAccess==null)
                {
                    orclAccess = new OracleAccess();
                }
                return orclAccess;
            }
        }
    }
}
