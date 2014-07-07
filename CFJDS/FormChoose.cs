using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CFJDS {
    public partial class FormChoose : Form {
        public FormChoose() {
            InitializeComponent();
            cbbCode.SelectedIndex = 0;

        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.Close();
        }

        private int _code;
        
        public int Code {
            get { return _code; }
            set { _code = this.cbbCode.SelectedIndex; }
        }
    }
}
