using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nbugs.IDAL
{
    public interface IBaseRepository<T> where T : class,new()
    {
        IDbContext db { get; set; }
        T AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        /// <summary>

        /// 实现对数据的分页查询

        /// </summary>

        /// <typeparam name="S">按照某个类进行排序</typeparam>

        /// <param name="pageIndex">当前第几页</param>

        /// <param name="pageSize">一页显示多少条数据</param>

        /// <param name="total">总条数</param>

        /// <param name="whereLambda">取得排序的条件</param>

        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>

        /// <param name="orderByLambda">根据那个字段进行排序</param>

        /// <returns></returns>
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda,
            bool isAsc, Expression<Func<T, S>> orderByLambda);
    }
}