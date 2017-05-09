using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoker
{
    class Program
    {
        static void Main(string[] args)
        {
            uint result = 0;

            var invoker = new SunytechAPIDynamicInvoker();
            invoker.LoadLibrary();

            result = invoker.Connect();
            result = invoker.Disconnect();

            invoker.FreeLibrary();

        }
    }
}
