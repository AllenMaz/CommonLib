using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibUnitTest
{
    [TestClass]
    public class JsonHandlerTest
    {
        [TestMethod]
        public void LitJsonTest()
        {
            TestModel model = new TestModel();
            model.UserName = "Test";
            model.Email = "12222@qq.com";
            var json = "{\"Email\":\"12222@qq.com\",\"UserName\":\"Test\"}";

            var jsonStr = LitJson.JsonMapper.ToJson(model);
            var unJson = LitJson.JsonMapper.ToObject<TestModel>(json);

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void NewtoJsonTest()
        {

            Assert.IsTrue(true);

        }
    }

    public class TestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
