using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using System.Collections;
using System.Data.OleDb;
using System.Threading;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace CFJDS {
    public partial class BiultForm : Form {
        public BiultForm() {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            cbbTowns.SelectedIndex = 0;
            cbbType.SelectedIndex = 0;
            FormChoose formChoose = new FormChoose();
            formChoose.ShowDialog();
            this.Text += " - " + formChoose.cbbCode.SelectedItem;
            code = getCode(formChoose.cbbCode.SelectedItem.ToString());
            //版本更新
            //bool isExsit=dataOperate.getTableExist("户主");
            //update.updateSqlite(formChoose.cbbCode.SelectedItem.ToString());
        }

        //string strConnection = System.IO.Directory.GetCurrentDirectory() + @"\CFSJ.db";//数据目录
        public int selectCode = new int();
        public string selectGTS = "";


        BiultIndex bcindex = new BiultIndex();
        BiultCFGZS bcfgzs = new BiultCFGZS();
        BiultCFJDS bcfjds = new BiultCFJDS();
        BiultCLJD bccljd = new BiultCLJD();
        BiultJKTZ bcjktz = new BiultJKTZ();
        string[] dataSouce;
        ArrayList dataList = new ArrayList();
        //DataCFSJ data = new DataCFSJ();

        string newData = Directory.GetCurrentDirectory() + @"\CFSJ.db";//当前数据库
        string oldData = Directory.GetCurrentDirectory() + @"\backup\CFSJ.db";//备份数据库
        string code = "";
        //string pText;// 文本信息   
        //int pFontSize;//字体大小  
        //string pFontName = "宋体";
        //Microsoft.Office.Interop.Word.WdColor pFontColor=Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        //int pFontBold;//字体粗体  
        //Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment;//方向 

        private void btnGenerate_Click(object sender, EventArgs e) {

            //dataSet = importExcelToDataSet(dataSouce);//导入数据
            //dataFill();
            try {
                //string ids = tbxID.Text;
                generate();

            } catch (Exception ex) {
                throw ex;
            }
        }

        private string getCode(string str) {
            string code = "";
            switch (str) {
                case "鹤城所":
                    code = "A";
                    break;
                case "温溪所":
                    code = "B";
                    break;
                case "山口所":
                    code = "C";
                    break;
                case "船寮所":
                    code = "D";
                    break;
                case "东源所":
                    code = "E";
                    break;
                case "腊口所":
                    code = "F";
                    break;
                case "北山所":
                    code = "G";
                    break;
            }
            return code;
        }

        private void generate() {
            string generatePath = System.IO.Directory.GetCurrentDirectory();

            try {
                Thread t1 = new Thread(() => {
                    //foreach (string id in ids.Split('，')) {
                    //readData(id);
                    int length = dataList.Count;
                    double count = 0;

                    BiultReportForm brf = new BiultReportForm();
                    //lock (lblSpeed) ;
                    for (int i = 0; i < dataList.Count; i++) {
                        DataCFSJ data = (DataCFSJ)dataList[i];
                        data.Code = code;
                        generatePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town;
                        if (!Directory.Exists(generatePath)) {//判断文件目录是否已经存在
                            Directory.CreateDirectory(generatePath);//创建文件夹
                        }
                        string filePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town + @"\" + data.ID + data.Name + @".doc";
                        brf.CreateAWord();//创建文档
                        bcindex.addIndex(brf, data);//创建目录
                        bcfgzs.addCFGZS(brf, data);//创建处罚告知书
                        bcfgzs.addCFGZS(brf, data);//创建处罚告知书
                        bcfjds.addCFJDS(brf, data);//创建处罚决定书
                        bcfjds.addCFJDS(brf, data);//创建处罚决定书
                        if (!data.ConfiscateID.ToString().Equals("0")) {
                            bccljd.addCLJD(brf, data);//创建处理决定
                            bccljd.addCLJD(brf, data);//创建处理决定
                        }
                        bcjktz.addJKTZ(brf, data);//创建缴款通知单                            
                        brf.TypeBackspace();
                        brf.TypeBackspace();
                        brf.SaveWord(filePath);//保存
                        count++;
                        lblSpeed.Text = Math.Round(count * 100 / length, 2) + "%";
                        prbSpeed.Value = (int)(count * 100 / length);
                    }
                    lblSpeed.Text = "100%";

                    //}
                    System.Diagnostics.Process.Start(generatePath);
                });
                t1.Start();

            } catch (Exception ex) {
                throw ex;
            }
        }

        private void dataFill() {
            try {
                dataList = new ArrayList();
                DataCFSJ data = new DataCFSJ();
                foreach (DataRow dr in dataSet.Tables[0].Rows) {
                    try {

                        if (Common.IsNumber(dr[0].ToString())) {
                            data = new DataCFSJ();//初始化数据
                            //data.ID = Int32.Parse(dr[0].ToString());
                            data.Name = dr[1].ToString();//户主姓名
                            data.CardID = dr[2].ToString().Replace("\n", "").Trim();//身份证
                            data.Accounts = dr[3].ToString().Replace("\n", "").Trim();//家庭人口
                            data.Location = dr[4].ToString();//座落地点
                            data.BuildDate = Int32.Parse(dr[5].ToString());//建成年月
                            data.Area = double.Parse(dr[6].ToString());//实地占地面积
                            data.LegalArea = double.Parse(dr[7].ToString());//合法面积
                            data.IllegaArea = double.Parse(dr[8].ToString());//超出面积
                            data.IllegaUnit = double.Parse(dr[9].ToString());//单价
                            data.Price = double.Parse(dr[10].ToString());//处罚金额
                            data.Layer = double.Parse(dr[11].ToString());//建设层数
                            data.Conform = dr[22].ToString();//土地利用总体规划
                            data.Available = dr[21].ToString();//建房资格
                            data.Control = dr[29].ToString();//控制区
                            data.LandOwner = dr[28].ToString();//土地性质
                            //data.IsnotConfiscate = !string.IsNullOrEmpty(dr[23].ToString());//是否没收
                            //data.IsnotConfiscate = ConfiscateCalculate.isNotConfiscate(data);
                            //data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//总金额
                            data.Town = cbbTowns.SelectedItem.ToString();//所在乡镇
                            data.Accounts = dr[3].ToString();//户口人数
                            data.CardIDs = data.CardID.Split('、');
                            if (data.CardIDs.Length != data.Names.Length) {
                                throw new Exception("，" + data.Location + "处数据有误请核实");
                            }
                            //foreach (string cardid in data.CardIDs) {
                            //    if (cardid.Length != 18 && cardid.Length != 0) {
                            //        //MessageBox.Show(data.Name+"，"+data.Location+"处身份证号码数据有误请核实");
                            //        //Exception ex=new Exception();
                            //        //ex.Message=data.Name+"，"+data.Location+"处身份证号码数据有误请核实";
                            //        throw new Exception( "，" + data.Location + "处身份证号码数据有误请核实");
                            //    }
                            //}
                            data.Guid = System.Guid.NewGuid().ToString();//GUID生成
                            data = ConfiscateCalculate.getConfiscateData(data);

                            if (data.IsnotConfiscate) {

                                //data.ConfiscateArea = double.Parse(dr[23].ToString());//没收面积
                                //data.ConfiscateAreaUnit = Int32.Parse(dr[24].ToString());//没收单价
                                //data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//没收金额
                                //data.ConfiscateFloorArea = data.ConfiscateArea / data.Layer;//没收占地面积
                            }
                            if (data.IllegaArea / data.Names.Length > 0) {//判断是否要处罚，平均每户小于1平方免于处罚
                                dataList.Add(data);
                            }
                        }
                    } catch (Exception ex) {
                        throw new Exception(data.Name.ToString() + ex.Message);
                    }

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }





        private void tbxDataSource_DoubleClick(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                dataSouce = openFileDialog.FileNames;
                StringBuilder source = new StringBuilder();
                foreach (string name in dataSouce) {
                    source.Append(name);
                    source.Append(";");
                }
                tbxDataSource.Text = source.ToString();
            }
        }


        private DataSet importExcelToDataSet(string souce) {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + souce + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [处罚金额$A:AF]", strConn);
            DataSet myDataSet = new DataSet();
            try {
                myCommand.Fill(myDataSet);
            } catch (Exception ex) {
                throw ex;
            }
            conn.Close();
            conn.Dispose();
            return myDataSet;
        }

        private void btnImport_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(tbxDataSource.Text)) {
                MessageBox.Show("请选择数据来源");
            } else {
                try {
                    Thread t1 = new Thread(() => {
                        foreach (string source in dataSouce) {
                            dataSet = importExcelToDataSet(source);//导入数据

                            dataFill();
                        }
                        insertData();
                        MessageBox.Show("数据导入完毕");
                    });
                    t1.Start();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void insertData() {
            try {
                foreach (DataCFSJ data in dataList) {
                    dataOperate.dataInsert(data);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }


        private void readData(string id) {
            string type = cbbType.SelectedItem.ToString();
            dataList = dataOperate.dataRead_Old(type, id);


        }

        private void BiultForm_Load(object sender, EventArgs e) {


        }



        private void tbxID_MouseEnter(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.Show("请键入要生成的批文号/或批文范围（用逗号分开）例如：1，3，5-12", tbxID);
        }

        private void tbxID_TextChanged(object sender, EventArgs e) {
            //string strValue = @"[^[\d\-\,]+$]";
            //System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(strValue);
            //if(!r.IsMatch(tbxID.Text)){
            //    tbxID.Text="";
            //}
        }

        private void tsmQuit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmSaveData_Click(object sender, EventArgs e) {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\backup")) {//判断文件目录是否已经存在
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\backup");//创建文件夹
            }

            File.Copy(newData, oldData, true);
        }

        private void tsmRevertData_Click(object sender, EventArgs e) {
            if (!File.Exists(oldData)) {
                MessageBox.Show("无保存数据！！");
            } else {
                File.Copy(oldData, newData, true);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            tvwIDs.Nodes.Clear();
            string ids = tbxID.Text;
            query(ids);
            tvwIDs.Nodes.Add(string.Empty);
        }

        private void query(string ids) {
            foreach (string id in ids.Split('，')) {
                readData(id);
            }
            int counts = dataList.Count;
            int singedCounts = 0;
            foreach (DataCFSJ data in dataList) {
                TreeNode tn = new TreeNode();
                tn.Text = code + String.Format("{0:0000}", data.ID) + ":" + data.Name + ";" + data.Location;
                if (data.Signed) {
                    tn.BackColor = Color.Red;
                    tn.ForeColor = Color.White;
                    singedCounts++;
                }
                //lsi.SubItems.Add(data.ID.ToString());
                tvwIDs.Nodes.Add(tn);
            }
            tssState.Text = "总计查询数据" + counts + "户，其中已处罚" + singedCounts + "户";
        }



        private void tvwIDs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            try {
                if (tvwIDs.SelectedNode.Index != tvwIDs.Nodes.Count - 1) {
                    DataCFSJ data = new DataCFSJ();
                    data = (DataCFSJ)dataList[tvwIDs.SelectedNode.Index];
                    DataQueryModify dataQueryModify = new DataQueryModify(data);
                    dataQueryModify.ShowDialog();
                }
                //FormChoose form = new FormChoose();
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void tvwIDs_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = tvwIDs.GetNodeAt(ClickPoint);
                if (CurrentNode != null) {
                    CurrentNode.ContextMenuStrip = cmsTvw;
                }
                tvwIDs.SelectedNode = CurrentNode;
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e) {
            try {
                DataCFSJ data = new DataCFSJ();
                int index = tvwIDs.SelectedNode.Index;
                data = (DataCFSJ)dataList[index];
                dataOperate.dataDelte(data, "鹤城所");
                dataOperate.dataDelte(data, "鹤城所没收");
                tvwIDs.SelectedNode.Remove();
                dataList.RemoveAt(index);
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 标记已处罚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSigned_Click(object sender, EventArgs e) {
            try {

                DataCFSJ data = new DataCFSJ();
                int index = tvwIDs.SelectedNode.Index;
                data = (DataCFSJ)dataList[index];
                dataOperate.dataSigned(data, true);
                tvwIDs.SelectedNode.BackColor = Color.Red;
                tvwIDs.SelectedNode.ForeColor = Color.White;
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 标记未处罚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUnsigned_Click(object sender, EventArgs e) {
            try {
                DataCFSJ data = new DataCFSJ();
                int index = tvwIDs.SelectedNode.Index;
                data = (DataCFSJ)dataList[index];
                dataOperate.dataSigned(data, false);
                tvwIDs.SelectedNode.BackColor = Color.White;
                tvwIDs.SelectedNode.ForeColor = Color.Black;
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmExport_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型  
            saveFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog.RestoreDirectory = true;
            try {

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    dataOperate.dataExport(dataList, saveFileDialog.FileName.ToString());
                }
            } catch (Exception ex) {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show("导出完成");
        }


    }
}
