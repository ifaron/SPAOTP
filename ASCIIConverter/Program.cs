using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytetest = System.Text.Encoding.Default.GetBytes("$");

            Console.ReadKey();
        }
    }
}
