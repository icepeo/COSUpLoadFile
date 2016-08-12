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

namespace COSUpLoadFile
{
    public partial class frmMain : CCSkinMain
    {
        #region 定义变量与常量区
        private object _thread=null;
        private int pagesize = 199;
        #endregion

        #region 初始化函数
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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
            
            //END
        }
        #endregion

        private void btnUpFileTitle_Click(object sender, EventArgs e)
        {

        }

        #region 绑定列表函数与事件
        private void Bind_lvbucketlist()
        {
            //先判断是否为多目录
            string[] bucketNames;
            string bucketName = GlobelSet.BucketName;
            if (bucketName.IndexOf(',') > 0)
            {
                bucketNames = bucketName.Split(',');
                BuckettableModel.Rows.Clear();
                BuckettableModel.RowHeight = 30;
                //lvBucketList.HideSelection = false;
                for (var bucketName_i = 0; bucketName_i < bucketNames.Length; bucketName_i++)
                {
                    BuckettableModel.Rows.Add(new Row());
                    BuckettableModel.Rows[bucketName_i].Cells.Add(new Cell(bucketNames[bucketName_i]));
                    ListViewItem lvitembucket = new ListViewItem();
                    lvitembucket.SubItems[0].Text = bucketNames[bucketName_i];
                    if (bucketName_i == 0)
                    {
                        lvitembucket.Selected = true;
                    }
                    lvitembucket.StateImageIndex = 0;
                    lvitembucket.SubItems[0].ForeColor = Color.Black;
                    //lvitembucket.SubItems[0].BackColor = Color.Black;
                    lvitembucket.Tag = bucketName_i;
                    //lvBucketList.Items.Add(lvitembucket);
                }
                //lvBucketList.EndUpdate();
            }//END
            else
            {
                //lvBucketList.Clear();
                //lvBucketList.BeginUpdate();
                ListViewItem lvitembucket = new ListViewItem();
                //lvitembucket.SubItems[0].Text = bucketName;// CurbucketName;
                //lvitembucket.StateImageIndex = 0;
                //lvitembucket.SubItems[0].BackColor = Color.White;
                //itembucket.SubItems[0].;

                //lvitembucket.Tag = "0";
                //lvBucketList.Items.Add(lvitembucket);
                //lvBucketList.EndUpdate();
            }
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
            return "";
            //if (lvBucketList.InvokeRequired)
            //{
            //    return lvBucketList.Invoke(new InvokeGetbucketName(ReadCurbucketName)).ToString();
            //}
            //else
            //{
            //    var obj = lvBucketList.SelectedItems[0];
            //    if (obj.Text != "" || obj == null)
            //    {
            //        return lvBucketList.SelectedItems[0].Text;
            //    }
            //    else
            //    {
            //        return "mybucketName";
            //    }
            //}
        }
        #endregion


    }
}
