using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace AssembyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //var assemby = Assembly.Load("OTP.WebAPI");

            var a = Assembly.GetAssembly(typeof(Class1)).CreateInstance("TestProject.Class1");
            new Test().Execute();
            
        }

        private class Test
        {
            public Test()
            {

            }

            public void Execute()
            {
                var type = this.GetType();
                var a = this.GetType().DeclaringType + "." + this.GetType().Name;
            }
        }
    }
}
