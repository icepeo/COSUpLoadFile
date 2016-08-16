using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace COSUpLoadFile.Common
{
    public static class Serializes
    {
        public static void MySerialize<T>(T s, string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, s);

                fileStream.Close();
                //Debug.Log("序列化成功");
            }
            catch (Exception e)
            {
                var e1 = e.Message;
            }

        }

        public static T MyDeSerialize<T>(string path) where T : class
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            T s = formatter.Deserialize(fileStream) as T;
            fileStream.Close();
            return s;
        }
    }
}
