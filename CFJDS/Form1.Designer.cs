﻿namespace CFJDS {
    partial class BiultForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BiultForm));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbxDataSource = new System.Windows.Forms.TextBox();
            this.dataSet = new System.Data.DataSet();
            this.prbSpeed = new System.Windows.Forms.ProgressBar();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.cbbTowns = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSaveData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRevertData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tbxID = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.tvwIDs = new System.Windows.Forms.TreeView();
            this.cmsTvw = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSigned = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUnsigned = new System.Windows.Forms.ToolStripMenuItem();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmExport = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.cmsTvw.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(408, 172);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tbxDataSource
            // 
            this.tbxDataSource.Location = new System.Drawing.Point(12, 27);
            this.tbxDataSource.Name = "tbxDataSource";
            this.tbxDataSource.Size = new System.Drawing.Size(471, 21);
            this.tbxDataSource.TabIndex = 1;
            this.tbxDataSource.DoubleClick += new System.EventHandler(this.tbxDataSource_DoubleClick);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "NewDataSet";
            // 
            // prbSpeed
            // 
            this.prbSpeed.Location = new System.Drawing.Point(12, 509);
            this.prbSpeed.Name = "prbSpeed";
            this.prbSpeed.Size = new System.Drawing.Size(434, 23);
            this.prbSpeed.Step = 1;
            this.prbSpeed.TabIndex = 2;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(452, 509);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(17, 12);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "0%";
            // 
            // cbbTowns
            // 
            this.cbbTowns.FormattingEnabled = true;
            this.cbbTowns.Items.AddRange(new object[] {
            "北山镇",
            "船寮镇",
            "东源镇",
            "方山乡",
            "阜山乡",
            "高湖镇",
            "高市乡",
            "贵岙乡",
            "海口镇",
            "海溪乡",
            "鹤城街道",
            "黄垟乡",
            "季宅乡",
            "巨浦乡",
            "腊口镇",
            "瓯南街道",
            "仁宫乡",
            "仁庄镇",
            "山口镇",
            "石溪乡",
            "舒桥乡",
            "汤垟乡",
            "万阜乡",
            "万山乡",
            "温溪镇",
            "吴坑乡",
            "小舟山乡",
            "油竹街道",
            "章村乡",
            "章旦乡",
            "祯埠乡",
            "祯旺乡"});
            this.cbbTowns.Location = new System.Drawing.Point(77, 59);
            this.cbbTowns.Name = "cbbTowns";
            this.cbbTowns.Size = new System.Drawing.Size(121, 20);
            this.cbbTowns.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "导入乡镇:";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(204, 57);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(493, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.tsmExport,
            this.toolStripSeparator1,
            this.tsmSaveData,
            this.tsmRevertData,
            this.toolStripSeparator2,
            this.tsmQuit});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.tbxDataSource_DoubleClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmSaveData
            // 
            this.tsmSaveData.Name = "tsmSaveData";
            this.tsmSaveData.Size = new System.Drawing.Size(152, 22);
            this.tsmSaveData.Text = "保存导入数据";
            this.tsmSaveData.Click += new System.EventHandler(this.tsmSaveData_Click);
            // 
            // tsmRevertData
            // 
            this.tsmRevertData.Name = "tsmRevertData";
            this.tsmRevertData.Size = new System.Drawing.Size(152, 22);
            this.tsmRevertData.Text = "还原数据";
            this.tsmRevertData.Click += new System.EventHandler(this.tsmRevertData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmQuit
            // 
            this.tsmQuit.Name = "tsmQuit";
            this.tsmQuit.Size = new System.Drawing.Size(152, 22);
            this.tsmQuit.Text = "退出";
            this.tsmQuit.Click += new System.EventHandler(this.tsmQuit_Click);
            // 
            // tbxID
            // 
            this.tbxID.Location = new System.Drawing.Point(290, 174);
            this.tbxID.Name = "tbxID";
            this.tbxID.Size = new System.Drawing.Size(100, 21);
            this.tbxID.TabIndex = 8;
            this.tbxID.TextChanged += new System.EventHandler(this.tbxID_TextChanged);
            this.tbxID.MouseEnter += new System.EventHandler(this.tbxID_MouseEnter);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(408, 143);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // tvwIDs
            // 
            this.tvwIDs.ContextMenuStrip = this.cmsTvw;
            this.tvwIDs.HotTracking = true;
            this.tvwIDs.Location = new System.Drawing.Point(12, 201);
            this.tvwIDs.Name = "tvwIDs";
            this.tvwIDs.Size = new System.Drawing.Size(471, 302);
            this.tvwIDs.TabIndex = 11;
            this.tvwIDs.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwIDs_NodeMouseDoubleClick);
            this.tvwIDs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwIDs_MouseDown);
            // 
            // cmsTvw
            // 
            this.cmsTvw.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDelete,
            this.tsmSigned,
            this.tsmUnsigned});
            this.cmsTvw.Name = "cmsTvw";
            this.cmsTvw.Size = new System.Drawing.Size(137, 70);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(136, 22);
            this.tsmDelete.Text = "删除";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // tsmSigned
            // 
            this.tsmSigned.Name = "tsmSigned";
            this.tsmSigned.Size = new System.Drawing.Size(136, 22);
            this.tsmSigned.Text = "标记已处罚";
            this.tsmSigned.Click += new System.EventHandler(this.tsmSigned_Click);
            // 
            // tsmUnsigned
            // 
            this.tsmUnsigned.Name = "tsmUnsigned";
            this.tsmUnsigned.Size = new System.Drawing.Size(136, 22);
            this.tsmUnsigned.Text = "标记未处罚";
            this.tsmUnsigned.Click += new System.EventHandler(this.tsmUnsigned_Click);
            // 
            // cbbType
            // 
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Items.AddRange(new object[] {
            "编号：",
            "户主：",
            "土地坐落："});
            this.cbbType.Location = new System.Drawing.Point(204, 175);
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(80, 20);
            this.cbbType.TabIndex = 12;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(493, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssState
            // 
            this.tssState.Name = "tssState";
            this.tssState.Size = new System.Drawing.Size(0, 17);
            // 
            // tsmExport
            // 
            this.tsmExport.Name = "tsmExport";
            this.tsmExport.Size = new System.Drawing.Size(152, 22);
            this.tsmExport.Text = "导出";
            this.tsmExport.Click += new System.EventHandler(this.tsmExport_Click);
            // 
            // BiultForm
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 544);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbbType);
            this.Controls.Add(this.tvwIDs);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.tbxID);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbTowns);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.prbSpeed);
            this.Controls.Add(this.tbxDataSource);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BiultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处罚数据处理";
            this.Load += new System.EventHandler(this.BiultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cmsTvw.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox tbxDataSource;
        private System.Data.DataSet dataSet;
        private System.Windows.Forms.ProgressBar prbSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.ComboBox cbbTowns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveData;
        private System.Windows.Forms.TextBox tbxID;
        private System.Windows.Forms.ToolStripMenuItem tsmQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmRevertData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TreeView tvwIDs;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.ContextMenuStrip cmsTvw;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmSigned;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssState;
        private System.Windows.Forms.ToolStripMenuItem tsmUnsigned;
        private System.Windows.Forms.ToolStripMenuItem tsmExport;
    }
}

