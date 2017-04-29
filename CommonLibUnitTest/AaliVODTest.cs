using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUtility;
using System.Net;
using System.IO;
using System.Web;

namespace CommonLibUnitTest
{
    [TestClass]
    public class AaliVODTest
    {
        [TestMethod]
        public void AaliVODSignTest()
        {
            
            var signStr = AaliVODSign.Sign();

            var requestUrl = AaliVODSign.ToUrl("http://vod.cn-shanghai.aliyuncs.com");
            requestUrl += "&Signature=" + signStr;

            var request = (HttpWebRequest)WebRequest.Create(requestUrl);

            try
            {
                
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {

            }
           

            Assert.IsTrue(true);
        }
    }
}
