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

        public static System.Collections.Specialized.NameValueCollection ParseUrl(string url, out string baseUrl)
        {
            baseUrl = "";
            if (string.IsNullOrEmpty(url))
                return null;
            System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();

            try
            {
                int questionMarkIndex = url.IndexOf('?');

                if (questionMarkIndex == -1)
                    baseUrl = url;
                else
                    baseUrl = url.Substring(0, questionMarkIndex);
                if (questionMarkIndex == url.Length - 1)
                    return null;
                string ps = url.Substring(questionMarkIndex + 1);

                // 开始分析参数对   
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
                System.Text.RegularExpressions.MatchCollection mc = re.Matches(ps);

                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
                }

            }
            catch { }
            return nvc;
        }
    }
}
