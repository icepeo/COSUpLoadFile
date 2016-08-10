using System;
using System.Configuration;
using COSUpLoadFile.Common;

namespace COSUpLoadFile
{
    public class GlobelSet
    {
        private static int _APP_ID = int.Parse(ConfigurationManager.AppSettings["APP_ID"]);
        private static string _SECRET_ID = ConfigurationManager.AppSettings["SECRET_ID"];
        private static string _SECRET_KEY = ConfigurationManager.AppSettings["SECRET_KEY"];
        private static string _BucketName = ConfigurationManager.AppSettings["bucketName"];
        private static int pagesize = 199;
        private static int threadnum = 1;
        private static string stringpath = System.AppDomain.CurrentDomain.BaseDirectory + @"list.dat";
        private static string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + @"config.xml";
        private static object obj = new object();
        //private List<UpLoad.Models.Serialize.Files> FilesList;
        private static string curbucketName;
        private static XmlHelper xml;

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
    }
}
