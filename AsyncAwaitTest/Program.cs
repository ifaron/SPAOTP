using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始");

            new Tester().Test();

            Console.WriteLine("结束");

            Console.ReadLine();
        }
    }

    public class Tester
    {
        public async Task Test()
        {
            await new Tester().Start();
        }

        public async Task Start()
        {
            await Task.Factory.StartNew(() =>
            {
                var result1 = CreateString(10000000);

                var result2 = CreateString(10000000);

                Console.WriteLine(result1.Result.Length + result2.Result.Length);
            });
        }


        private async Task<string> CreateString(int count)
        {
            return await Task.Factory.StartNew<string>(() =>
            {
                StringBuilder sb = new StringBuilder();
                while (count > 0)
                {
                    sb.Append(count);
                    count--;
                }

                return sb.ToString();
            });
        }
    }
}
