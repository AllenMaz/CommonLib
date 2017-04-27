using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUtility;

namespace CommonLibUnitTest
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Log.Write("unit", "测试", LogLevel.All);
        }
    }
}
