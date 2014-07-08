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
            FormChoose formChoose = new FormChoose();
            formChoose.ShowDialog();
            this.Text += " - " + formChoose.cbbCode.SelectedItem;
        }

        string strConnection = System.IO.Directory.GetCurrentDirectory() + @"\CFSJ.db";//数据目录
        public int selectCode = new int();
        public string selectGTS = "";


        BiultIndex bcindex = new BiultIndex();
        BiultCFGZS bcfgzs = new BiultCFGZS();
        BiultCFJDS bcfjds = new BiultCFJDS();
        BiultCLJD bccljd = new BiultCLJD();
        string[] dataSouce;
        ArrayList dataList = new ArrayList();
        DataCFSJ data = new DataCFSJ();

        string newData = Directory.GetCurrentDirectory() + @"\CFSJ.db";//当前数据库
        string oldData = Directory.GetCurrentDirectory() + @"\backup\CFSJ.db";//备份数据库

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
                string ids = tbxID.Text;
                generate(ids);
                
            } catch (Exception ex) { throw ex; }
        }

        private void generate(string ids) {
            string code = "";
            switch ((this.Text.Split(' '))[2]) {
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
            try {
                Thread t1 = new Thread(() => {
                    foreach (string id in ids.Split('，')) {
                        readData(id);
                        int length = dataList.Count;
                        double count = 0;

                        BiultReportForm brf = new BiultReportForm();
                        //lock (lblSpeed) ;
                        for (int i = 0; i < dataList.Count; i++) {
                            data = (DataCFSJ)dataList[i];
                            data.Code = code;
                            if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town)) {//判断文件目录是否已经存在
                                Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town);//创建文件夹
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
                            brf.SaveWord(filePath);//保存
                            count++;
                            lblSpeed.Text = Math.Round(count * 100 / length, 2) + "%";
                            prbSpeed.Value = (int)(count * 100 / length);
                        }
                        lblSpeed.Text = "100%";

                    }
                    System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory());
                });
                t1.Start();
                
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void dataFill() {
            try {
                foreach (DataRow dr in dataSet.Tables[0].Rows) {
                    try {

                        if (IsNumber(dr[0].ToString())) {
                            data = new DataCFSJ();//初始化数据
                            //data.ID = Int32.Parse(dr[0].ToString());
                            data.Name = dr[1].ToString();//户主姓名
                            data.CardID = dr[2].ToString().Replace("\n", "").Trim();//身份证
                            data.Location = dr[4].ToString();//座落地点
                            data.BuildDate = Int32.Parse(dr[5].ToString());//建成年月
                            data.Area = double.Parse(dr[6].ToString());//实地占地面积
                            data.LegalArea = double.Parse(dr[7].ToString());//合法面积
                            data.IllegaArea = double.Parse(dr[8].ToString());//超出面积
                            data.IllegaUnit = Int32.Parse(dr[9].ToString());//单价
                            data.Price = double.Parse(dr[10].ToString());//处罚金额
                            data.Layer = double.Parse(dr[11].ToString());//建设层数
                            //data.IsnotConfiscate = !string.IsNullOrEmpty(dr[23].ToString());//是否没收
                            data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//总金额
                            data.Town = cbbTowns.SelectedItem.ToString();//所在乡镇

                            data.CardIDs = data.CardID.Split('、');
                            if (data.CardIDs.Length != data.Names.Length) {
                                throw new Exception(data.Name+"，"+data.Location+"处数据有误请核实");                                    
                            }
                            foreach (string cardid in data.CardIDs) {
                                if (cardid.Length != 18&&cardid.Length!=0) {
                                    //MessageBox.Show(data.Name+"，"+data.Location+"处身份证号码数据有误请核实");
                                    //Exception ex=new Exception();
                                    //ex.Message=data.Name+"，"+data.Location+"处身份证号码数据有误请核实";
                                    throw new Exception(data.Name + "，" + data.Location + "处身份证号码数据有误请核实");                                    
                                }
                            }
                            data.Guid = System.Guid.NewGuid().ToString();//GUID生成
                            if (data.IsnotConfiscate) {
                                data.ConfiscateArea = double.Parse(dr[23].ToString());//没收面积
                                data.ConfiscateAreaUnit = Int32.Parse(dr[24].ToString());//没收单价
                                data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//没收金额
                                data.ConfiscateFloorArea = data.ConfiscateArea / data.Layer;//没收占地面积
                            }
                            if (data.IllegaArea / data.Names.Length > 0) {//判断是否要处罚，平均每户小于1平方免于处罚
                                dataList.Add(data);
                            }
                        }
                    } catch (Exception ex) {
                        throw ex;
                    }

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsNumber(string strNumber) {
            //看要用哪種規則判斷，自行修改strValue即可

            //strValue = @"^\d+[.]?\d*$";//非負數字
            string strValue = @"^\d+(\.)?\d*$";//數字
            //strValue = @"^\d+$";//非負整數
            //strValue = @"^-?\d+$";//整數
            //strValue = @"^-[0-9]*[1-9][0-9]*$";//負整數
            //strValue = @"^[0-9]*[1-9][0-9]*$";//正整數
            //strValue = @"^((-\d+)|(0+))$";//非正整數

            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(strValue);
            return r.IsMatch(strNumber);
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
                foreach(string name in dataSouce){
                    source.Append(name);
                    source.Append(";");
                }
                tbxDataSource.Text = source.ToString();
            }
        }
        

        private DataSet importExcelToDataSet(string souce) {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + souce + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [处罚金额$A:AB]", strConn);
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
                Thread t1 = new Thread(() => {
                    foreach (string source in dataSouce) {
                        dataSet = importExcelToDataSet(source);//导入数据

                        dataFill();
                    }
                    insertData();
                    MessageBox.Show("数据导入完毕");
                });
                t1.Start();
            }
        }

        private void insertData() {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into 鹤城所 (");
            sql.Append("户主,");                                
            sql.Append("身份证号,");                            
            sql.Append("乡镇,");                                
            sql.Append("土地座落,");                            
            sql.Append("占地面积,");                            
            sql.Append("建筑面积,");                            
            sql.Append("层数,");                                
            sql.Append("建成年月,");                            
            sql.Append("审批面积,");                            
            sql.Append("超建面积,");                            
            sql.Append("单价,");                                
            sql.Append("金额,");                                
            sql.Append("没收占地面积,");                        
            sql.Append("没收建筑面积,");                        
            sql.Append("没收单价,");                            
            sql.Append("没收金额,");                            
            sql.Append("GUID");                                 
            sql.Append(") values (");
            sql.Append("@Name,");
            sql.Append("@CardID,");
            sql.Append("@Town,");
            sql.Append("@Location,");
            sql.Append("@Area,");
            sql.Append("@ConfiscateArea,");
            sql.Append("@Layer,");
            sql.Append("@BuildDate,");
            sql.Append("@LegalArea,");
            sql.Append("@IllegaArea,");
            sql.Append("@IllegaUnit,");
            sql.Append("@Price,");
            sql.Append("@ConfiscateFloorArea,");
            sql.Append("@ConfiscateArea,");
            sql.Append("@ConfiscateAreaUnit,");
            sql.Append("@ConfiscateAreaPrice,");
            sql.Append("@Guid)");
            CFSJDal db = new CFSJDal(strConnection);
            foreach (DataCFSJ data in dataList) {
                SQLiteParameter[] parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Name",data.Name.ToString()),
                    new SQLiteParameter("@CardID",data.CardID.ToString()),
                    new SQLiteParameter("@Town",data.Town.ToString()),
                    new SQLiteParameter("@Location",data.Location.ToString()),
                    new SQLiteParameter("@Area",data.Area.ToString()),
                    new SQLiteParameter("@ConfiscateArea",data.ConfiscateArea.ToString()),
                    new SQLiteParameter("@Layer",data.Layer.ToString()),
                    new SQLiteParameter("@BuildDate",data.BuildDate.ToString()),
                    new SQLiteParameter("@LegalArea",data.LegalArea.ToString()),
                    new SQLiteParameter("@IllegaArea",data.IllegaArea.ToString()),
                    new SQLiteParameter("@IllegaUnit",data.IllegaUnit.ToString()),
                    new SQLiteParameter("@Price",data.Price.ToString()),
                    new SQLiteParameter("@ConfiscateFloorArea",data.ConfiscateFloorArea.ToString()),
                    new SQLiteParameter("@ConfiscateArea",data.ConfiscateArea.ToString()),
                    new SQLiteParameter("@ConfiscateAreaUnit",data.ConfiscateAreaUnit.ToString()),
                    new SQLiteParameter("@ConfiscateAreaPrice",data.ConfiscateAreaPrice.ToString()),
                    new SQLiteParameter("@Guid",data.Guid.ToString())
                };
                if (data.ConfiscateArea != 0) {
                    StringBuilder _sql = new StringBuilder();
                    _sql.Append("insert into 鹤城所没收 ");
                    _sql.Append("(GUID) values ");
                    _sql.Append("(@guid)");
                    SQLiteParameter[] pt = new SQLiteParameter[]{
                        new SQLiteParameter("@guid",data.Guid)
                    };
                    db.ExecuteNonQuery(_sql.ToString(), pt);
                }
                db.ExecuteNonQuery(sql.ToString(), parameters);
            }
        }

        private void readData(string id) {
            
            dataList = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.Append("select 鹤城所.*, 鹤城所没收.ID from  鹤城所 left join 鹤城所没收 on 鹤城所.GUID=鹤城所没收.GUID ");
            if (!string.IsNullOrEmpty(id)) {
                if (id.Split('-').Length == 1) {
                    sql.Append("where 鹤城所.id = ");
                    sql.Append(id);
                } else {
                    string a = id.Split('-')[0];
                    string b = id.Split('-')[1];
                    sql.Append("where ");

                    sql.Append("鹤城所.id>= ");
                    if (string.IsNullOrEmpty(a)) {
                        sql.Append(0);
                    } else {
                        sql.Append(a);
                    }
                    if (!string.IsNullOrEmpty(b)) {
                        sql.Append(" and 鹤城所.id<= ");
                        sql.Append(b);
                    }
                }
            }
            CFSJDal db = new CFSJDal(strConnection);
            using (SQLiteDataReader reader = db.ExecuteReader(sql.ToString(), null)) {
                while (reader.Read()) {
                    data = new DataCFSJ();
                    data.ID = Int32.Parse(reader[0].ToString());
                    data.Name = reader[1].ToString();
                    data.CardID = reader[3].ToString();
                    data.Town = reader[4].ToString();
                    data.Location = reader[8].ToString();
                    data.Area = double.Parse(reader[9].ToString());
                    data.Layer = double.Parse(reader[18].ToString());
                    data.BuildDate = Int32.Parse(reader[13].ToString());
                    data.LegalArea = double.Parse(reader[15].ToString());
                    data.IllegaArea = double.Parse(reader[17].ToString());
                    data.IllegaUnit = Int32.Parse(reader[18].ToString());
                    data.Price = double.Parse(reader[20].ToString());
                    data.ConfiscateFloorArea = double.Parse(reader[21].ToString());
                    data.ConfiscateArea = double.Parse(reader[22].ToString());
                    data.ConfiscateAreaUnit = Int32.Parse(reader[23].ToString());
                    data.ConfiscateAreaPrice = double.Parse(reader[24].ToString());
                    data.Guid = reader[26].ToString();
                    data.CardIDs = data.CardID.Split('、');
                    if (!string.IsNullOrEmpty(reader[27].ToString())) {
                        data.ConfiscateID = Int32.Parse(reader[27].ToString());
                    }
                    dataList.Add(data);
                }

            };


        }

        private void BiultForm_Load(object sender, EventArgs e) {
           
            
        }



        private void tbxID_MouseEnter(object sender, EventArgs e) {
            ToolTip tooltip = new ToolTip();
            tooltip.Show("请键入要生成的批文号/或批文范围（用逗号分开）例如：1，3，5-12", tbxID);
        }

        private void tbxID_TextChanged(object sender, EventArgs e) {

        }

        private void tsmQuit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmSaveData_Click(object sender, EventArgs e) {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\backup")) {//判断文件目录是否已经存在
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\backup" );//创建文件夹
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


    }
}
