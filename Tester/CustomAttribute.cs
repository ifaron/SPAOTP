using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class CustomAttribute : Attribute
    {
        public CustomAttribute(string message)
        {
            Message = message;
        }

        public string Message
        {
            get;
            set;
        }
    }
}
