using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
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
        private volatile List<FileModel> _filelist = null;
        private static ILog log = LogManager.GetLogger("Logs");
        private static int _count;  //进程完成量;
        private static string tabname = "上传队列";
        #endregion

        #region 初始化函数
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            log.Info("初始化程序内容");
            skinTabPage2.Text = tabname;
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
                    uploadtableModel.Rows[vi].Tag = v.filefrontdir;
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filepath));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filesize));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell("Progress" + vi, v.fileprogress));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell("操作"+vi));
                    uploadtableModel.Rows[vi].Editable = false;
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
            int sliceSize =9216;
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
                if (_files.filesizes > sliceSize) //文件大小大于sliceSize,使用SliceUploadFile分片上传,其余为UploadFile普通上传
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
                        }
                    }
                    results = result;
                }
                else
                {
                    results = cos.UploadFile(CurbucketName, curfolder + "/" + Path.GetFileName(_files.filepath), _files.filepath);
                    UpLoadFileMulti_DisplayProgress(threadindex, index, indexs, 100);
                }
            }
            catch (Exception ex)
            {
                var ex1 = ex.Message;
                return;
            }
        }

        /// <summary>
        /// 完成(结束)
        /// </summary>
        private void UpLoadFileMulti_Complated()
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                //skinTabPage2.Text=tabname+
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
                try
                {
                    this.uploadtableModel.Rows[listindex].Cells[2].Data = progress;
                    lock (this)
                    {
                        Fileslist[listindex].fileprogress = progress;
                        Serializes.MySerialize<List<FileModel>>(Fileslist, GlobelSet.stringpath);
                    }
                }
                catch(Exception e)
                {
                    log.Info("出错原因：",e);
                    return;
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
                    BuckettableModel.Rows[bucketName_i].Editable = false;
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
                BuckettableModel.Rows[0].Editable = false;
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

        #region 右击菜单响应事件
        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                _thread.Stop();
            }
            else
            {
                NeedupFilelist = null;
            }
            if (NeedupFilelist == null)
            {
                NeedupFilelist = new List<FileModel>();
                foreach (Row item in uploadtable.SelectedItems)
                {
                    NeedupFilelist.Add(new FileModel()
                    {
                        filepath = item.Cells[0].Text,
                        fileprogress = Convert.ToInt32(item.Cells[2].Data),
                        filesize = item.Cells[1].Text,
                        filesizes = Convert.ToInt64(item.Cells[1].Tag),
                        filetype = "文件",
                        fileindex = item.Index,
                        filefrontdir = item.Tag.ToString()
                    });
                }
            }
            else
            {
                foreach (Row item in uploadtable.SelectedItems)
                {
                    for (var f = 0; f < NeedupFilelist.Count; f++)
                    {
                        if (item.Index != NeedupFilelist[f].fileindex)
                        {
                            NeedupFilelist.Add(new FileModel()
                            {
                                filepath = item.Cells[0].Text,
                                fileprogress = Convert.ToInt32(item.Cells[2].Data),
                                filesize = item.Cells[1].Text,
                                filesizes = Convert.ToInt64(item.Cells[1].Tag),
                                filetype = "文件",
                                fileindex = item.Index,
                                filefrontdir = item.Tag.ToString()
                            });
                        }
                    }
                }
            }
            //先删除存在的文件
            CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
            Function.DelFiles(cos, CurbucketName, NeedupFilelist, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName));
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpLoadFileMulti_Start), new object[] { NeedupFilelist });
        }

        private void clearcomplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_thread != null)
            {
                return;
            }
            List<FileModel> rfilelist = Fileslist.FindAll(t => t.fileprogress != 100);
            Serializes.MySerialize<List<FileModel>>(rfilelist, GlobelSet.stringpath);
            BindXPTable(rfilelist);            
        }

        private void clearallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearallupfile();
        }

        private void clearallupfile()
        {
            
            if (_thread != null)
            {
                _thread.Stop();
            }
            NeedupFilelist = null;
            Fileslist = null;
            try
            {
                File.Delete(GlobelSet.stringpath);
            }
            catch
            {
                return;
            }
            uploadtableModel.Rows.Clear();
        }

        private void BindXPTable(List<FileModel> l)
        {
            uploadtableModel.Rows.Clear();
            //XPTable表头
            //uploadtable.ColumnModel = uploadcolumnModel;
            //uploadtable.TableModel = uploadtableModel;

            //uploadcolumnModel.Columns.Add(new TextColumn("路径"));
            //uploadcolumnModel.Columns[0].Width = 460;
            //uploadcolumnModel.Columns.Add(new TextColumn("大小"));
            //uploadcolumnModel.Columns[1].Width = 100;
            //uploadcolumnModel.Columns.Add(new ProgressBarColumn("进度"));
            //uploadcolumnModel.Columns[2].Width = 150;
            //uploadcolumnModel.Columns.Add(new ButtonColumn("操作"));
            //columnModel1.Columns[3].Width = 100;
            if (l != null && l.Count > 0)
            {
                int vi = 0;
                foreach (FileModel v in l)
                {
                    //XPTable
                    // add some Rows and Cells to the TableModel
                    uploadtableModel.Rows.Add(new Row());
                    uploadtableModel.Rows[vi].Tag = v.filefrontdir;
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filepath));
                    uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filesize));
                    uploadtableModel.Rows[vi].Cells[1].Tag = v.filesizes;
                    Cell pCell = new Cell("Progress" + vi);
                    pCell.Data = v.fileprogress;
                    uploadtableModel.Rows[vi].Cells.Add(pCell);
                    //uploadtableModel.Rows[vi].Cells.Add(new Cell("续传"));
                    uploadtableModel.Rows[vi].Editable = false;
                    //END
                    vi++;
                }
            }
            else
            {
                uploadtableModel.Rows.Clear();
            }
        }
        

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FolderList.SelectedItems.Count() <= 0)
            {
                downToolStripMenuItem.Enabled = false;
                return;
            }
            if (FolderList.SelectedItems[0].Tag.ToString() == "1")
            {
                CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                string result = cos.GetFileStat(CurbucketName, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName) + FolderList.SelectedItems[0].Cells[0].Text);
                JObject obj = (JObject)JsonConvert.DeserializeObject(result);
                if ((int)obj["code"] != 0)
                {
                    MessageBox.Show("读取文件信息出错：" + obj["message"]);
                    return;
                }
                string URL = HttpUtility.UrlDecode(obj["data"]["access_url"].ToString());
                WebClient client = new WebClient();
                string fileName = HttpUtility.UrlDecode(URL.Substring(URL.LastIndexOf("/") + 1));  //被下载的文件名
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                DialogResult dr = fbd.ShowDialog();
                if (dr != DialogResult.OK)
                {
                    return;
                }
                string f = @fbd.SelectedPath + "\\";
                string Path = f + fileName;   //另存为的绝对路径＋文件名

                try
                {
                    WebRequest myre = WebRequest.Create(URL);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

                try
                {
                    client.DownloadFile(HttpUtility.UrlDecode(URL), fileName);
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader r = new BinaryReader(fs);
                    byte[] mbyte = r.ReadBytes((int)fs.Length);

                    FileStream fstr = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);

                    fstr.Write(mbyte, 0, (int)fs.Length);
                    fstr.Close();
                    MessageBox.Show("下载完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }
            }
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FolderList.SelectedItems.Count() <= 0)
            {
                MessageBox.Show("请选中,再操作!");
                return;
            }
            else
            {
                CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                string deljsonstr = "";
                //文件夹删除操作
                if (FolderList.SelectedItems[0].Tag.ToString() == "0")
                {
                    string m = "";
                    bool res = true;
                    string remotePath = Function.FolderPath(Lblcurpathvalue.Text + FolderList.SelectedItems[0].Cells[0].Text + "/", CurbucketName);
                    string result = cos.GetFolderList(CurbucketName, remotePath, 199, "", 0, FolderPattern.Both);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    FolderModel bfm = jss.Deserialize<FolderModel>(result);
                    if (bfm.data.infos.Count > 0)
                    {
                        DialogResult dresult = MessageBox.Show("存在子目录或文件,是否删除!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dresult == DialogResult.Yes)
                        {
                            res = Function.BacthDel(cos, CurbucketName, remotePath, out m);
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        res = Function.BacthDel(cos, CurbucketName, remotePath, out m);
                    }
                    if (res)
                    {
                        MessageBox.Show(m);
                        Function.ReadMainFolder(Function.FolderPath(Lblcurpathvalue.Text, CurbucketName), FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
                        FolderListtableModel.Rows[0].Cells[0].Checked=true;
                        return;
                    }
                    //try
                    //{
                    //    deljsonstr = cos.DeleteFolder(CurbucketName, Function.FolderPath(lblfolderpath.Text + lvdata.SelectedItems[0].Text + "/", CurbucketName));
                    //    JavaScriptSerializer jss = new JavaScriptSerializer();
                    //    CosBase deljson = jss.Deserialize<CosBase>(deljsonstr);
                    //    if (deljson.code == 0 && deljson.message == "SUCCESS")
                    //    {
                    //        MessageBox.Show("删除完成!");
                    //        BindListView(Function.FolderPath(lblfolderpath.Text, CurbucketName));
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("删除出错!" + deljson.message);
                    //        return;
                    //    }
                    //}
                    //catch(Exception ex)
                    //{
                    //    MessageBox.Show("删除出错!"+ex.Message);
                    //    return;
                    //}
                }
                //文件删除操作
                if (FolderList.SelectedItems[0].Tag.ToString() == "1")
                {
                    try
                    {
                        deljsonstr = cos.DeleteFile(CurbucketName, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName) + FolderList.SelectedItems[0].Cells[0].Text);
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        CosBase deljson = jss.Deserialize<CosBase>(deljsonstr);
                        if (deljson.code == 0 && deljson.message == "SUCCESS")
                        {
                            MessageBox.Show("删除完成!");
                            Function.ReadMainFolder(Function.FolderPath(Lblcurpathvalue.Text, CurbucketName), FolderListtableModel, FolderShowImg, CurbucketName, Lblcurpathvalue);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("删除出错!" + deljson.message);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除出错!" + ex.Message);
                        return;
                    }
                }
                //txtbox.Text = lvdata.SelectedItems[0].Text;
            }
        }

        

        private void BtnWFNtitle_Click(object sender, EventArgs e)
        {
            if (_thread == null)
            {
                if (Fileslist != null)
                {
                    //先删除存在的文件
                    //CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
                    //Function.DelFiles(cos, CurbucketName, Fileslist, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(UpLoadFileMulti_Start), new object[] { Fileslist });
                }
            }
            else
            {
                MessageBox.Show("正在传送！");
                return;
            }
        }        

        private void BtnClearUpFilesTitle_Click(object sender, EventArgs e)
        {
            clearallToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 拖入数据上传文件
        private void FolderList_DragDrop(object sender, DragEventArgs e)
        {
            if (_thread != null)
            {
                _thread.Stop();
            }
            else
            {
                NeedupFilelist = null;
            }
            List<FileModel> filescollections = new List<FileModel>();
            int fi = 0;
            string[] File = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Fileslist != null)
            {
                //清除上传完成的
                int itemi = 0;
                foreach (var item1 in Fileslist)
                {
                    if (item1.fileprogress < 100)
                    {
                        filescollections.Add(new FileModel()
                        {
                            filefrontdir = item1.filefrontdir,
                            fileindex = itemi,
                            filepath = item1.filepath,
                            fileprogress = item1.fileprogress,
                            filesize = item1.filesize,
                            filesizes = item1.filesizes,
                            filetype = item1.filetype
                        });
                        itemi = itemi + 1;
                    }
                }
                //filescollections = fileslist;
                fi = filescollections.Count;
            }
            else
            {
                filescollections = new List<FileModel>();
            }
            foreach (var item in File)
            {
                FileModel files = new FileModel();
                FileSystemInfo fsi = new FileInfo(Path.GetFullPath(item));
                if ((fsi.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    files.filepath = Path.GetFullPath(item);
                    files.filesize = Function.FormatCapacity(Function.FileSize(Path.GetFullPath(item)));
                    files.filesizes = Function.FileSize(Path.GetFullPath(item));
                    files.filetype = "文件";
                    files.fileprogress = 0;  //0-100;
                    files.fileindex = fi;
                    files.filefrontdir = "";
                    if (filescollections.FindAll(o => o.filepath == files.filepath).Count <= 0)
                    {
                        filescollections.Add(files);
                        fi = fi + 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    filescollections = Function.ListFiles(fsi, filescollections, "", fi, out fi);
                }
            }
            //filescollections = filescollections.OrderByDescending(o => o.fileindex).ToList();
            //序列化到文件
            Fileslist = filescollections;
            Serializes.MySerialize<List<FileModel>>(filescollections, GlobelSet.stringpath);

            //从文件读取记录
            List<FileModel> _fileslist = Serializes.MyDeSerialize<List<FileModel>>(GlobelSet.stringpath);
            NeedupFilelist = _filelist;
            int vi = 0;
            uploadtableModel.Rows.Clear();
            foreach (FileModel v in _fileslist)
            {
                //XPTable
                // add some Rows and Cells to the TableModel
                uploadtableModel.Rows.Add(new Row());
                uploadtableModel.Rows[vi].Tag = v.filefrontdir;
                uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filepath));
                uploadtableModel.Rows[vi].Cells.Add(new Cell(v.filesize));
                uploadtableModel.Rows[vi].Cells[1].Tag = v.filesizes;
                uploadtableModel.Rows[vi].Cells.Add(new Cell("Progress" + vi, v.fileprogress));
                //uploadtableModel.Rows[vi].Cells.Add(new Cell("操作"+vi));
                uploadtableModel.Rows[vi].Editable = false;
                //END
                vi++;
            }
            //先删除存在的文件
            CosCloud cos = new CosCloud(GlobelSet.APP_ID, GlobelSet.SECRET_ID, GlobelSet.SECRET_KEY);
            Function.DelFiles(cos, CurbucketName, _fileslist, Function.FolderPath(Lblcurpathvalue.Text, CurbucketName));
            //skinTabPage2.Text = skinTabPage2.Text + "(0/" + _filelist.Count + ")";
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpLoadFileMulti_Start), new object[] { _fileslist });
        }

        private void FolderList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void uploadtable_DragDrop(object sender, DragEventArgs e)
        {
            FolderList_DragDrop(sender, e);
        }

        private void uploadtable_DragEnter(object sender, DragEventArgs e)
        {
            FolderList_DragEnter(sender, e);
        }
        #endregion
    }
}
