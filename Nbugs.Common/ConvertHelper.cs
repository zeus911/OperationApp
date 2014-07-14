using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Nbugs.Common
{
    public class ConvertHelper<T> where T:class,new()
    {
        /// 利用反射和泛型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataTable dt)
        {
            // 定义集合
            List<T> ts = new List<T>();

            try
            {
                // 获得此模型的类型
                Type type = typeof(T);

                // 定义一个临时变量
                string tempName = string.Empty;

                // 遍历DataTable中所有的数据行
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();

                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();

                    // 遍历该对象的所有属性
                    foreach (PropertyInfo pi in propertys)
                    {
                        // 将属性名称赋值给临时变量
                        tempName = pi.Name;

                        // 检查DataTable是否包含此列（列名==对象的属性名）
                        if (dt.Columns.Contains(tempName))
                        {
                            // 判断此属性是否有Setter 该属性不可写，直接跳出
                            if (!pi.CanWrite) continue;

                            // 取值
                            object value = dr[tempName];
                            if (value is Int32)
                            {
                                pi.SetValue(t, Convert.ToInt32(value), null);
                                continue;
                            }
                            // 如果非空，则赋给对象的属性
                            if (value != DBNull.Value)
                                pi.SetValue(t, value, null);
                        }
                    }

                    // 对象添加到泛型集合中
                    ts.Add(t);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("ConvertHelper:" + ex.Message);
            }
            return ts;
        }
    }
}