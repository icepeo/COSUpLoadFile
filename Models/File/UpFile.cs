using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSUpLoadFile.Models
{
    /// <summary>
    /// 上传文件所需参数模版类
    /// </summary>
    public class UpFile
    {
        public string filename { get; set; } //文件对话框中选定的文件名的字符串
        public long filenamelen { get; set; }  //文件对话框中选定的文件实际大小
        public string currentfolder { get; set; } //当前路径
    }
}
