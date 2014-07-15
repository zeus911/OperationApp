using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Nbugs.IDAL
{
    public interface IDataAccess
    {
        //连接字符串
        string ConnStr { get; set; }
        /// <summary>
        /// 执行无返回的sql语句
        /// </summary>
        /// <param name="str">sql语句</param>
        /// <param name="prams">sql参数</param>
        void ExcuteSQL(string str, DbParameter[] prams);
        /// <summary>
        /// 执行sql语句返回对象集合
        /// </summary>
        /// <typeparam name="T">泛型，返回对象类型</typeparam>
        /// <param name="str">sql语句</param>
        /// <param name="prams">sql参数</param>
        /// <param name="result">返回对象集合变量</param>
        void ExcuteSQL<T>(string str, DbParameter[] prams, ref List<T> result) where T : class,new();
        /// <summary>
        /// 执行sql语句返回第一列第一行
        /// </summary>
        /// <param name="str">sql语句</param>
        /// <param name="prams">sql参数</param>
        /// <returns></returns>
        string ExcuteSQLScalar(string str, DbParameter[] prams);
        /// <summary>
        /// 执行存储过程无返回
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        void ExcuteProc(string procName, DbParameter[] prams);
        /// <summary>
        /// 执行存储过程返回对象集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">存储过程参数</param>
        /// <param name="result">返回的对象集合变量</param>
        void ExcuteProc<T>(string procName, DbParameter[] prams, ref List<T> result) where T : class,new();
    }
}
