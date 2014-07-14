using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace Nbugs.Common
{
    public class OrclHelper
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

        private static OracleParameter[] CreateParamters(List<Param> prams)
        {
            if (prams.Count == 0)
                return null;
            else
            {
                OracleParameter[] orclParams=new OracleParameter[prams.Count-1];
                for(int i=0;i<prams.Count;i++)
                {
                    orclParams[i] = new OracleParameter(prams[i].Name, prams[i].Value);
                }
                return orclParams;
            }

        }
        private static OracleCommand CreateProcCommand(string procName, OracleParameter[] prams)
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

        private static OracleDataAdapter CreateProcDataAdapter(string procName, OracleParameter[] prams)
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

        private static OracleDataAdapter CreateOracleDataAdapter(string cmdText, OracleParameter[] prams)
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

        private static OracleCommand CreateOracleCommand(string cmdText, OracleParameter[] prams)
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
        public static void RunProc(string procName, List<Param> lists, string orclConnStr)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    var prams = CreateParamters(lists);
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
        public static void RunProc(string procName, List<Param> lists, ref DataSet dataSet, string orclConnStr)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(orclConnStr))
                {
                    var prams = CreateParamters(lists);
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
        public static void RunSQL(string cmdText, List<Param> lists, string orclConnStr)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    var prams = CreateParamters(lists);
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
        public static void RunSQL(string cmdText, List<Param> lists, ref DataSet dataSet, string orclConnStr)
        {
            dataSet = dataSet == null ? new DataSet() : dataSet;
            try
            {
                using (Init(orclConnStr))
                {
                    var prams = CreateParamters(lists);
                    OracleDataAdapter da = CreateOracleDataAdapter(cmdText, prams);
                    da.Fill(dataSet);
                }
            }
            catch (Exception)
            { }
        }

        public static string RunSQLExecuteScalar(string cmdText, List<Param> lists, string orclConnStr)
        {
            try
            {
                using (Init(orclConnStr))
                {
                    var prams = CreateParamters(lists);
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
