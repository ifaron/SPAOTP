using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var data = IntToHexByte(255);

            //var bytes = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 };

            //var a = BitConverter.ToInt32(bytes, 0);

            //var b = BitConverter.GetBytes(600);

            var a = 1;

            object o = 1;

            var o1 = o.ToString();

            byte b1 = byte.Parse("255");

            string dd = "sfdfdsfsdfds";

            //var b = BitConverter.GetBytes()

            var floatBytes = BitConverter.GetBytes(Convert.ToSingle(10222222221222222.22222333333222));

            var c = CreateBoardAndSlotByte();

            Console.ReadKey();
        }

        public static byte IntToHexByte(int value)
        {
            string strValue = value.ToString("x8");
            return byte.Parse(strValue, NumberStyles.AllowHexSpecifier);
        }

        private static byte CreateBoardAndSlotByte()
        {
            var index = int.Parse(string.Format("{0}{1}", 1, 1));
            return (byte)((index / 10) * 16 + index % 10);
        }
    }
}
