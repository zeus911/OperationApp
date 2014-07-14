using System;
using System.Data;
using System.Data.SqlClient;

namespace Nbugs.Common
{
    public abstract class SqlHelper
    {
        private static SqlConnection sqlConn;

        private static SqlConnection Init(string str)
        {
            sqlConn = new SqlConnection(str);
            return sqlConn;
        }

        private static void Open()
        {
            if (sqlConn == null)
                throw new ArgumentNullException("sql字符串为空");
            if (sqlConn.State == ConnectionState.Closed)
            {
                try
                {
                    sqlConn.Open();
                }
                catch (Exception) { }
            }
        }

        private static void Close()
        {
            if (sqlConn != null)
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }

        private static SqlCommand CreateProcCommand(string procName, SqlParameter[] prams)
        {
            Open();
            SqlCommand cmd = new SqlCommand(procName, sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            ///添加返回参数ReturnValue
            //cmd.Parameters.Add(
            //    new SqlParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlCommand对象
            return cmd;
        }

        private static SqlDataAdapter CreateProcDataAdapter(string procName, SqlParameter[] prams)
        {
            Open();
            ///设置SqlDataAdapter对象
            SqlDataAdapter da = new SqlDataAdapter(procName, sqlConn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }

            ///添加返回参数ReturnValue
            //da.SelectCommand.Parameters.Add(
            //    new SqlParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlDataAdapter对象
            return da;
        }

        private static SqlDataAdapter CreateSQLDataAdapter(string cmdText, SqlParameter[] prams)
        {
            Open();
            ///设置SqlDataAdapter对象
            SqlDataAdapter da = new SqlDataAdapter(cmdText, sqlConn);

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }

            /////添加返回参数ReturnValue
            //da.SelectCommand.Parameters.Add(
            //    new SqlParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlDataAdapter对象
            return da;
        }

        private static SqlCommand CreateSQLCommand(string cmdText, SqlParameter[] prams)
        {
            Open();
            ///设置Command
            SqlCommand cmd = new SqlCommand(cmdText, sqlConn);

            ///添加把存储过程的参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            /////添加返回参数ReturnValue
            //cmd.Parameters.Add(
            //    new SqlParameter("RETURNVALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue,
            //    false, 0, 0, string.Empty, DataRowVersion.Default, null));

            ///返回创建的SqlCommand对象
            return cmd;
        }

        /// <summary>
        /// 执行存储过程无返回
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">参数</param>
        public static void RunProc(string procName, SqlParameter[] prams,string sqlConnStr)
        {
            try
            {
                using (Init(sqlConnStr))
                {
                    SqlCommand cmd = CreateProcCommand(procName, prams);
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
        public static void RunProc(string procName, SqlParameter[] prams, ref DataSet dataSet,string sqlConnStr)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(sqlConnStr))
                {
                    SqlDataAdapter da = CreateProcDataAdapter(procName, prams);
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
        public static void RunSQL(string cmdText, SqlParameter[] prams,string sqlConnStr)
        {
            try
            {
                using (Init(sqlConnStr))
                {
                    SqlCommand cmd = CreateSQLCommand(cmdText, prams);
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
        public static void RunSQL(string cmdText, SqlParameter[] prams, ref DataSet dataSet,string sqlConnStr)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(sqlConnStr))
                {
                    SqlDataAdapter da = CreateSQLDataAdapter(cmdText, prams);
                    da.Fill(dataSet);
                }
            }
            catch (Exception)
            { }
        }

        public static string RunSQLExecuteScalar(string cmdText, SqlParameter[] prams,string sqlConnStr)
        {
            try
            {
                using (Init(sqlConnStr))
                {
                    SqlCommand cmd = CreateSQLCommand(cmdText, prams);
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