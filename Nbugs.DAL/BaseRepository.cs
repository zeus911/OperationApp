using Nbugs.IDAL;
using Ninject;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Nbugs.DAL
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class,new()
    {
        [Inject]
        public IDbContext db { get; set; }

        // 实现对数据库的添加功能,添加实现EF框架的引用
        public T AddEntity(T entity)
        {
            db.GetInstance.Set<T>().Add(entity);
            //db.GetInstance.SaveChanges();
            return entity;
        }

        //实现对数据库的修改功能
        public void UpdateEntity(T entity)
        {
            db.GetInstance.Set<T>().Attach(entity);
            db.GetInstance.Entry<T>(entity).State = EntityState.Modified;

            //return db.GetInstance.SaveChanges() > 0;
        }

        //实现对数据库的删除功能
        public void DeleteEntity(T entity)
        {
            db.GetInstance.Set<T>().Attach(entity);
            db.GetInstance.Entry<T>(entity).State = EntityState.Deleted;

            //return db.GetInstance.SaveChanges() > 0;
        }

        //实现对数据库的查询  --简单查询
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return db.GetInstance.Set<T>().Where<T>(whereLambda).AsQueryable();
        }

        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个字段进行排序,表达式中自动获取</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">取得排序的条件</param>
        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>
        /// <param name="orderByLambda">根据那个字段进行排序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out  int total, Expression<Func<T, bool>> whereLambda,
            bool isAsc, Expression<Func<T, S>> orderByLambda)
        {
            var temp = db.GetInstance.Set<T>().Where<T>(whereLambda);
            total = temp.Count(); //得到总的条数
            //排序,获取当前页的数据
            if (isAsc)
            {
                temp = temp.OrderBy<T, S>(orderByLambda)
                     .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                     .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                    .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            return temp.AsQueryable();
        }
    }
}