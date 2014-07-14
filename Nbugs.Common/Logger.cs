using System;
using System.IO;
using System.Text;

namespace Nbugs.Common
{
    public class Logger
    {
        private static string _fileName = "D:\\Log\\log.txt";

        public static string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fileName = value;
                }
            }
        }

        public static void Write(string str)
        {
            if (File.Exists(_fileName))
            {
                StreamWriter sw = new StreamWriter(_fileName, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString() + ":" + str);
                sw.Close();
            }
            else
            {
                StreamWriter sw = File.CreateText(_fileName);
                sw.WriteLine(DateTime.Now.ToString() + ":" + str);
                sw.Close();
            }
        }
    }
}