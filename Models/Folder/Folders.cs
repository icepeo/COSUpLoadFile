using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COSUpLoadFile.Models
{
    public class Folders
    {
        public string context { get; set; }
        public bool has_more { get; set; }
        public int dircount { get; set; }
        public int filecount { get; set; }
        public List<FolderProperty> infos { get; set; }
    }
}
