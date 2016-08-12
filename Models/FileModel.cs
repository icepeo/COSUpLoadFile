using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSUpLoadFile.Models
{
    [Serializable]
    public class FileModel
    {
        public string filepath { get; set; }
        public string filesize { get; set; }
        public string filetype { get; set; }
        public int fileprogress { get; set; }
        public long filesizes { get; set; }
        public int fileindex { get; set; } //原列表中所在的Index
        public string filefrontdir { get; set; }  //上传列表中文件的目录路径
    }
}
