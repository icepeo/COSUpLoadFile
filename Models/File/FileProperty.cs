using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSUpLoadFile.Models
{
    public class FileProperty
    {
        public string name { get; set; }
        public string biz_attr { get; set; }
        public string ctime { get; set; }
        public string mtime { get; set; }
        public int filesize { get; set; }
        public int filelen { get; set; }
        public string sha { get; set; }
        public string access_url { get; set; }
    }
}
