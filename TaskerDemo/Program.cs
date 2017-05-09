using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tasker = new Tasker();
            //tasker.Start();
            //tasker.Stop();
            //tasker.Print();
            var listA = new List<string> { "a", "b", "c" };
            //var listB = new List<string> { "11", "22", "33", "a" };

            //var res = listA.Where(p => !listB.Contains(p)).ToList();


            Console.ReadLine();
        }
    }

    public class Tasker
    {
        public Tasker()
        {

        }

        public void Start()
        {
            _TokenResource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                while (!_TokenResource.IsCancellationRequested)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(_TokenResource.IsCancellationRequested);
                }
            }, _TokenResource.Token);
        }

        public void Stop()
        {
            _TokenResource.Cancel(); //立即取消
            //_TokenResource.CancelAfter(3000);//延时3秒后取消
        }

        public void Print()
        {                        
            Console.WriteLine(_TokenResource.IsCancellationRequested);
        }

        private CancellationTokenSource _TokenResource;
    }
}
