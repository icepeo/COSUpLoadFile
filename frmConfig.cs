using System;
using System.Windows.Forms;
using System.IO;

using CCWin;
using COSUpLoadFile.Common;

namespace COSUpLoadFile
{
    public partial class frmConfig : CCSkinMain
    {
        private string currpath = System.AppDomain.CurrentDomain.BaseDirectory + @"config.xml";
        private string appid = "10046129";
        private string secretid = "AKIDtN47V5re4q5buarSlOVdVGKBYe98dUOT";
        private string secretkey = "xx3jw44FfQ1HtzmAdnzXfxEH05F3qgGO";
        private string bucketName = "mytest";
        private string thread = "1";
        public frmConfig()
        {
            InitializeComponent();
        }

        private void BtnCloseConfig_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnSaveConfig_Click(object sender, EventArgs e)
        {
            XmlHelper xmlhelper = new XmlHelper(currpath);
            if (!File.Exists(currpath))
            {
                xmlhelper.CreateXmlDocument(currpath, "config", "1.0", "utf-8", null);
                xmlhelper.AddNode(currpath, "config", "appid", txtAppIDValue.Text);
                xmlhelper.AddNode(currpath, "config", "secretid",txtSecretIDValue.Text);
                xmlhelper.AddNode(currpath, "config", "secretkey",txtSecretKeyValue.Text);
                xmlhelper.AddNode(currpath, "config", "bucketName",txtBucketNameValue.Text);
                xmlhelper.AddNode(currpath, "config", "thread", txtThreadValue.Text);
            }
            else
            {
                //修改
                xmlhelper.UpdateNode(currpath, "config", "appid", txtAppIDValue.Text);
                xmlhelper.UpdateNode(currpath, "config", "secretkey", txtSecretIDValue.Text);
                xmlhelper.UpdateNode(currpath, "config", "secretkey", txtSecretKeyValue.Text);
                xmlhelper.UpdateNode(currpath, "config", "bucketName", txtBucketNameValue.Text);
                xmlhelper.UpdateNode(currpath, "config", "thread", txtThreadValue.Text);
            }
            MessageBox.Show("配置完成");
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            if (!File.Exists(currpath))
            {
                return;
            }
            else
            {
                XmlHelper xmlhelper = new XmlHelper(currpath);
                txtAppIDValue.Text = (xmlhelper.GetXmlNode(currpath, "config/appid") == null ? appid : xmlhelper.GetXmlNode(currpath, "config/appid").InnerText);
                txtSecretIDValue.Text = (xmlhelper.GetXmlNode(currpath, "config/secretid") == null ? secretid : xmlhelper.GetXmlNode(currpath, "config/secretid").InnerText);
                txtSecretKeyValue.Text = (xmlhelper.GetXmlNode(currpath, "config/secretkey") == null ? secretkey : xmlhelper.GetXmlNode(currpath, "config/secretkey").InnerText);
                txtBucketNameValue.Text = (xmlhelper.GetXmlNode(currpath, "config/bucketName") == null ? bucketName : xmlhelper.GetXmlNode(currpath, "config/bucketName").InnerText);
                txtThreadValue.Text = (xmlhelper.GetXmlNode(currpath, "config/thread") == null ? thread : xmlhelper.GetXmlNode(currpath, "config/thread").InnerText);
            }
        }
    }
}
