using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Class1
    {
        public void GetCurrentAssembly()
        {
            var a1 = Assembly.GetExecutingAssembly();
            var a2 = Assembly.GetEntryAssembly();
            var a3 = Assembly.GetCallingAssembly();
            var a4 = Assembly.GetAssembly(this.GetType());
        }
    }
}
