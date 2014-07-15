using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nbugs.DAL.Db144
{
    public class Db144Access:SqlAccess
    {
        protected override void SetConnStr()
        {
            ConnStr=ConfigurationManager.ConnectionStrings["Db144"].ConnectionString;
        }
    }
}
