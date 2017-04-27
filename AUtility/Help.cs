using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AUtility
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class Help
    {

        public void GetClassName()
        {

            StackTrace trace = new StackTrace();

            MethodBase methodName = trace.GetFrame(1).GetMethod();
        }
    }
}
