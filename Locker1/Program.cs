using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker1
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new LockerDemo();
            demo.ExecuteNonLocker();
            demo.ExecuteByLocker();

            Console.ReadLine();
        }
    }

    public class LockerDemo
    {
        public LockerDemo()
        {
            _Items = new List<long>();
        }

        public void ExecuteNonLocker()
        {
            _Items.Clear();
            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (long i = 0; i < 2000000; i++)
            {
                _Items.Add(i);            
            }

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        public void ExecuteByLocker()
        {
            _Items.Clear();
            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (long i = 0; i < 2000000; i++)
            {
                lock (_Locker)
                {
                    _Items.Add(i);
                }
            }

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);            
        }

        private static readonly object _Locker = new object();
        private List<long> _Items;
    }
}
