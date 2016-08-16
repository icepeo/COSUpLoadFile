using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CCWin;
using COSUpLoadFile.CosApi.API;
using COSUpLoadFile.Common;
using XPTable;
using XPTable.Models;
using log4net;

namespace COSUpLoadFile
{
    public partial class frmMain : CCSkinMain
    {
        #region 定义变量与常量区
        private object _thread=null;
        private int pagesize = 199;
        private static ILog log = LogManager.GetLogger("Logs");
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
            
            //FolderList.Update();
            
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
            Function.ReadMainFolder("/", FolderListtableModel,FolderShowImg, CurbucketName);
        }
        #endregion

        private void btnUpFileTitle_Click(object sender, EventArgs e)
        {

        }

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
            Function.ReadMainFolder("/", FolderListtableModel, FolderShowImg, CurbucketName);
        }

        #endregion

        #region 新建文件夹
        private void BtnCreatePathTitle_Click(object sender, EventArgs e)
        {
            //string curfolder = Function.FolderPath(lblfolderpath.Text, CurbucketName);
            //string inputstr = Interaction.InputBox("请输入文件夹名称", "新建文件夹");
            //if (inputstr != "")
            //{
            //    string cf;
            //    string err = "";
            //    CosCloud cos = new CosCloud(APP_ID, SECRET_ID, SECRET_KEY);
            //    try
            //    {
            //        cf = cos.CreateFolder(CurbucketName, curfolder.TrimEnd('/') + "/" + inputstr);
            //        err = "";
            //    }
            //    catch (Exception ex)
            //    {
            //        cf = "";
            //        err = ex.Message;
            //    }
            //    if (cf != "")
            //    {
            //        JavaScriptSerializer jss = new JavaScriptSerializer();
            //        CreateModels crf = jss.Deserialize<CreateModels>(cf);
            //        if (crf.code == 0 && crf.message == "SUCCESS")
            //        {
            //            MessageBox.Show("目录创建成功.");
            //            BindListView(curfolder.TrimEnd('/') + "/");
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("目录创建失败:" + crf.message);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("目录创建失败:" + err);
            //        return;
            //    }
            //}
        }
        #endregion

        #region 调用参数配置窗体
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig f = new frmConfig();
            f.ShowDialog();
        }
        #endregion

        #region 未定
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
        }
        #endregion
    }
}
