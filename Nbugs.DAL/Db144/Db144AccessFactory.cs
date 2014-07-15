using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Nbugs.DAL.Db144
{
    public class Db144AccessFactory
    {
        public static Db144Access GetInstance
        {
            get
            {
                Db144Access access = (Db144Access)CallContext.GetData("Db144Access");
                if(access==null)
                {
                    access = new Db144Access();
                }
                return access;
            }
        }
    }
}
