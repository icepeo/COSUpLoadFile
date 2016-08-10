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

namespace COSUpLoadFile
{
    public partial class frmMain : CCSkinMain
    {
        #region 定义变量与常量区
        object _thread=null;
        #endregion

        #region 初始化函数
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Bind_lvbucketlist();
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

        private void btnReadPathTitle_Click(object sender, EventArgs e)
        {
            List<FolderProperty> getdata = GetFolderData(CurbucketName, path, pagesize, "", 0, FolderPattern.Both);
            if (getdata == null)
            {
                //MessageBox.Show("获取目录出错!");
                lvdata.Items.Clear();
                return;
            }
            //36, 39, 43
            //219
            lvdata.Items.Clear();
            lvdata.BeginUpdate();
            ListViewItem lvitemsh = new ListViewItem();
            lvitemsh.SubItems[0].Text = "..";
            lvitemsh.StateImageIndex = 0;

            lvitemsh.Tag = "-1";
            lvdata.Items.Add(lvitemsh);
            foreach (FolderProperty fp in getdata)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.SubItems[0].Text = fp.name;
                //文件属性sha必定有值,文件夹无
                if (!String.IsNullOrEmpty(fp.sha))
                {
                    //lvitem.SubItems[0].Text += imageList1.Images["folder-icon.png"].ToString();
                    lvitem.StateImageIndex = 1;
                    lvitem.Tag = "1";
                    lvitem.SubItems.Add(Function.FormatCapacity(fp.filesize));
                }
                else
                {
                    lvitem.StateImageIndex = 0;
                    lvitem.Tag = "0";
                    lvitem.SubItems.Add("--");
                }

                lvitem.SubItems.Add("--");
                lvitem.SubItems.Add(Function.ConvertIntDateTime(fp.ctime).ToString());
                lvitem.SubItems.Add("");
                lvdata.Items.Add(lvitem);
            }
            lvdata.EndUpdate();
            lblfolderpath.Text = "/" + CurbucketName + path;
            //txtbox.Text = getdata[0].mtime + "=" + Function.ConvertIntDateTime(getdata[0].mtime);
        }

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
                lvBucketList.Clear();
                lvBucketList.BeginUpdate();
                lvBucketList.HideSelection = false;
                for (var bucketName_i = 0; bucketName_i < bucketNames.Length; bucketName_i++)
                {
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
                    lvBucketList.Items.Add(lvitembucket);
                }
                lvBucketList.EndUpdate();
            }//END
            else
            {
                lvBucketList.Clear();
                lvBucketList.BeginUpdate();
                ListViewItem lvitembucket = new ListViewItem();
                lvitembucket.SubItems[0].Text = bucketName;// CurbucketName;
                lvitembucket.StateImageIndex = 0;
                lvitembucket.SubItems[0].BackColor = Color.White;
                //itembucket.SubItems[0].;

                lvitembucket.Tag = "0";
                lvBucketList.Items.Add(lvitembucket);
                lvBucketList.EndUpdate();
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

        
    }
}
