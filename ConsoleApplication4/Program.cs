using MyLog;
using Newtonsoft.Json;
using OTP.Framework.Contract;
using OTP.WebAPI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        public enum ExamTimeState
        {
            /// <summary>
            /// 已过期
            /// </summary>
            Ended,

            /// <summary>
            /// 进行中
            /// </summary>
            Starting,

            /// <summary>
            /// 已完成
            /// </summary>
            HasCompleted,

            /// <summary>
            /// 未开始
            /// </summary>
            NotStart
        }


        public static string GetJsonEnum(Type enumType, string alias)
        {
            int[] values = (int[])Enum.GetValues(enumType);
            string[] names = Enum.GetNames(enumType);
            string[] pairs = new string[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                pairs[i] = names[i] + ": " + values[i];
            }

            if (string.IsNullOrEmpty(alias))
            {
                alias = enumType.Name;
            }            

            return string.Format("var {0} = {{{1}}} ", alias, string.Join(", ", pairs));
        }

        static void Main(string[] args)
        {
            ////删除初始化代码,改为在配置文件中设置
            //Trace.Listeners.Clear();  //清除系统监听器 (就是输出到Console的那个)
            //Trace.Listeners.Add(new MyTraceListener(AppDomain.CurrentDomain.BaseDirectory)); //添加MyTraceListener实例
            //Test();

            //DownloadTaskAsync();

            //var list = GenerateRandom(1, 10, 9);

            //DisposeTester tester = new DisposeTester();
            //tester.Start();
            //tester.Stop();
            //tester = null;

            //FileWriter fw = new FileWriter();
            //fw.Write("sfdsfsfs\r\nsfdsfdsfdsfs计算员工工资出现异常sfdsfsfs");            
            
            //#if TEST1

            //            Console.WriteLine("TEST1");

            //#endif

            //#if TEST2

            //            Console.WriteLine("TEST2");

            //#endif

            //var s = DateTimeToUnixTimestamp(DateTime.Now);

            //List<string> a = new List<string>();

            //var x = a.Where(p => string.IsNullOrEmpty(p)).ToList();
            //MethodA();

            //JsonConvert.SerializeObject()

            //var s = GetJsonEnum(typeof(ExamTimeState), "a");

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("         this.{0} = function (input, httpParams) {{\n", "Getss");

            //BuildAngularJsServices();

            //ConcurrentBag<string> list = new ConcurrentBag<string>();
            

            //Console.WriteLine(sb.ToString());

            //ThreadTester tester = new ThreadTester();
            //tester.Start();

            //Thread.Sleep(10000);

            //tester.Stop();


            //List<string> list = new List<string>();
            //list.Add("A"); list.Add("B"); list.Add("C"); list.Add("A"); list.Add("E");

            //list.ForEach(item =>
            //{
            //    list.Remove(item);
            //});

            //list.RemoveAll(p => p.Equals("A"));


            //int x = 1000, y = 10000;
            //int[] valueArray = new int[x * y];
            //Hashtable valueHT = new Hashtable();
            //Dictionary<int, int> valueDict = new Dictionary<int, int>();

            //for (int i = 0; i < x * y; i++)
            //{
            //    var value = new Random().Next(int.MaxValue);
            //    valueArray[i] = value;
            //    //valueHT.Add(i.ToString(), value);
            //   // valueDict.Add(i, value);
            //}

            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //for (int i = 0; i < x * y; i++)
            //{
            //    int v = valueArray[i];              
            //}
            //sw.Stop();
            //Console.WriteLine("数组用时：" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //for (int i = 0; i < x * y; i++)
            //{
            //    var v = valueHT[i];
            //}  
            //sw.Stop();
            //Console.WriteLine("Hashtable用时：" + sw.ElapsedMilliseconds);

            //sw.Restart();
            //for (int i = 0; i < x * y; i++)
            //{
            //    var v = valueDict[i];
            //}  
            //sw.Stop();
            //Console.WriteLine("Dictionary用时：" + sw.ElapsedMilliseconds);


            //var str = "0000002";
            //var b = int.Parse(str);


            //var list = new List<DataObject>();

            //var amount = list.Sum(p => p.Amount);

            var a = 3 % 2;

            Console.ReadLine();
        }

        private class DataObject
        {
            public decimal Amount
            {
                get;
                set;
            }
        }

        private static void BuildAngularJsServices()
        {
            StringBuilder _Scripts = new StringBuilder();

            var types = Assembly.Load("OTP.WebAPI").GetTypes().Where(t => t.BaseType == typeof(BaseApiController)).ToArray();

            _Scripts.AppendLine();
            _Scripts.Append("(function(App){\n\n");
            _Scripts.Append("   var module = angular.module('otpApp');\n\n");

            foreach (var type in types)
            {
                if (type != typeof(ExamAPIController))
                {
                    continue;
                }

                var controllerName = type.Name.Replace("Controller", "");
                var serviceName = GetFirstLowerStr(controllerName.Replace("API", ""));

                _Scripts.AppendFormat("   module.factory('{0}', [\n", serviceName);
                _Scripts.Append("       '$http', function ($http) {\n");
                _Scripts.Append("           return new function () {\n");

                var methods = type.GetMethods().Where(t => t.GetParameters().Length == 1 && t.GetParameters()[0].Name == "input").ToArray();
                foreach (var method in methods)
                {
                    var actionName = method.Name;
                    var jsFuncName = GetFirstLowerStr(actionName);

                    _Scripts.AppendFormat("             this.{0} = function (input, httpParams) {{\n", jsFuncName);
                    _Scripts.Append("               return $http(angular.extend({\n");
                    _Scripts.AppendFormat("                 url: '/api/{0}/{1}',\n", controllerName, actionName);
                    _Scripts.Append("                 method: 'POST',\n");
                    _Scripts.Append("                 data: JSON.stringify(input)\n");
                    _Scripts.Append("               }, httpParams));\n");
                    _Scripts.Append("             };\n\n");
                }

                _Scripts.Append("           };\n");
                _Scripts.Append("       }\n");
                _Scripts.Append("   ]);\n\n");
            }

            _Scripts.Append("\n})(App || (App = {}));");
            _Scripts.AppendLine();
        }

        private static string GetFirstLowerStr(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length > 1)
                {
                    return char.ToLower(s[0]) + s.Substring(1);
                }
                return char.ToLower(s[0]).ToString();
            }
            return null;
        }

        public static void MethodA()
        {
            Console.WriteLine("A START");

            MethodB();

            Console.WriteLine("A END");
        }

        public static void MethodB()
        {
            return;
        }

        public static long DateTimeToUnixTimestamp(DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return (long)ts.TotalMilliseconds;
        }






















        //private static List<int> GenerateRandom(int start, int end, int num)
        //{
        //    long lTick = DateTime.Now.Ticks;
        //    List<int> lstRet = new List<int>();
        //    for (int i = start; i < num; i++)
        //    {
        //        Random ran = new Random((int)lTick * i);
        //        int iTmp = ran.Next(end);
        //        lstRet.Add(iTmp);
        //        lTick += (new Random((int)lTick).Next(978));
        //    }
        //    return lstRet;
        //}

        private static int[] GenerateRandom(int start, int end, int num)
        {
            List<int> range = new List<int>(); ;

            //设置初始值
            for (int i = start; i <= end; i++)
            {
                range.Add(i);
            }

            Random rd = new Random();

            //要得到15个随机数
            int[] numList = new int[num];

            for (int i = 0; i < num; i++)
            {
                //临时变量
                int temp = rd.Next(range.Count);

                //取随机数
                numList[i] = range[temp];

                range.Remove(range[temp]);
            }

            return numList;
        }

        protected static async void DownloadTaskAsync()
        {
            WebClient client = new WebClient();
            var result1 = await client.DownloadStringTaskAsync("http://www.baidu.com");
            WebClient client2 = new WebClient();
            var result2 = await client.DownloadStringTaskAsync(result1);
            //do more 
        }

        private static void Test()
        {
            try
            {
                int i = 0;
                Console.WriteLine(5 / i); //出现除0异常
            }
            catch (Exception ex)
            {
                Trace.Write(ex, "计算员工工资出现异常");
            }
        }
    }

    public class ThreadTester
    {
        public ThreadTester()
        {

        }

        public void Start()
        {

            if (_Thread == null || !_Thread.IsAlive)
            {
                // release unmanaged code resource.
                if (_Thread != null && !_Thread.IsAlive)
                {
                    try
                    {
                        _Thread.Abort();
                        _Thread = null;
                    }
                    catch (ThreadAbortException)
                    {
                        // do nothing here
                    }
                }

                _Thread = new Thread(Execute);
                _Thread.IsBackground = true;
                _Thread.Start();
            }
        }

        public void Stop()
        {
            //_Thread.Abort();
            //_Thread = null;
            IsExit = true;
            while (true)
            {
                if (_Thread.IsAlive)
                {
                    Console.WriteLine("1");
                }
                Thread.Sleep(1000);
            }
        }

        private void Execute()
        {
            while (!IsExit)
            {
                Console.WriteLine(DateTime.Now.ToString());
                Thread.Sleep(1000);
            }
        }

        private Thread _Thread;
        private bool IsExit = false;
    }

    public class AsyncTest
    {
        public AsyncTest()
        {
        }

        public async void Clear()
        {
            await Wait();

            Console.WriteLine("wait end");
        }

        private Task Wait()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
            });
        }

    }

    public class DisposeTester
    {
        public void Start()
        {
            _Source = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_Source.IsCancellationRequested)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        public void Stop()
        {
            if (_Source != null)
            {
                _Source.Cancel();
            }
        }

        private CancellationTokenSource _Source;
    }

    public class FileWriter
    {
        public FileWriter()
        {
            _FileDir = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString());
        }

        public void Write(string text)
        {
            File.WriteAllText(_FileDir, text, Encoding.Default);
        }

        private string _FileDir;
    }

    #region 记录方法执行时间AOP

    ///// <summary>
    ///// 标记要性能监测的类
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Class)]
    //public class MonitorServiceAttribute : ProxyAttribute
    //{
    //    public override MarshalByRefObject CreateInstance(Type serverType)
    //    {
    //        var instance = base.CreateInstance(serverType);
    //        var proxy = new InjectProxy(serverType, instance).GetTransparentProxy();
    //        return proxy as MarshalByRefObject;
    //    }
    //}

    //public class InjectProxy : RealProxy
    //{
    //    private MarshalByRefObject _Instance = null;

    //    public InjectProxy(Type type, MarshalByRefObject instance)
    //        : base(type)
    //    {
    //        _Instance = instance;
    //    }

   

    //    public override IMessage Invoke(IMessage msg)
    //    {
    //        var call = (IMethodCallMessage)msg;
    //        var ctr = call as IConstructionCallMessage;

    //        IMethodReturnMessage back;
    //        if (ctr != null)
    //        {
    //            RealProxy defaultProxy = RemotingServices.GetRealProxy(_Instance);
    //            defaultProxy.InitializeServerObject(ctr);
    //            back = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctr,
    //                (MarshalByRefObject)GetTransparentProxy());
    //        }
    //        else
    //        {
    //            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
    //            var sw = Stopwatch.StartNew();
    //            back = RemotingServices.ExecuteMessage(_Instance, call);
    //            long second = sw.ElapsedMilliseconds;
    //            var attr = MethodAttributeCache.GetAttribute<MonitorMethodAttribute>(_Instance.GetType().TypeHandle,
    //                call.MethodBase);

    //            if (attr != null)
    //            {
    //                attr.Value = second.ToString();
    //                attr.ExecuteTime = now;
    //                Queue.Enqueue(Mapper(attr));
    //            }
    //        }
    //        return back;
    //    }
    //}


    #endregion
}
