using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using XiaoYingZhou.Core.DataPack;

namespace XmlReaderTester
{
    class Program
    {
        static void Main(string[] args)
        {                        
            var fileName = System.Configuration.ConfigurationManager.AppSettings.Get("ModelFile");
            var testCount = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("TestCount"));

            var tester = new XmlReaderTester();

            var currentCount = 0;
            while (currentCount < testCount)
            {
                Thread.Sleep(2000);

                currentCount++;

                var hasError = false;

                try
                {
                    var result = tester.Open(fileName);
                    hasError = result != 0;
                }
                catch (ApplicationException aex)
                {
                    hasError = true;
                }

                Console.WriteLine("第{0}轮,has error:{1}", currentCount, hasError);
            }

            Console.ReadLine();
        }
    }

    public class XmlReaderTester
    {
        public XmlReaderTester()
        {

        }

        public uint Open(string fileName)
        {
            var settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                CheckCharacters = false,
                CloseInput = true,
                IgnoreProcessingInstructions = true,
                IgnoreComments = true,
            };

            using (var xmlReader = XmlReader.Create(fileName, settings))
            {
                if (xmlReader.Read() && xmlReader.Name == "Core")
                {                    
                    var result = ReadXml(xmlReader.ReadSubtree());
                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            return 0;
        }

        private uint ReadXml(XmlReader xmlReader)
        {
            uint result = 0;

            using (xmlReader)
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case XML_TAGNAME_DATA:
                                result = DataMgrReadXml(xmlReader.ReadSubtree());
                                if (result != 0)
                                    return result;
                                break;
                            case XML_TAGNAME_CONFIG:
                                result = ConfigMgrReadXml(xmlReader.ReadSubtree());
                                if (result != 0)
                                    return result;
                                break;
                            case XML_TAGNAME_COMPONENT:
                                result = ReadBlocksXml(xmlReader.ReadSubtree());
                                if (result != 0)
                                    return result;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            GC.Collect();

            return result;
        }

        private uint DataMgrReadXml(XmlReader xmlReader)
        {
            uint result = 0;
            using (xmlReader)
            {
                bool isEmpty = xmlReader.IsEmptyElement;
                xmlReader.ReadStartElement();
                if (isEmpty)
                {
                    return result;
                }

                if (xmlReader.Name == "Model")
                {
                    // create a new model and restore data
                    // ensure the integrality of data
                    var newModel = new Model();
                    result = newModel.ReadXml2(xmlReader.ReadSubtree());
                    if (result != 0)
                        return result;

                    // operation completed, attach it to data manager's model
                    _Model = newModel;
                }

                xmlReader.ReadEndElement();
            }

            return result;
        }

        private uint ConfigMgrReadXml(XmlReader xmlReader)
        {
            uint result = 0;

            //using (xmlReader)
            //{
            //    while (xmlReader.Read())
            //    {
            //        if (xmlReader.IsStartElement())
            //        {
            //            switch (xmlReader.Name)
            //            {
            //                case "DataTypes":
            //                    result = _DataTypeLib.ReadXml(xmlReader.ReadSubtree());
            //                    if (result != ErrorCodes.None)
            //                        return result;
            //                    break;
            //                case "SimBasis":
            //                    result = _SimulationBasisMgr.ReadXml(xmlReader.ReadSubtree());
            //                    if (result != ErrorCodes.None)
            //                        return result;
            //                    break;
            //                case XiaoYingZhou.Core.Configurations.ScalingFactorManager.XmlTag:
            //                    result = _ScalingManager.ReadXml(xmlReader.ReadSubtree());
            //                    if (result != ErrorCodes.None)
            //                        return result;
            //                    break;
            //                case XiaoYingZhou.Core.Configurations.SimulationParameterManager.XmlTag:
            //                    result = _SimulationParameterManager.ReadXml(xmlReader.ReadSubtree());
            //                    if (result != ErrorCodes.None)
            //                        return result;
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //    }
            //}

            return result;
        }

        private uint ReadBlocksXml(XmlReader xmlReader)
        {
            uint result = 0;

            //// reset
            //_Modules.Clear();

            //var moduleKeyPath = new Dictionary<string, XiaoYingZhou.Global.Modules.IModule>();
            //var tasks = new List<Task<uint>>();
            //using (xmlReader)
            //{
            //    while (xmlReader.Read())
            //    {
            //        switch (xmlReader.Name)
            //        {
            //            case XML_TAGNAME_BLOCK:
            //                tasks.Add(BuildBlock(xmlReader.ReadSubtree(), moduleKeyPath));
            //                break;
            //            case XML_TAGNAME_LINE:
            //                Task.WaitAll(tasks.ToArray());
            //                foreach (var item in tasks)
            //                {
            //                    if (item.Result != ErrorCodes.None)
            //                        return item.Result;
            //                }
            //                tasks.Clear();

            //                tasks.Add(BuildLine(xmlReader.ReadString(), moduleKeyPath));
            //                break;
            //            case XML_TAGNAME_TXT_BLOCK:
            //                tasks.Add(BuildTextBlock(xmlReader.ReadSubtree()));
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}

            //Task.WaitAll(tasks.ToArray());

            //foreach (var item in tasks)
            //{
            //    if (item.Result != ErrorCodes.None)
            //        return item.Result;
            //}            

            return result;
        }

        private const string XML_TAGNAME_ROOT = "Project";
        private const string XML_TAGNAME_DATA = "Data";
        private const string XML_TAGNAME_CONFIG = "Config";
        private const string XML_TAGNAME_COMPONENT = "Components";

        private Model _Model;
    }
}
