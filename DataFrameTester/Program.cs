using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrameTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var tester = new ResolveTester();
            tester.Init();
            tester.Execute();

            //var tester = new XORTester();
            //tester.Execute();

            Console.ReadLine();
        }
    }

    public class ResolveTester
    {
        private Queue<byte[]> _ReceivedQueue;
        private List<byte> _ReceivedDatas;

        public ResolveTester()
        {
            _ReceivedDatas = new List<byte>();
        }

        public void Init()
        {
            _ReceivedDatas.AddRange(new byte[] { 13, 05, 13, 04, 00, 00, 00, 00, 31, 13, 10, 00, 13, 05, 00, 00, 13, 05, 00, 00, 13, 10 });//0D 05 13 04 00 00 00 00 1F 0D 0A
        }

        public void Execute()
        {
            if (_ReceivedQueue == null)
            {
                _ReceivedQueue = new Queue<byte[]>();
            }

            var startIndex = -1;
            var endIndex = -1;
            var lastEndIndex = -1;

            for (var i = 0; i < _ReceivedDatas.Count; i++)
            {
                if (_ReceivedDatas[i] == 13 && _ReceivedDatas.Count > i + 1 && _ReceivedDatas[i + 1] == 5)
                {
                    startIndex = i;
                }

                if (_ReceivedDatas[i] == 13 && _ReceivedDatas.Count > i + 1 && _ReceivedDatas[i + 1] == 10)
                {
                    endIndex = i;
                    lastEndIndex = i;
                }

                if (startIndex > -1 && endIndex > -1)
                {
                    var bytes = _ReceivedDatas.GetRange(startIndex, endIndex - startIndex + 2).ToArray();
                    _ReceivedQueue.Enqueue(bytes);

                    startIndex = -1;
                    endIndex = -1;
                }
            }

            if (lastEndIndex > -1)
            {
                _ReceivedDatas.RemoveRange(0, lastEndIndex + 2);
            }

            while (_ReceivedQueue.Count > 0)
            {
                var bytes = _ReceivedQueue.Dequeue();

                Console.WriteLine(string.Join(" ", bytes));
            }
        }
    }

    /// <summary>
    /// 判断检验值
    /// </summary>
    public class XORTester
    {
        public void Execute()
        {
            var bytes = new byte[] { 13, 05, 13, 07, 00, 00, 13, 00, 00, 00, 00, 10, 13, 10 };

            var a1 = CheckSum1(bytes);
            var a2 = CheckSum2(bytes);
            var b = bytes[bytes[3] + 4];

            var c = 07 ^ 00 ^ 00 ^ 13 ^ 00 ^ 00 ^ 00 ^ 00;

            var isTrue = a2 == b;
        }

        /// <summary>
        /// 第一版检验逻辑
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte CheckSum1(byte[] data)
        {
            byte[] checkData = new byte[data[3] + 3];
            Array.Copy(data, 2, checkData, 0, checkData.Length);

            byte xorResult = 0;
            for (int i = 0; i < checkData.Length - 1; i++)
            {
                xorResult ^= checkData[i];
            }

            return IntToHexByte(xorResult);
        }

        /// <summary>
        /// 第二版检验逻辑
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte CheckSum2(byte[] data)
        {
            int xorResult = data[3];

            for (var i = 4; i <= data[3] + 3; i++)
            {
                var temp = data[i];
                xorResult ^= temp;
            }

            return IntToHexByte(xorResult);
        }

        private byte IntToHexByte(int value)
        {
            string strValue = value.ToString("x8");
            return byte.Parse(strValue, NumberStyles.AllowHexSpecifier);
        }
    }
}
