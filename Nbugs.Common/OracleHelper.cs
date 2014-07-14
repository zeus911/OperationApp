using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace Nbugs.Common
{
    public sealed class OracleHelper
    {
        private static OracleConnection orclConn;

        private static OracleConnection Init(string str)
        {
            orclConn = new OracleConnection(str);
            return orclConn;
        }

        private static void Open()
        {
            if (orclConn == null)
                throw new ArgumentNullException("sql字符串为空");
            if (orclConn.State == ConnectionState.Closed)
            {
                try
                {
                    orclConn.Open();
                }
                catch (Exception) { }
            }
        }

        private static void Close()
        {
            if (orclConn != null)
            {
                if (orclConn.State == ConnectionState.Open)
                {
                    orclConn.Close();
                }
            }
        }

        private static OracleCommand CreateProcCommand(string procName, params DbParameter[] prams)
        {
            Open();
            OracleCommand cmd = new OracleCommand(procName, orclConn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (prams != null)
            {
                foreach (OracleParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            ///添加返回参数ReturnValue
            //cmd.Parameters.Add(
            //    new OracleParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的OracleCommand对象
            return cmd;
        }

        private static OracleDataAdapter CreateProcDataAdapter(string procName, params DbParameter[] prams)
        {
            Open();
            ///设置OracleDataAdapter对象
            OracleDataAdapter da = new OracleDataAdapter(procName, orclConn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (OracleParameter parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }

            ///添加返回参数ReturnValue
            //da.SelectCommand.Parameters.Add(
            //    new OracleParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的OracleDataAdapter对象
            return da;
        }

        private static OracleDataAdapter CreateOracleDataAdapter(string cmdText, params DbParameter[] prams)
        {
            Open();
            ///设置OracleDataAdapter对象
            OracleDataAdapter da = new OracleDataAdapter(cmdText, orclConn);

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (OracleParameter parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }

            /////添加返回参数ReturnValue
            //da.SelectCommand.Parameters.Add(
            //    new OracleParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的OracleDataAdapter对象
            return da;
        }

        private static OracleCommand CreateOracleCommand(string cmdText, params DbParameter[] prams)
        {
            Open();
            ///设置Command
            OracleCommand cmd = new OracleCommand(cmdText, orclConn);

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (OracleParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            /////添加返回参数ReturnValue
            //cmd.Parameters.Add(
            //    new OracleParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的OracleCommand对象
            return cmd;
        }

        /// <summary>
        /// 执行存储过程无返回
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数</param>
        public static void RunProc(string procName,string orclConnStr, params DbParameter[] prams)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    OracleCommand cmd = CreateProcCommand(procName, prams);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数</param>
        /// <param name="dataSet">DataSet引用</param>
        public static void RunProc(string procName, ref DataSet dataSet, string orclConnStr, params DbParameter[] prams)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(orclConnStr))
                {
                    OracleDataAdapter da = CreateProcDataAdapter(procName, prams);
                    da.Fill(dataSet);
                }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 执行sql语句无返回
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="prams">参数</param>
        public static void RunSQL(string cmdText, string orclConnStr, params DbParameter[] prams)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    OracleCommand cmd = CreateOracleCommand(cmdText, prams);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 执行sql语句返回DataSet
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="prams"></param>
        /// <param name="dataSet"></param>
        public static void RunSQL(string cmdText, ref DataSet dataSet, string orclConnStr, params DbParameter[] prams)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(orclConnStr))
                {
                    OracleDataAdapter da = CreateOracleDataAdapter(cmdText, prams);
                    da.Fill(dataSet);
                }
            }
            catch (Exception)
            { }
        }

        public static string RunSQLExecuteScalar(string cmdText, string orclConnStr, params DbParameter[] prams)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    OracleCommand cmd = CreateOracleCommand(cmdText, prams);
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("sql执行出错！");
            }
        }
    }
}