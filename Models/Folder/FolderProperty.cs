using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSUpLoadFile.Models
{
    public class FolderProperty
    {
        public string name { get; set; }
        public string biz_attr { get; set; }
        public long filesize { get; set; }
        public int filelen { get; set; }
        public string sha { get; set; }
        public double ctime { get; set; }
        public double mtime { get; set; }
        public string access_url { get; set; }
    }
}
