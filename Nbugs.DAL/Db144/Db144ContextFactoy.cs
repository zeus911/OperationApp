using Nbugs.OperationApp.Models;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using Nbugs.IDAL;

namespace Nbugs.DAL.Db144
{
    public class Db144ContextFactoy:IDbContext
    {
        public DbContext GetInstance
        {
            get
            {
                Db144Context dbContext = (Db144Context)CallContext.GetData("Db144Context");
                if (dbContext == null)
                {
                    dbContext = new Db144Context();
                    CallContext.SetData("Db144Context", dbContext);
                }
                return dbContext;
            }
            set
            {
                GetInstance = value;
            }
        }
    }
}