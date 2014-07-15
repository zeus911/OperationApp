using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Nbugs.DAL.Db203
{
    public class Db203AccessFactory
    {
        public Db203Access GetInstance
        {
            get
            {
                Db203Access access = (Db203Access)CallContext.GetData("Db203Access");
                if(access==null)
                {
                    access = new Db203Access();
                }
                return access;
            }
        }
    }
}
