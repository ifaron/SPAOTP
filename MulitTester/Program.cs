using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MulitTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var tester = new Tester();

            var tasks = new List<Task>();

            for (var i = 0; i < 1000; i++)
            {
                var task = tester.Add();
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine(tester.List.Count);
            Console.ReadLine();
        }  

        //static void Main(string[] args)
        //{
        //    var tester2 = new Tester2();

        //    var tasks = new List<Task<int[]>>();

        //    for (var i = 0; i <= 10000; i++)
        //    {
        //        var task = Task.Factory.StartNew(() =>
        //        {
        //            return tester2.GetLists();
        //        });
        //        tasks.Add(task);
        //    }

        //    Task.WaitAll(tasks.ToArray());

        //    foreach(var task in tasks)
        //    {
        //        Console.WriteLine(task.Result.Distinct().Count());
        //    }

        //    Console.ReadLine();                      
        //}
    }

    public class Tester
    {
        public Tester()
        {
            _List = ArrayList.Synchronized(new ArrayList());
            //_List = new ArrayList();
        }

        public Task Add()
        {
            return Task.Factory.StartNew(() =>
            {                
                _List.Add(0);
            });
        }

        public ArrayList List
        {
            get
            {
                return _List;
            }
        }

        private ArrayList _List;
    }

    public class Tester2
    {
        public Tester2()
        {
            _List = new List<int>();
            for(var i=0;i<1000;i++)
            {
                _List.Add(i);
            }
        }

        public int[] GetLists()
        {
            return _List.ToArray();
        }

        private List<int> _List;
    }
}
