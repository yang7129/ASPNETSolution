using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
namespace MVC.Controllers
{
    public class GetAndPostController : Controller
    {
        // GET: GetAndPost
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            string Url = form["InUrl"];


            ViewBag.Msg = "回傳得到內容為:" + "<br />" + "Post" + "<br />" + PostResponse(Url, "") + "<br />" + "Get" + "<br />" + GetResponse(Url) + "<br />";
            return View();
        }
        #region 商品
        //此方法若是HTTPS會多顯示一頁面是否繼續
        private static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public string PostResponse(string restServerUrl, string PostString)
        {
            byte[] parameterString = Encoding.UTF8.GetBytes(PostString);
            ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;
            HttpWebRequest WebRequest = (HttpWebRequest)HttpWebRequest.Create(restServerUrl.Trim());
            WebRequest.Method = "POST";
            WebRequest.ContentType = "application/x-www-form-urlencoded"; //有可能會變更   //"text/xml"
            WebRequest.ContentLength = parameterString.Length;
            //WebRequest.Timeout = 5 * 60 * 1000; // 5 minutes
            //等待要求逾時之前的毫秒數。預設值為 100,000 毫秒 (100 秒)。
            Stream newStream = WebRequest.GetRequestStream();
            newStream.Write(parameterString, 0, parameterString.Length);
            newStream.Close();

            HttpWebResponse WebResponse = (HttpWebResponse)WebRequest.GetResponse();

            StreamReader sr = new StreamReader(WebResponse.GetResponseStream(), Encoding.UTF8);
            //Convert the stream to a string
            string ReturnString = sr.ReadToEnd();
            sr.Close();
            WebResponse.Close();

            return ReturnString;
        }
        public string GetResponse(string ResponseWord)
        {
            string GetResponse = "";
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ValidateCertificate;
                HttpWebRequest oHttpRequest = (HttpWebRequest)WebRequest.Create(ResponseWord);
                //oHttpRequest.Timeout =  5 * 60 * 1000;
                HttpWebResponse ohttpResponse = (HttpWebResponse)oHttpRequest.GetResponse(); //oHttpRequest.GetResponse;
                System.IO.Stream MyStream;
                MyStream = ohttpResponse.GetResponseStream();
                System.IO.StreamReader StreamReader = new System.IO.StreamReader(MyStream, System.Text.Encoding.UTF8);  //目前適用UTF8 若有改變則必須更換

                GetResponse = StreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "GetResponse失敗|" + ResponseWord + "|" + ex.Message;
                //Throw New Exception("GetResponse失敗|" & ResponseWord & "|" & ex.Message)
            }
            return GetResponse;
        }
        #endregion
    }
}