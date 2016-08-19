namespace COSUpLoadFile
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.skinSplitContainer1 = new CCWin.SkinControl.SkinSplitContainer();
            this.BucketList = new XPTable.Models.Table();
            this.BucketcolumnModel = new XPTable.Models.ColumnModel();
            this.BuckettableModel = new XPTable.Models.TableModel();
            this.skinSplitContainer2 = new CCWin.SkinControl.SkinSplitContainer();
            this.FolderList = new XPTable.Models.Table();
            this.FolderListcolumnModel = new XPTable.Models.ColumnModel();
            this.FolderListskinContextMenuStrip = new CCWin.SkinControl.SkinContextMenuStrip();
            this.downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderListtableModel = new XPTable.Models.TableModel();
            this.filesskinProgressBar = new CCWin.SkinControl.SkinProgressBar();
            this.Lblcurpathvalue = new CCWin.SkinControl.SkinLabel();
            this.Lblcurpathtitle = new CCWin.SkinControl.SkinLabel();
            this.BtnClearUpFilesTitle = new CCWin.SkinControl.SkinButton();
            this.BtnCreatePathTitle = new CCWin.SkinControl.SkinButton();
            this.btnReadPathTitle = new CCWin.SkinControl.SkinButton();
            this.BtnWFNtitle = new CCWin.SkinControl.SkinButton();
            this.btnUpFileTitle = new CCWin.SkinControl.SkinButton();
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
            this.uploadtable = new XPTable.Models.Table();
            this.uploadcolumnModel = new XPTable.Models.ColumnModel();
            this.uploadlistskinContextMenuStrip = new CCWin.SkinControl.SkinContextMenuStrip();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearcomplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadtableModel = new XPTable.Models.TableModel();
            this.textColumn1 = new XPTable.Models.TextColumn();
            this.textColumn2 = new XPTable.Models.TextColumn();
            this.textColumn3 = new XPTable.Models.TextColumn();
            this.FolderShowImg = new System.Windows.Forms.ImageList(this.components);
            this.skinMenuStrip1 = new CCWin.SkinControl.SkinMenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).BeginInit();
            this.skinSplitContainer1.Panel1.SuspendLayout();
            this.skinSplitContainer1.Panel2.SuspendLayout();
            this.skinSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BucketList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer2)).BeginInit();
            this.skinSplitContainer2.Panel1.SuspendLayout();
            this.skinSplitContainer2.Panel2.SuspendLayout();
            this.skinSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FolderList)).BeginInit();
            this.FolderListskinContextMenuStrip.SuspendLayout();
            this.skinTabControl1.SuspendLayout();
            this.skinTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadtable)).BeginInit();
            this.uploadlistskinContextMenuStrip.SuspendLayout();
            this.skinMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinSplitContainer1
            // 
            this.skinSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinSplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.skinSplitContainer1.IsSplitterFixed = true;
            this.skinSplitContainer1.Location = new System.Drawing.Point(4, 53);
            this.skinSplitContainer1.Name = "skinSplitContainer1";
            // 
            // skinSplitContainer1.Panel1
            // 
            this.skinSplitContainer1.Panel1.Controls.Add(this.BucketList);
            // 
            // skinSplitContainer1.Panel2
            // 
            this.skinSplitContainer1.Panel2.Controls.Add(this.skinSplitContainer2);
            this.skinSplitContainer1.Size = new System.Drawing.Size(995, 580);
            this.skinSplitContainer1.SplitterDistance = 225;
            this.skinSplitContainer1.SplitterWidth = 1;
            this.skinSplitContainer1.TabIndex = 1;
            // 
            // BucketList
            // 
            this.BucketList.ColumnModel = this.BucketcolumnModel;
            this.BucketList.ColumnResizing = false;
            this.BucketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BucketList.EnableHeaderContextMenu = false;
            this.BucketList.FullRowSelect = true;
            this.BucketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.BucketList.Location = new System.Drawing.Point(0, 0);
            this.BucketList.Name = "BucketList";
            this.BucketList.NoItemsText = "";
            this.BucketList.Scrollable = false;
            this.BucketList.Size = new System.Drawing.Size(225, 580);
            this.BucketList.TabIndex = 1;
            this.BucketList.TableModel = this.BuckettableModel;
            this.BucketList.Click += new System.EventHandler(this.BucketList_Click);
            // 
            // BuckettableModel
            // 
            this.BuckettableModel.RowHeight = 25;
            // 
            // skinSplitContainer2
            // 
            this.skinSplitContainer2.CollapsePanel = CCWin.SkinControl.CollapsePanel.Panel2;
            this.skinSplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinSplitContainer2.IsSplitterFixed = true;
            this.skinSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.skinSplitContainer2.Name = "skinSplitContainer2";
            this.skinSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // skinSplitContainer2.Panel1
            // 
            this.skinSplitContainer2.Panel1.Controls.Add(this.FolderList);
            this.skinSplitContainer2.Panel1.Controls.Add(this.filesskinProgressBar);
            this.skinSplitContainer2.Panel1.Controls.Add(this.Lblcurpathvalue);
            this.skinSplitContainer2.Panel1.Controls.Add(this.Lblcurpathtitle);
            this.skinSplitContainer2.Panel1.Controls.Add(this.BtnClearUpFilesTitle);
            this.skinSplitContainer2.Panel1.Controls.Add(this.BtnCreatePathTitle);
            this.skinSplitContainer2.Panel1.Controls.Add(this.btnReadPathTitle);
            this.skinSplitContainer2.Panel1.Controls.Add(this.BtnWFNtitle);
            this.skinSplitContainer2.Panel1.Controls.Add(this.btnUpFileTitle);
            // 
            // skinSplitContainer2.Panel2
            // 
            this.skinSplitContainer2.Panel2.Controls.Add(this.skinTabControl1);
            this.skinSplitContainer2.Size = new System.Drawing.Size(769, 580);
            this.skinSplitContainer2.SplitterDistance = 310;
            this.skinSplitContainer2.SplitterWidth = 5;
            this.skinSplitContainer2.TabIndex = 0;
            // 
            // FolderList
            // 
            this.FolderList.AllowDrop = true;
            this.FolderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderList.ColumnModel = this.FolderListcolumnModel;
            this.FolderList.ColumnResizing = false;
            this.FolderList.ContextMenuStrip = this.FolderListskinContextMenuStrip;
            this.FolderList.EnableHeaderContextMenu = false;
            this.FolderList.FullRowSelect = true;
            this.FolderList.GridLines = XPTable.Models.GridLines.Both;
            this.FolderList.HideSelection = true;
            this.FolderList.Location = new System.Drawing.Point(0, 37);
            this.FolderList.Name = "FolderList";
            this.FolderList.NoItemsText = "暂无文件";
            this.FolderList.Size = new System.Drawing.Size(769, 272);
            this.FolderList.SortedColumnBackColor = System.Drawing.Color.Transparent;
            this.FolderList.TabIndex = 4;
            this.FolderList.TableModel = this.FolderListtableModel;
            this.FolderList.Text = "FolderList";
            this.FolderList.Click += new System.EventHandler(this.FolderList_Click);
            this.FolderList.DragDrop += new System.Windows.Forms.DragEventHandler(this.FolderList_DragDrop);
            this.FolderList.DragEnter += new System.Windows.Forms.DragEventHandler(this.FolderList_DragEnter);
            this.FolderList.DoubleClick += new System.EventHandler(this.FolderList_DoubleClick);
            // 
            // FolderListskinContextMenuStrip
            // 
            this.FolderListskinContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.FolderListskinContextMenuStrip.Back = System.Drawing.Color.White;
            this.FolderListskinContextMenuStrip.BackRadius = 4;
            this.FolderListskinContextMenuStrip.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.FolderListskinContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.FolderListskinContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.FolderListskinContextMenuStrip.HoverFore = System.Drawing.Color.White;
            this.FolderListskinContextMenuStrip.ItemAnamorphosis = true;
            this.FolderListskinContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.FolderListskinContextMenuStrip.ItemBorderShow = true;
            this.FolderListskinContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.FolderListskinContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.FolderListskinContextMenuStrip.ItemRadius = 4;
            this.FolderListskinContextMenuStrip.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.FolderListskinContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downToolStripMenuItem,
            this.delToolStripMenuItem});
            this.FolderListskinContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.FolderListskinContextMenuStrip.Name = "FolderListskinContextMenuStrip";
            this.FolderListskinContextMenuStrip.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.FolderListskinContextMenuStrip.Size = new System.Drawing.Size(125, 48);
            this.FolderListskinContextMenuStrip.SkinAllColor = true;
            this.FolderListskinContextMenuStrip.TitleAnamorphosis = true;
            this.FolderListskinContextMenuStrip.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.FolderListskinContextMenuStrip.TitleRadius = 4;
            this.FolderListskinContextMenuStrip.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // downToolStripMenuItem
            // 
            this.downToolStripMenuItem.Name = "downToolStripMenuItem";
            this.downToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.downToolStripMenuItem.Text = "下载文件";
            this.downToolStripMenuItem.Click += new System.EventHandler(this.downToolStripMenuItem_Click);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.delToolStripMenuItem.Text = "删除文件";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.delToolStripMenuItem_Click);
            // 
            // filesskinProgressBar
            // 
            this.filesskinProgressBar.Back = null;
            this.filesskinProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.filesskinProgressBar.BarBack = null;
            this.filesskinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.filesskinProgressBar.ForeColor = System.Drawing.Color.Red;
            this.filesskinProgressBar.Location = new System.Drawing.Point(632, 6);
            this.filesskinProgressBar.Name = "filesskinProgressBar";
            this.filesskinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.filesskinProgressBar.Size = new System.Drawing.Size(124, 23);
            this.filesskinProgressBar.TabIndex = 3;
            // 
            // Lblcurpathvalue
            // 
            this.Lblcurpathvalue.AutoSize = true;
            this.Lblcurpathvalue.BackColor = System.Drawing.Color.Transparent;
            this.Lblcurpathvalue.BorderColor = System.Drawing.Color.White;
            this.Lblcurpathvalue.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lblcurpathvalue.Location = new System.Drawing.Point(72, 9);
            this.Lblcurpathvalue.Name = "Lblcurpathvalue";
            this.Lblcurpathvalue.Size = new System.Drawing.Size(0, 17);
            this.Lblcurpathvalue.TabIndex = 1;
            // 
            // Lblcurpathtitle
            // 
            this.Lblcurpathtitle.AutoSize = true;
            this.Lblcurpathtitle.BackColor = System.Drawing.Color.Transparent;
            this.Lblcurpathtitle.BorderColor = System.Drawing.Color.White;
            this.Lblcurpathtitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lblcurpathtitle.Location = new System.Drawing.Point(7, 9);
            this.Lblcurpathtitle.Name = "Lblcurpathtitle";
            this.Lblcurpathtitle.Size = new System.Drawing.Size(68, 17);
            this.Lblcurpathtitle.TabIndex = 1;
            this.Lblcurpathtitle.Text = "当前目录：";
            // 
            // BtnClearUpFilesTitle
            // 
            this.BtnClearUpFilesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearUpFilesTitle.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearUpFilesTitle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnClearUpFilesTitle.DownBack = null;
            this.BtnClearUpFilesTitle.Location = new System.Drawing.Point(550, 8);
            this.BtnClearUpFilesTitle.MouseBack = null;
            this.BtnClearUpFilesTitle.Name = "BtnClearUpFilesTitle";
            this.BtnClearUpFilesTitle.NormlBack = null;
            this.BtnClearUpFilesTitle.Size = new System.Drawing.Size(75, 23);
            this.BtnClearUpFilesTitle.TabIndex = 0;
            this.BtnClearUpFilesTitle.Text = "清除上传文件";
            this.BtnClearUpFilesTitle.UseVisualStyleBackColor = false;
            this.BtnClearUpFilesTitle.Click += new System.EventHandler(this.BtnClearUpFilesTitle_Click);
            // 
            // BtnCreatePathTitle
            // 
            this.BtnCreatePathTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCreatePathTitle.BackColor = System.Drawing.Color.Transparent;
            this.BtnCreatePathTitle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnCreatePathTitle.DownBack = null;
            this.BtnCreatePathTitle.Location = new System.Drawing.Point(469, 8);
            this.BtnCreatePathTitle.MouseBack = null;
            this.BtnCreatePathTitle.Name = "BtnCreatePathTitle";
            this.BtnCreatePathTitle.NormlBack = null;
            this.BtnCreatePathTitle.Size = new System.Drawing.Size(75, 23);
            this.BtnCreatePathTitle.TabIndex = 0;
            this.BtnCreatePathTitle.Text = "新建目录";
            this.BtnCreatePathTitle.UseVisualStyleBackColor = false;
            this.BtnCreatePathTitle.Click += new System.EventHandler(this.BtnCreatePathTitle_Click);
            // 
            // btnReadPathTitle
            // 
            this.btnReadPathTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadPathTitle.BackColor = System.Drawing.Color.Transparent;
            this.btnReadPathTitle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnReadPathTitle.DownBack = null;
            this.btnReadPathTitle.Location = new System.Drawing.Point(388, 8);
            this.btnReadPathTitle.MouseBack = null;
            this.btnReadPathTitle.Name = "btnReadPathTitle";
            this.btnReadPathTitle.NormlBack = null;
            this.btnReadPathTitle.Size = new System.Drawing.Size(75, 23);
            this.btnReadPathTitle.TabIndex = 0;
            this.btnReadPathTitle.Text = "读取目录";
            this.btnReadPathTitle.UseVisualStyleBackColor = false;
            this.btnReadPathTitle.Click += new System.EventHandler(this.btnReadPathTitle_Click);
            // 
            // BtnWFNtitle
            // 
            this.BtnWFNtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnWFNtitle.BackColor = System.Drawing.Color.Transparent;
            this.BtnWFNtitle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnWFNtitle.DownBack = null;
            this.BtnWFNtitle.Location = new System.Drawing.Point(307, 8);
            this.BtnWFNtitle.MouseBack = null;
            this.BtnWFNtitle.Name = "BtnWFNtitle";
            this.BtnWFNtitle.NormlBack = null;
            this.BtnWFNtitle.Size = new System.Drawing.Size(75, 23);
            this.BtnWFNtitle.TabIndex = 0;
            this.BtnWFNtitle.Text = "全部续传";
            this.BtnWFNtitle.UseVisualStyleBackColor = false;
            this.BtnWFNtitle.Click += new System.EventHandler(this.BtnWFNtitle_Click);
            // 
            // btnUpFileTitle
            // 
            this.btnUpFileTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpFileTitle.BackColor = System.Drawing.Color.Transparent;
            this.btnUpFileTitle.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnUpFileTitle.DownBack = null;
            this.btnUpFileTitle.Location = new System.Drawing.Point(226, 8);
            this.btnUpFileTitle.MouseBack = null;
            this.btnUpFileTitle.Name = "btnUpFileTitle";
            this.btnUpFileTitle.NormlBack = null;
            this.btnUpFileTitle.Size = new System.Drawing.Size(75, 23);
            this.btnUpFileTitle.TabIndex = 0;
            this.btnUpFileTitle.Text = "上传文件";
            this.btnUpFileTitle.UseVisualStyleBackColor = false;
            this.btnUpFileTitle.Click += new System.EventHandler(this.btnUpFileTitle_Click);
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.skinTabPage2);
            this.skinTabControl1.DrawType = CCWin.SkinControl.DrawStyle.Draw;
            this.skinTabControl1.HeadBack = null;
            this.skinTabControl1.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(384, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(0, 0);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageArrowVisble = false;
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Top;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 0;
            this.skinTabControl1.Size = new System.Drawing.Size(769, 263);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 0;
            // 
            // skinTabPage2
            // 
            this.skinTabPage2.BackColor = System.Drawing.Color.White;
            this.skinTabPage2.Controls.Add(this.uploadtable);
            this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
            this.skinTabPage2.Name = "skinTabPage2";
            this.skinTabPage2.Size = new System.Drawing.Size(769, 227);
            this.skinTabPage2.TabIndex = 1;
            this.skinTabPage2.TabItemImage = null;
            this.skinTabPage2.Text = "上传队列";
            // 
            // uploadtable
            // 
            this.uploadtable.AllowDrop = true;
            this.uploadtable.ColumnModel = this.uploadcolumnModel;
            this.uploadtable.ContextMenuStrip = this.uploadlistskinContextMenuStrip;
            this.uploadtable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadtable.FullRowSelect = true;
            this.uploadtable.GridLines = XPTable.Models.GridLines.Both;
            this.uploadtable.Location = new System.Drawing.Point(0, 0);
            this.uploadtable.Name = "uploadtable";
            this.uploadtable.NoItemsText = "";
            this.uploadtable.Size = new System.Drawing.Size(769, 227);
            this.uploadtable.TabIndex = 0;
            this.uploadtable.TableModel = this.uploadtableModel;
            this.uploadtable.DragDrop += new System.Windows.Forms.DragEventHandler(this.uploadtable_DragDrop);
            this.uploadtable.DragEnter += new System.Windows.Forms.DragEventHandler(this.uploadtable_DragEnter);
            // 
            // uploadlistskinContextMenuStrip
            // 
            this.uploadlistskinContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.uploadlistskinContextMenuStrip.Back = System.Drawing.Color.White;
            this.uploadlistskinContextMenuStrip.BackRadius = 4;
            this.uploadlistskinContextMenuStrip.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.uploadlistskinContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.uploadlistskinContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.uploadlistskinContextMenuStrip.HoverFore = System.Drawing.Color.White;
            this.uploadlistskinContextMenuStrip.ItemAnamorphosis = true;
            this.uploadlistskinContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.uploadlistskinContextMenuStrip.ItemBorderShow = true;
            this.uploadlistskinContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.uploadlistskinContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.uploadlistskinContextMenuStrip.ItemRadius = 4;
            this.uploadlistskinContextMenuStrip.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.uploadlistskinContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.clearcomplateToolStripMenuItem,
            this.clearallToolStripMenuItem});
            this.uploadlistskinContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.uploadlistskinContextMenuStrip.Name = "uploadlistskinContextMenuStrip";
            this.uploadlistskinContextMenuStrip.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.uploadlistskinContextMenuStrip.Size = new System.Drawing.Size(130, 70);
            this.uploadlistskinContextMenuStrip.SkinAllColor = true;
            this.uploadlistskinContextMenuStrip.TitleAnamorphosis = true;
            this.uploadlistskinContextMenuStrip.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.uploadlistskinContextMenuStrip.TitleRadius = 4;
            this.uploadlistskinContextMenuStrip.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.uploadToolStripMenuItem.Text = "上传/续传";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // clearcomplateToolStripMenuItem
            // 
            this.clearcomplateToolStripMenuItem.Name = "clearcomplateToolStripMenuItem";
            this.clearcomplateToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.clearcomplateToolStripMenuItem.Text = "清除完成";
            this.clearcomplateToolStripMenuItem.Click += new System.EventHandler(this.clearcomplateToolStripMenuItem_Click);
            // 
            // clearallToolStripMenuItem
            // 
            this.clearallToolStripMenuItem.Name = "clearallToolStripMenuItem";
            this.clearallToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.clearallToolStripMenuItem.Text = "清除所有";
            this.clearallToolStripMenuItem.Click += new System.EventHandler(this.clearallToolStripMenuItem_Click);
            // 
            // uploadtableModel
            // 
            this.uploadtableModel.RowHeight = 20;
            // 
            // textColumn1
            // 
            this.textColumn1.Text = "文件名";
            // 
            // textColumn2
            // 
            this.textColumn2.Text = "自定义权限";
            // 
            // textColumn3
            // 
            this.textColumn3.Text = "创建时间";
            // 
            // FolderShowImg
            // 
            this.FolderShowImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FolderShowImg.ImageStream")));
            this.FolderShowImg.TransparentColor = System.Drawing.Color.Transparent;
            this.FolderShowImg.Images.SetKeyName(0, "folder.png");
            this.FolderShowImg.Images.SetKeyName(1, "document.png");
            this.FolderShowImg.Images.SetKeyName(2, "doc.png");
            this.FolderShowImg.Images.SetKeyName(3, "docx.png");
            this.FolderShowImg.Images.SetKeyName(4, "image.png");
            this.FolderShowImg.Images.SetKeyName(5, "ai.png");
            this.FolderShowImg.Images.SetKeyName(6, "apk.png");
            this.FolderShowImg.Images.SetKeyName(7, "audio.png");
            this.FolderShowImg.Images.SetKeyName(8, "cdr.png");
            this.FolderShowImg.Images.SetKeyName(9, "compress.png");
            this.FolderShowImg.Images.SetKeyName(10, "dmg.png");
            this.FolderShowImg.Images.SetKeyName(11, "execute.png");
            this.FolderShowImg.Images.SetKeyName(12, "ipa.png");
            this.FolderShowImg.Images.SetKeyName(13, "iso.png");
            this.FolderShowImg.Images.SetKeyName(14, "md.png");
            this.FolderShowImg.Images.SetKeyName(15, "other.png");
            this.FolderShowImg.Images.SetKeyName(16, "pdf.png");
            this.FolderShowImg.Images.SetKeyName(17, "ppt.png");
            this.FolderShowImg.Images.SetKeyName(18, "pptx.png");
            this.FolderShowImg.Images.SetKeyName(19, "psd.png");
            this.FolderShowImg.Images.SetKeyName(20, "video.png");
            this.FolderShowImg.Images.SetKeyName(21, "xls.png");
            this.FolderShowImg.Images.SetKeyName(22, "xlsx.png");
            // 
            // skinMenuStrip1
            // 
            this.skinMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.skinMenuStrip1.Back = System.Drawing.Color.White;
            this.skinMenuStrip1.BackRadius = 4;
            this.skinMenuStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinMenuStrip1.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinMenuStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinMenuStrip1.BaseForeAnamorphosis = false;
            this.skinMenuStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinMenuStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.BaseItemAnamorphosis = true;
            this.skinMenuStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemBorderShow = true;
            this.skinMenuStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemDown")));
            this.skinMenuStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinMenuStrip1.BaseItemMouse")));
            this.skinMenuStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.BaseItemRadius = 4;
            this.skinMenuStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinMenuStrip1.Fore = System.Drawing.Color.Black;
            this.skinMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.skinMenuStrip1.ItemAnamorphosis = true;
            this.skinMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemBorderShow = true;
            this.skinMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinMenuStrip1.ItemRadius = 4;
            this.skinMenuStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.skinMenuStrip1.Location = new System.Drawing.Point(4, 28);
            this.skinMenuStrip1.Name = "skinMenuStrip1";
            this.skinMenuStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinMenuStrip1.Size = new System.Drawing.Size(995, 25);
            this.skinMenuStrip1.SkinAllColor = true;
            this.skinMenuStrip1.TabIndex = 0;
            this.skinMenuStrip1.Text = "skinMenuStrip1";
            this.skinMenuStrip1.TitleAnamorphosis = true;
            this.skinMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinMenuStrip1.TitleRadius = 4;
            this.skinMenuStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ConfigToolStripMenuItem.Text = "参数配置";
            this.ConfigToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ExitToolStripMenuItem.Text = "退出程序";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1003, 637);
            this.Controls.Add(this.skinSplitContainer1);
            this.Controls.Add(this.skinMenuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.skinMenuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TenCentCos Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.skinSplitContainer1.Panel1.ResumeLayout(false);
            this.skinSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).EndInit();
            this.skinSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BucketList)).EndInit();
            this.skinSplitContainer2.Panel1.ResumeLayout(false);
            this.skinSplitContainer2.Panel1.PerformLayout();
            this.skinSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer2)).EndInit();
            this.skinSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FolderList)).EndInit();
            this.FolderListskinContextMenuStrip.ResumeLayout(false);
            this.skinTabControl1.ResumeLayout(false);
            this.skinTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uploadtable)).EndInit();
            this.uploadlistskinContextMenuStrip.ResumeLayout(false);
            this.skinMenuStrip1.ResumeLayout(false);
            this.skinMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinMenuStrip skinMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer1;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer2;
        private CCWin.SkinControl.SkinLabel Lblcurpathvalue;
        private CCWin.SkinControl.SkinLabel Lblcurpathtitle;
        private CCWin.SkinControl.SkinButton BtnClearUpFilesTitle;
        private CCWin.SkinControl.SkinButton BtnCreatePathTitle;
        private CCWin.SkinControl.SkinButton btnReadPathTitle;
        private CCWin.SkinControl.SkinButton BtnWFNtitle;
        private CCWin.SkinControl.SkinButton btnUpFileTitle;
        private CCWin.SkinControl.SkinProgressBar filesskinProgressBar;
        private XPTable.Models.TextColumn textColumn1;
        private XPTable.Models.TextColumn textColumn2;
        private XPTable.Models.TextColumn textColumn3;
        private XPTable.Models.ColumnModel FolderListcolumnModel;
        private XPTable.Models.TableModel FolderListtableModel;
        private System.Windows.Forms.ImageList FolderShowImg;
        private XPTable.Models.Table BucketList;
        private XPTable.Models.TableModel BuckettableModel;
        public XPTable.Models.ColumnModel BucketcolumnModel;
        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private CCWin.SkinControl.SkinTabPage skinTabPage2;
        private XPTable.Models.Table uploadtable;
        private XPTable.Models.ColumnModel uploadcolumnModel;
        private XPTable.Models.TableModel uploadtableModel;
        private CCWin.SkinControl.SkinContextMenuStrip FolderListskinContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private CCWin.SkinControl.SkinContextMenuStrip uploadlistskinContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearcomplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearallToolStripMenuItem;
        private XPTable.Models.Table FolderList;
    }
}

