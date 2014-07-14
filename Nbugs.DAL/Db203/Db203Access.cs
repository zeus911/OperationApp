using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nbugs.DAL.Db203
{
    public class Db203Access:SqlAccess
    {
        protected override void SetConnStr()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["Db203"].ConnectionString;
        }
    }
}
