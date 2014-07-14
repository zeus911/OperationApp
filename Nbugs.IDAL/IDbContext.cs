using System.Data.Entity;

namespace Nbugs.IDAL
{
    public interface IDbContext
    {
        DbContext GetInstance { get; }
    }
}