using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using Newtonsoft.Json;
using ContractForUnity;
using System.Diagnostics;
using System.Net;
using System.IO;
using ProxyForUnity;
using System.Threading;

namespace UnityWebApiProxyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;

            var p1 = new string[] { "a", "b" };
            var p2 = new double[] { 1, 2 };

            Stopwatch sw = new Stopwatch();
   
            while (true)
            {
                sw.Reset();
                sw.Start();

                var result = UnityApiServiceProxy.Instance.GetValuesFromOTS(new string[]{});

                sw.Stop();

                Console.WriteLine("本次测试，耗时{0}毫秒", sw.ElapsedMilliseconds);

                Thread.Sleep(1000);
            }

        

            //UnityApiServiceProxy.Instance.GetExecuteScript();

            Console.WriteLine("按任意键退出");
            Console.ReadKey();

            UnityApiServiceProxy.Instance.Dispose();

            Console.WriteLine("本次测试，共调用{0}次，耗时{1}毫秒；平时耗时{2}毫秒", count, sw.ElapsedMilliseconds, sw.ElapsedMilliseconds / count);

            

         
            //UnityApiServiceProxy.Instance.AnswerToQuestion(p1);
            //UnityApiServiceProxy.Instance.GetExecuteInterPhoneInformation();
            //UnityApiServiceProxy.Instance.GetExecuteQuestionInformation();
            //UnityApiServiceProxy.Instance.GetExecuteScript();
            //UnityApiServiceProxy.Instance.GetExecutorCustomScript();
            //UnityApiServiceProxy.Instance.GetExecutorTime();
            //UnityApiServiceProxy.Instance.GetExecutorTimeState();
            //UnityApiServiceProxy.Instance.GetPlayerLocation();
            //UnityApiServiceProxy.Instance.GetTagFullpaths();
            //UnityApiServiceProxy.Instance.GetValuesFromOTS(p1);          
            //UnityApiServiceProxy.Instance.SetValuesToOTS(null);
            //UnityApiServiceProxy.Instance.TriggerObjectIdentify(string.Empty);
            //UnityApiServiceProxy.Instance.TriggerPlayerLocation(string.Empty);
            //UnityApiServiceProxy.Instance.UpdateTags(null);    

            //UnityApiServiceProxy.Instance.NotifyCustomScriptExecuted(true);


            Console.Write("End");
            Console.ReadLine();
        }


        //static void TestPost()
        //{
        //    string jsonString = JsonConvert.SerializeObject(new GetValuesFromOTSRequest
        //    {
        //        tags = null
        //    });

        //    //url get it from config  
        //    string url = "http://localhost:9000/api/unity/TestPost0";
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    request.ContentType = "application/json; charset=utf-8";
        //    request.Method = "POST";

        //    //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //    //{
        //    //    streamWriter.Write(jsonString);
        //    //    streamWriter.Flush();
        //    //}
  
        //    var httpResponse = (HttpWebResponse)request.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        var result = streamReader.ReadToEnd();
        //        var res = JsonConvert.DeserializeObject<Dictionary<string, double>>(result);
        //    } 
        //}

        //static void TestGet()
        //{
        //    var p1 = new string[] { "a", "b" };

        //    var p2 = "d";

        //    var p3 = new GetValuesFromOTSRequest
        //    {
        //        tags = null
        //    };

        //    string jsonString = JsonConvert.SerializeObject(p3);
        //    //url get it from config  
        //    string url = "http://localhost:9000/api/unity/TestGet3";
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    request.ContentType = "application/json; charset=utf-8";
        //    request.Method = "GET";            

        //    //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //    //{
        //    //    streamWriter.Write(jsonString);
        //    //    streamWriter.Flush();
        //    //}
        //    //System.Diagnostics.Debug.WriteLine("there");  
        //    //return "";  
        //    var httpResponse = (HttpWebResponse)request.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        var result = streamReader.ReadToEnd();
        //        var res = JsonConvert.DeserializeObject<Dictionary<string, double>>(result);
        //    } 
        //}
    } 
}
