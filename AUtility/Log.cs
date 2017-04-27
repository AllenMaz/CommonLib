using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AUtility
{
    /// <summary>
    /// 日志记录
    /// 默认记录到:C://CommonLib//logs
    /// </summary>
    public class Log
    {
        private static string _logPath = "C://CommonLib//logs";

        public void SetLogPath(string logPath)
        {
            _logPath = logPath;
        }

        /**
        * 向日志文件写入出错信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Write(string className, string content, LogLevel logLevel)
        {
            WriteLog(logLevel.ToString(), className, content);
        }

        /**
        * 实际的写日志操作
        * @param type 日志记录类型
        * @param className 类名
        * @param content 写入内容
        */
        protected static void WriteLog(string type, string className, string content)
        {
            if (!Directory.Exists(_logPath))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(_logPath);
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = _logPath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            //向日志文件写入内容
            string write_content = time + " " + type + " " + className + ": " + content;
            mySw.WriteLine(write_content);

            //关闭日志文件
            mySw.Close();
        }
    }

    /// <summary>
    /// //=======【日志级别】===================================
    /* 日志等级，0.调试信息；1.警告信息及调试信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
    */
    /// </summary>
    public enum LogLevel
    {
        Debug = 0,
        Warning = 1,
        Information = 2,
        Error = 3,
        All = 9
    }
}
