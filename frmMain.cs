using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CCWin;
using COSUpLoadFile.CosApi.API;
using COSUpLoadFile.Common;
using XPTable;
using XPTable.Models;
using log4net;
using COSUpLoadFile.Threading;
using COSUpLoadFile.Models;

namespace COSUpLoadFile
{
    public partial class frmMain : CCSkinMain
    {
        #region 定义变量与常量区
        private ThreadMulti _thread = null;
        private int pagesize = 199;
        private List<FileModel> _needupfilelist=null;
        private List<FileModel> _filelist = null;
        private static ILog log = LogManager.GetLogger("Logs");
        private static int _count;  //进程完成量;
        #endregion

        #region 初始化函数
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            log.Info("初始化程序内容");
            initFolderList();
            Bind_lvbucketlist();
            Bind_uploadlist();
            Function.ReadMainFolder("/", FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
        }

        /// <summary>
        /// 初始化目录列表
        /// </summary>
        private void initFolderList()
        {
            FolderList.ColumnModel = FolderListcolumnModel;
            FolderList.TableModel = FolderListtableModel;
            //定义列表头
            FolderListcolumnModel.Columns.Add(new ImageColumn("文件名"));
            FolderListcolumnModel.Columns.Add(new TextColumn("大小"));
            FolderListcolumnModel.Columns.Add(new TextColumn("自定义权限"));
            FolderListcolumnModel.Columns.Add(new TextColumn("创建时间"));
            FolderListcolumnModel.Columns[0].Width = 240;
            FolderListcolumnModel.Columns[1].Width = 120;
            FolderListcolumnModel.Columns[2].Width = 100;
            FolderListcolumnModel.Columns[3].Width = 180;            
            FolderListtableModel.RowHeight = 24;
            //END            
        }

        /// <summary>
        /// 初始化上传队列表
        /// </summary>
        private void Bind_uploadlist()
        {
            //uploadtable.ColumnModel = uploadcolumnModel;
            //uploadtable.TableModel = uploadtableModel;
            uploadcolumnModel.Columns.Add(new TextColumn("路径"));
            uploadcolumnModel.Columns[0].Width = 460;
            uploadcolumnModel.Columns.Add(new TextColumn("大小"));
            uploadcolumnModel.Columns[1].Width = 100;
            uploadcolumnModel.Columns.Add(new ProgressBarColumn("进度"));
            uploadcolumnModel.Columns[2].Width = 150;
            if (Fileslist != null)
            {
                int vi = 0;
                //fileslist = fileslist.OrderByDescending(o=>o.fileindex).ToList();
                foreach (FileModel fm in Fileslist)
                {
                    //XPTable
                    // add some Rows and Cells to the TableModel
                    uploadtableModel.Rows.Add(new Row());
                    uploadtableModel.Rows[vi].Tag = fm.filefrontdir;
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(fm.filepath));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(fm.filesize));
                    uploadtableModel.Rows[vi].Cells[1].Tag = fm.filesizes;
                    Cell pCell = new Cell("Progress" + vi);
                    pCell.Data = fm.fileprogress;
                    uploadtableModel.Rows[vi].Cells.Add(pCell);
                    //uploadtableModel.Rows[vi].Cells.Add(new Cell("续传"));
                    uploadtableModel.Rows[vi].Editable = false;
                    //END
                    vi++;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region 退出
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosingEventArgs e1 = new FormClosingEventArgs(CloseReason.UserClosing, false);
            frmMain_FormClosing(sender, e1);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dresult = MessageBox.Show("是否确定退出!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dresult == DialogResult.Yes)
            {
                //Application.Exit();
                if (_thread != null)
                {
                    //_thread.Stop();
                }
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 读取主目录
        private void btnReadPathTitle_Click(object sender, EventArgs e)
        {
            Function.ReadMainFolder("/", FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
        }
        #endregion

        #region 上传文件
        private void btnUpFileTitle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (_thread != null)
                {
                    _thread.Stop();
                }
                else
                {
                    NeedupFilelist = null;
                }
                //fileslist = null;
                List<FileModel> filescollections;
                int fi = 0;
                if (Fileslist != null)
                {
                    filescollections = Fileslist;
                    fi = Fileslist.Count;
                }
                else
                {
                    filescollections = new List<FileModel>();
                }
                FileModel files = new FileModel();
                FileSystemInfo fsi = new FileInfo(Path.GetFullPath(@ofd.FileName));
                if ((fsi.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    files.filepath = Path.GetFullPath(@ofd.FileName);
                    files.filesize = Function.FormatCapacity(Function.FileSize(Path.GetFullPath(@ofd.FileName)));
                    files.filesizes = Function.FileSize(Path.GetFullPath(@ofd.FileName));
                    files.filetype = "文件";
                    files.fileprogress = 0;  //0-100;
                    files.fileindex = fi;
                    files.filefrontdir = "";
                    if (filescollections.FindAll(o => o.filepath == files.filepath).Count <= 0)
                    {
                        filescollections.Add(files);
                        fi = fi + 1;
                    }
                }
                else
                {
                    filescollections = Function.ListFiles(fsi, filescollections, "", fi, out fi);
                }

                //filescollections = filescollections.OrderByDescending(o => o.fileindex).ToList();
                //序列化到文件
                Serializes.MySerialize<List<FileModel>>(filescollections, GlobelSet.stringpath);

                //从文件读取记录
                List<FileModel> _fileslist = Serializes.MyDeSerialize<List<FileModel>>(GlobelSet.stringpath);

                int vi = 0;
                foreach (FileModel v in _fileslist)
                {
                    //XPTable
                    // add some Rows and Cells to the TableModel
                    uploadtableModel.Rows.Add(new Row());
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filepath));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filesize));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell("Progress" + vi, v.fileprogress));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell("操作"+vi));
                    //END
                    vi++;
                }
                //先删除存在的文件
                CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                Function.DelFiles(cos, CurbucketName, _fileslist, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName));
                ThreadPool.QueueUserWorkItem(new WaitCallback(UpLoadFileMulti_Start), new object[] { _fileslist });
            }
            else
            {
                return;
            }
            //txtbox.Text = "";
        }
        #endregion        

        #region 队列上传
        /// <summary>
        /// 启动
        /// </summary>
        private void UpLoadFileMulti_Start(object arg)
        {
            int workcount = 0;
            List<FileModel> fileslist = ((object[])arg)[0] as List<FileModel>;
            workcount = fileslist.Count;

            _count = workcount * 100;
            _thread = new ThreadMulti(workcount, GlobelSet.ThreadNum);
            NeedupFilelist = fileslist;
            _thread.WorkMethod = new ThreadMulti.DelegateWork(UpLoadFileMulti_DoWork);
            _thread.CompleteEvent = new ThreadMulti.DelegateComplete(UpLoadFileMulti_Complated);
            _thread.Start();
        }

        /// <summary>
        /// 执行
        /// </summary>
        private void UpLoadFileMulti_DoWork(int index, int threadindex)
        {
            //要上传的文件队列
            List<FileModel> _fileslist = NeedupFilelist;
            FileModel _files = _fileslist[index - 1];
            string results = "";
            int sliceSize = 524288;
            int indexs = _files.fileindex;
            CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
            try
            {
                //获取当前所在目录
                string curfolder = Function.FolderPath(Lblcurpathvalue.Text, CurbucketName); //ulf.currentfolder;
                if (_files.filefrontdir != "")
                {
                    curfolder = curfolder + _files.filefrontdir;
                }
                if (_files.filesizes >= 1024 * 1024 * 0) //文件大小大于5M,使用SliceUploadFile分片上传,其余为UploadFile普通上传
                {
                    string result = cos.SliceUploadFileFirstStep(CurbucketName, curfolder + "/" + Path.GetFileName(_files.filepath), _files.filepath, sliceSize);
                    JObject obj = (JObject)JsonConvert.DeserializeObject(result);
                    var code = (int)obj["code"];
                    if (code != 0)
                    {
                        //MessageBox.Show("上传失败:" + obj["message"]);
                        return;
                    }
                    JToken data = obj["data"];
                    if (data["access_url"] != null)
                    {
                        var accessUrl = data["access_url"];
                        UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, 100);
                        //MessageBox.Show("上传完成:" + accessUrl);
                        return;
                    }
                    else
                    {
                        string sessionId = data["session"].ToString();
                        sliceSize = (int)data["slice_size"];
                        long offset = (long)data["offset"];
                        var retryCount = 0;
                        var progress = Function.prencent(offset, _files.filesizes);
                        UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, progress);
                        //bw.ReportProgress(Convert.ToInt32(offset));//progressBar1.Value = Convert.ToInt32(offset);
                        while (true)
                        {
                            result = cos.SliceUploadFileFollowStep(CurbucketName, curfolder + "/" + Path.GetFileName(_files.filepath), _files.filepath, sessionId, offset, sliceSize);
                            obj = (JObject)JsonConvert.DeserializeObject(result);
                            code = (int)obj["code"];
                            if (code != 0)
                            {
                                //当上传失败后会重试3次
                                if (retryCount < 3)
                                {
                                    retryCount++;
                                    //Console.WriteLine("重试....");
                                }
                                else
                                {
                                    //MessageBox.Show("上传出错:" + obj["message"]);
                                    return;
                                }
                            }
                            else
                            {
                                data = obj["data"];
                                if (data["offset"] != null)
                                {
                                    offset = (long)data["offset"] + sliceSize;
                                    //progressBar1.Value = Convert.ToInt32(offset);
                                }
                                else
                                {
                                    UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, 100);
                                    break;
                                }
                            }
                            progress = Function.prencent(offset, _files.filesizes);
                            UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, progress);
                            //bw.ReportProgress(Convert.ToInt32(offset));// progressBar1.Value = Convert.ToInt32(offset);
                        }
                    }
                    // return;
                    results = result;
                }
                else
                {
                    results = cos.UploadFile(CurbucketName, curfolder + "/" + Path.GetFileName(_files.filepath), _files.filepath);
                    UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, 100);
                }
                //e.Result = results;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("上传文件失败:" + ex.Message);
                var ex1 = ex.Message;
                return;
            }

            //for (int i = 0; i < 100; i++)
            //{
            //    System.Threading.Thread.Sleep(threadindex * 5);
            //    UpLoadFileMulti_DisplayProgress(threadindex, index, i);
            //}
        }

        /// <summary>
        /// 完成(结束)
        /// </summary>
        private void UpLoadFileMulti_Complated()
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                Function.ReadMainFolder(Function.FolderPath(Lblcurpathvalue.Text, CurbucketName).TrimEnd('/') + "/", FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);                
            }));
            _thread = null;
            NeedupFilelist = null;
        }

        /// <summary>
        /// 进度显示
        /// </summary>
        /// <param name="threadindex"></param>
        /// <param name="taskindex"></param>
        /// <param name="progress"></param>
        private void UpLoadFileMulti_DisplayProgress(int threadindex, int taskindex, int listindex, int progress)
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                this.uploadtableModel.Rows[listindex].Cells[2].Data = progress;
                lock (this)
                {
                    Fileslist[listindex].fileprogress = progress;
                    Serializes.MySerialize<List<FileModel>>(Fileslist, GlobelSet.stringpath);
                }
            }));
        }
        #endregion

        #region 绑定Bucket列表函数与事件
        private void Bind_lvbucketlist()
        {
            //先判断是否为多目录
            string[] bucketNames;
            string bucketName = GlobelSet.BucketName;
            BucketList.TableModel = BuckettableModel;
            BucketList.ColumnModel = BucketcolumnModel;
            if (bucketName.IndexOf(',') > 0)
            {
                BucketcolumnModel.Columns.Add(new TextColumn(""));
                BucketcolumnModel.Columns[0].Width = 220;
                bucketNames = bucketName.Split(',');
                BuckettableModel.Rows.Clear();
                BuckettableModel.RowHeight = 30;
                //lvBucketList.HideSelection = false;
                for (var bucketName_i = 0; bucketName_i < bucketNames.Length; bucketName_i++)
                {
                    BuckettableModel.Rows.Add(new Row());
                    BuckettableModel.Rows[bucketName_i].Cells.Add(new Cell(bucketNames[bucketName_i]));
                    BuckettableModel.Rows[bucketName_i].Tag = bucketName_i;
                    BuckettableModel.Rows[0].IsCellSelected(0);
                    if (bucketName_i % 2 == 0)
                    {
                        BuckettableModel.Rows[bucketName_i].BackColor = Color.White;
                    }
                    else
                    {
                        BuckettableModel.Rows[bucketName_i].BackColor = Color.WhiteSmoke;
                    }
                }
            }
            else
            {
                BucketcolumnModel.Columns.Add(new TextColumn(""));
                BucketcolumnModel.Columns[0].Width = 220;
                BuckettableModel.Rows.Clear();
                BuckettableModel.Rows.Add(new Row());
                BuckettableModel.Rows[0].Cells.Add(new Cell(bucketName));
                BuckettableModel.Rows[0].Tag = "0";
            }
        }

        private void BucketList_Click(object sender, EventArgs e)
        {
            Function.ReadMainFolder("/", FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
        }

        #endregion

        #region 目录列表项双击与单击响应事件
        private void FolderList_DoubleClick(object sender, EventArgs e)
        {
            if (FolderList.SelectedItems.Count() <= 0)
            {
                return;
            }
            string isfiles = FolderList.SelectedItems[0].Tag.ToString();
            if (isfiles == "-1")
            {
                Function.ReadMainFolder(Function.FolderPath(Lblcurpathvalue.Text, CurbucketName, false), FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);                
            }
            if (isfiles == "0")
            {
                string foldername = FolderList.SelectedItems[0].Cells[0].Text;
                Function.ReadMainFolder(Function.FolderPath(Lblcurpathvalue.Text + foldername + "/", CurbucketName), FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);                
            }
        }

        private void FolderList_Click(object sender, EventArgs e)
        {
            downToolStripMenuItem.Enabled = false;
            if (FolderList.SelectedItems.Count() <= 0)
            {
                downToolStripMenuItem.Enabled = false;
                return;
            }
            string isfiles = FolderList.SelectedItems[0].Tag.ToString();
            string foldername = FolderList.SelectedItems[0].Cells[0].Text;
            JObject obj = null;
            if (isfiles == "1")
            {

                CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                string result = cos.GetFileStat(CurbucketName, Function.FolderPath(Lblcurpathvalue.Text + foldername, CurbucketName));
                obj = (JObject)JsonConvert.DeserializeObject(result);
                if ((int)obj["code"] == 0)
                {
                    if ((int)obj["data"]["filelen"] != 0)
                    {
                        downToolStripMenuItem.Enabled = true;
                    }

                    filesskinProgressBar.Minimum = 0;
                    filesskinProgressBar.Maximum = (int)obj["data"]["filesize"];
                    filesskinProgressBar.Value = (int)obj["data"]["filelen"];
                    //FolderList.SelectedItems[0].UseItemStyleForSubItems = false;
                    if ((int)obj["data"]["filelen"] == (int)obj["data"]["filesize"])
                    {
                        FolderList.SelectedItems[0].Cells[4].Text = "完整";
                    }
                    else
                    {
                        FolderList.SelectedItems[0].Cells[4].Text = "未传毕";
                    }
                    FolderList.SelectedItems[0].Cells[4].ForeColor = Color.YellowGreen;
                }
                else
                {
                    FolderList.SelectedItems[0].Cells[4].Text = "";
                }
            }
            else
            {
                downToolStripMenuItem.Enabled = false;
            }
            return;
        }
        #endregion

        #region 新建文件夹
        private void BtnCreatePathTitle_Click(object sender, EventArgs e)
        {
            string curfolder = Function.FolderPath(Lblcurpathvalue.Text, CurbucketName);
            string inputstr = Interaction.InputBox("请输入文件夹名称", "新建文件夹");
            if (inputstr != "")
            {
                string cf;
                string err = "";
                CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                try
                {
                    cf = cos.CreateFolder(CurbucketName, curfolder.TrimEnd('/') + "/" + inputstr);
                    err = "";
                }
                catch (Exception ex)
                {
                    cf = "";
                    err = ex.Message;
                }
                if (cf != "")
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    CreateModels crf = jss.Deserialize<CreateModels>(cf);
                    if (crf.code == 0 && crf.message == "SUCCESS")
                    {
                        MessageBox.Show("目录创建成功.");
                        Function.ReadMainFolder(curfolder.TrimEnd('/') + "/", FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("目录创建失败:" + crf.message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("目录创建失败:" + err);
                    return;
                }
            }
        }
        #endregion

        #region 调用参数配置窗体
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig f = new frmConfig();
            f.ShowDialog();
        }
        #endregion

        #region Properties
        private delegate string InvokeGetbucketName();
        public string CurbucketName
        {
            get
            {
                return ReadCurbucketName();
            }
            set
            {
                GlobelSet._curbucketName = value;
            }
        }

        private string ReadCurbucketName()
        {
            if (BucketList.InvokeRequired)
            {
                return BucketList.Invoke(new InvokeGetbucketName(ReadCurbucketName)).ToString();
            }
            else
            {
                try
                {
                    var obj = BucketList.SelectedItems[0];
                    if (obj.Cells[0].Text != "" || obj == null)
                    {
                        return obj.Cells[0].Text;
                    }
                    else
                    {
                        return "mytest";
                    }
                }
                catch
                {
                    return GlobelSet.BucketName.Split(',')[0];
                }
            }
        }

        /// <summary>
        /// 获取或设置要上传文件的队列
        /// </summary>
        public List<FileModel> NeedupFilelist
        {
            get
            {
                return _needupfilelist;
            }
            set
            {
                _needupfilelist = value;
            }
        }
                
        /// <summary>
        /// 从文件读取上传队列
        /// </summary>
        public List<FileModel> Fileslist
        {
            get
            {
                lock (this)
                {
                    if (_filelist == null)
                    {
                        try
                        {
                            _filelist = Serializes.MyDeSerialize<List<FileModel>>(GlobelSet.stringpath);
                        }
                        catch
                        {
                            _filelist = null;
                        }
                    }
                    else
                    {
                        return _filelist;
                    }
                    return _filelist;
                }
            }
            set
            {
                _filelist = value;
            }
        }
        #endregion
        
    }
}
