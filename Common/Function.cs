using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using COSUpLoadFile.CosApi.API;
using COSUpLoadFile.Models;
using CCWin.SkinControl;
using System.Windows.Forms;
using System.IO;
using XPTable.Models;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace COSUpLoadFile.Common
{
    public class Function
    {
        /// <summary>
        /// 读取远程目录列表数据
        /// </summary>
        /// <param name="bn">bucketName</param>
        /// <param name="ml">远程路径</param>
        /// <param name="num">读取目录项数量</param>
        /// <param name="ct"></param>
        /// <param name="o"></param>
        /// <param name="fp"></param>
        /// <param name="prefix">前缀</param>
        /// <param name="mes">输出出错信息</param>
        /// <returns></returns>
        public static List<FolderProperty> GetFolderData(string bn, string ml, int num, string ct, int o, FolderPattern fp, string prefix, out string mes)
        {
            CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
            string result = "";
            mes = "";
            result = cos.GetFolderList(bn, ml, num, ct, o, fp);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            FolderModel bfm = jss.Deserialize<FolderModel>(result);
            if (bfm.code != 0 && bfm.message != "SUCCESS")
            {
                mes = "获取目录出错!错误码：" + bfm.code + ";错误信息：" + bfm.message + ";";
                return null;
            }
            return bfm.data.infos;
        }

        public static void ReadMainFolder(string path, TableModel tm,ImageList image, string CurbucketName,SkinLabel label)
        {
            string message = "";
            int count = 1;
            List<FolderProperty> getdata = GetFolderData(CurbucketName, path, GlobelSet.pagesize, "", 0, FolderPattern.Both,"",out message);
            if (getdata == null)
            {
                //MessageBox.Show("获取目录出错!");
                message = "获取目录出错!";
                tm.Rows.Clear();
                return;
            }
            tm.Rows.Clear();
            tm.Rows.Add(new Row());
            tm.Rows[0].Cells.Add(new Cell("..",image.Images[0]));
            tm.Rows[0].Tag = "-1";
            foreach (FolderProperty fp in getdata)
            {
                tm.Rows.Add(new Row());
                if (count % 2 == 0)
                {
                    tm.Rows[count].BackColor = Color.White;
                }
                else
                {
                    tm.Rows[count].BackColor = Color.WhiteSmoke;
                }
                if (!String.IsNullOrEmpty(fp.sha))
                {
                    tm.Rows[count].Tag = "1";
                    tm.Rows[count].Cells.Add(new Cell(fp.name, image.Images[0]));
                    tm.Rows[count].Cells.Add(new Cell(Function.FormatCapacity(fp.filesize)));
                }
                else
                {
                    tm.Rows[count].Tag = "0";
                    tm.Rows[count].Cells.Add(new Cell(fp.name, image.Images[0]));
                    tm.Rows[count].Cells.Add(new Cell("--"));
                }
                tm.Rows[count].Cells.Add(new Cell("--"));
                tm.Rows[count].Cells.Add(new Cell(Function.ConvertIntDateTime(fp.ctime).ToString()));
                tm.Rows[count].Cells.Add(new Cell(""));                
                count = count + 1;                
            }
            label.Text = "/" + CurbucketName + path;
        }

        #region 通用
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(DateTime time)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
        /// <summary>
        /// 格式化容量大小B,KB,MB,GB
        /// </summary>
        /// <param name="fileSize">容量</param>
        /// <returns>string</returns>
        public static string FormatCapacity(long fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0}Bytes", fileSize);
            }
        }

        /// <summary>
        /// 上传路径中所对应的文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>long</returns>
        //
        public static long FileSize(string filePath)
        {
            //定义一个FileInfo对象，是指与filePath所指向的文件相关联，以获取其大小
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        /// <summary>
        /// 替换路径目录
        /// </summary>
        /// <param name="folderPath">路径</param>
        /// <returns>string</returns>
        public static string FolderPath(string folderPath, string buckName, Boolean flag = true)
        {
            string res = "";
            if (flag)
            {
                res = folderPath.Replace("/" + buckName, "");//进入下级目录
            }
            else
            {
                folderPath = folderPath.TrimEnd('/');
                folderPath = folderPath.Substring(0, folderPath.LastIndexOf('/') + 1);
                res = folderPath.Replace("/" + buckName, "");//返回上级目录
            }
            return res;
        }

        /// <summary>
        /// 递归获取文件夹目录下文件及文件夹
        /// </summary>
        /// <param name="pathName">需要递归遍历的文件夹信息</param>
        /// <returns>文件路径集合</returns>
        public static List<FileModel> ListFiles(FileSystemInfo info, List<FileModel> list, string fdir, int count, out int ocount)
        {
            ocount = count;
            if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                if (!Directory.Exists(info.FullName)) return list;
                DirectoryInfo dir = new DirectoryInfo(info.FullName);
                string fdir1 = fdir + "/" + dir.Name;
                //不是目录   
                //if (dir == null) return list;
                FileSystemInfo[] files = dir.GetFileSystemInfos();
                foreach (FileSystemInfo fileitem in files)
                {
                    FileModel f = new FileModel();
                    if ((fileitem.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        f.filepath = fileitem.FullName;
                        f.filesize = FormatCapacity(FileSize(fileitem.FullName));
                        f.filetype = "文件";
                        f.fileprogress = 0;
                        f.fileindex = ocount;
                        f.filefrontdir = fdir1;
                        list.Add(f);
                        ocount = ocount + 1;
                    }
                    else
                    {
                        ListFiles(fileitem, list, fdir1, ocount, out ocount);
                    }
                }
            }
            return list;
        }

        public static Int32 prencent(long x, long y)
        {
            Int32 result = 0;
            if (y == 0)
            {
                result = 0;
            }
            else
            {
                result = Convert.ToInt32(((double)x / y) * 100);
            }

            return result;
        }
        #endregion

        #region 文件列表操作函数
        public static bool BacthDel(CosCloud cos, string bucketName, string path, out string mes)
        {
            mes = "";
            bool b = true;
            string result = cos.GetFolderList(bucketName, path, 199, "", 0, FolderPattern.Both);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            FolderModel bfm = jss.Deserialize<FolderModel>(result);
            if (bfm.code != 0)
            {
                mes = bfm.message;
                return false;
            }
            if (bfm.data.infos.Count == 0)
            {
                b = DelFolder(cos, bucketName, path, out mes);
            }
            else
            {
                foreach (var item in bfm.data.infos)
                {
                    if (!string.IsNullOrEmpty(item.sha))
                    {
                        string path1 = path.TrimEnd('/') + "/" + item.name;
                        b = DelFiles(cos, bucketName, path1, out mes);
                    }
                    else
                    {
                        string path2 = path.TrimEnd('/') + "/" + item.name;
                        BacthDel(cos, bucketName, path2, out mes);
                        //删除本身
                        b = DelFolder(cos, bucketName, path2, out mes);
                    }
                }
                b = DelFolder(cos, bucketName, path, out mes);
            }
            return b;
        }

        public static bool DelFolder(CosCloud cos, string bucketName, string path, out string outmes)
        {
            string deljsonstr = "";
            bool flag = true;
            try
            {
                deljsonstr = cos.DeleteFolder(bucketName, path);
                JavaScriptSerializer jss1 = new JavaScriptSerializer();
                CosBase deljson = jss1.Deserialize<CosBase>(deljsonstr);
                if (deljson.code == 0 && deljson.message == "SUCCESS")
                {
                    outmes = "删除完成!";
                }
                else
                {
                    outmes = "删除出错!" + deljson.message;
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                outmes = "删除出错!" + ex.Message;
                flag = false;
            }
            return flag;
        }

        public static bool DelFiles(CosCloud cos, string bucketName, string path, out string outmes)
        {
            string deljsonstr = "";
            bool flag = true;
            deljsonstr = cos.DeleteFile(bucketName, path);
            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            CosBase deljson = jss2.Deserialize<CosBase>(deljsonstr);
            if (deljson.code == 0 && deljson.message == "SUCCESS")
            {
                outmes = "删除完成!";
            }
            else
            {
                outmes = "删除出错!" + deljson.message;
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 判断文件是否为完整：完整删除,不完整跳过
        /// </summary>
        /// <param name="cos"></param>
        /// <param name="bucketName"></param>
        /// <param name="lf"></param>
        public static void DelFiles(CosCloud cos, string bucketName, List<FileModel> lf, string curfolder)
        {
            foreach (var item in lf)
            {
                if (item.filefrontdir != "")
                {
                    curfolder = curfolder + item.filefrontdir;
                }
                string tempres = cos.GetFileStat(bucketName, curfolder + "/" + Path.GetFileName(item.filepath));
                JObject tempobj = (JObject)JsonConvert.DeserializeObject(tempres);
                var tempcode = (int)tempobj["code"];
                if (tempcode == 0)
                {
                    if (tempobj["data"]["filelen"].ToString() == tempobj["data"]["filesize"].ToString())
                    {
                        cos.DeleteFile(bucketName, curfolder.TrimEnd('/') + "/" + Path.GetFileName(item.filepath));
                    }
                }
            }
        }
        #endregion

    }
}
