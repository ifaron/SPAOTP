using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class CustomAttributeHelper
    {
        public Dictionary<string, string> GetColumns(Type type)
        {
            var columns = new Dictionary<string, string>();

            var attributeType = typeof(CustomAttribute);
            var props = type.GetProperties().Where(p => p.IsDefined(typeof(CustomAttribute), false)).ToList();
            foreach(var prop in props)
            {
                var customAttr = prop.GetCustomAttributes(attributeType, false)[0] as CustomAttribute;
                columns.Add(prop.Name, customAttr.Message);
            }

            return columns;
        }
    }
}
