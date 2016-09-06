using System;
using System.Configuration;
using COSUpLoadFile.Common;

namespace COSUpLoadFile
{
    public class GlobelSet
    {
        #region 私有
        private static int _APP_ID = int.Parse(ConfigurationManager.AppSettings["APP_ID"]);
        private static string _SECRET_ID = ConfigurationManager.AppSettings["SECRET_ID"];
        private static string _SECRET_KEY = ConfigurationManager.AppSettings["SECRET_KEY"];
        private static string _BucketName = ConfigurationManager.AppSettings["bucketName"];
        private static int _sliceSize = 9216;
        private static object obj = new object();        
        private static XmlHelper xml;
        #endregion

        #region 公有
        public static string stringpath = System.AppDomain.CurrentDomain.BaseDirectory + @"list.dat";
        public static string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + @"config.xml";
        public static int pagesize = 199;
        public static int threadnum = 1;
        public static string _curbucketName;
        
        #endregion

        static GlobelSet()
        {
            xml = new XmlHelper(xmlpath);
        }

        public static int APP_ID
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/appid") != null)
                {
                    return int.Parse(xml.GetXmlNode(xmlpath, "config/appid").InnerText);
                }
                else
                {
                    return _APP_ID;
                }
            }
            set
            {
                _APP_ID = value;
            }
        }

        public static string SECRET_ID
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/secretid") != null)
                {
                    return xml.GetXmlNode(xmlpath, "config/secretid").InnerText;
                }
                else
                {
                    return _SECRET_ID;
                }
            }
            set
            {
                _SECRET_ID = value;
            }
        }

        public static string SECRET_KEY
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/secretkey") != null)
                {
                    return xml.GetXmlNode(xmlpath, "config/secretkey").InnerText;
                }
                else
                {
                    return _SECRET_KEY;
                }
            }
            set
            {
                _SECRET_KEY = value;
            }
        }

        public static string BucketName
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/bucketName") != null)
                {
                    return xml.GetXmlNode(xmlpath, "config/bucketName").InnerText;
                }
                else
                {
                    return _BucketName;
                }
            }
            set
            {
                _BucketName = value;
            }
        }

        public static int ThreadNum
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/thread") != null)
                {
                    return int.Parse(xml.GetXmlNode(xmlpath, "config/thread").InnerText);
                }
                else
                {
                    return threadnum;
                }
            }
        }

        public static int SliceSize
        {
            get
            {
                if (xml.GetXmlNode(xmlpath, "config/sliceSize") != null)
                {
                    return int.Parse(xml.GetXmlNode(xmlpath, "config/sliceSize").InnerText);
                }
                else
                {
                    return _sliceSize;
                }
            }
        }
    }
}
