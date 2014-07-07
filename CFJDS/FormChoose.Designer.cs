namespace CFJDS {
    partial class FormChoose {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbbCode = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblShow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbCode
            // 
            this.cbbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCode.FormattingEnabled = true;
            this.cbbCode.Items.AddRange(new object[] {
            "鹤城所",
            "温溪所",
            "山口所",
            "船寮所",
            "东源所",
            "腊口所",
            "北山所"});
            this.cbbCode.Location = new System.Drawing.Point(12, 41);
            this.cbbCode.Name = "cbbCode";
            this.cbbCode.Size = new System.Drawing.Size(124, 20);
            this.cbbCode.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 67);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(124, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(26, 15);
            this.lblShow.Margin = new System.Windows.Forms.Padding(0);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(101, 12);
            this.lblShow.TabIndex = 2;
            this.lblShow.Text = "请选择所在国土所";
            // 
            // FormChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 101);
            this.Controls.Add(this.lblShow);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbbCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblShow;
        public System.Windows.Forms.ComboBox cbbCode;
    }
}