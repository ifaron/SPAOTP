using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class UserInfo
    {
        [Custom("用户ID")]
        public string Id
        {
            get;
            set;
        }

        [Custom("用户姓名")]
        public string Name
        {
            get;
            set;
        }
    }
}
