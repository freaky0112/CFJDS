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

namespace CFJDS {
    public partial class BiultForm : Form {
        public BiultForm() {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            cbbTowns.SelectedIndex = 0;
            cbbCode.SelectedIndex = 0;

        }

        string strConnection = System.IO.Directory.GetCurrentDirectory() + @"\test.db";//数据目录



        BiultIndex bcindex = new BiultIndex();
        BiultCFGZS bcfgzs = new BiultCFGZS();
        BiultCFJDS bcfjds = new BiultCFJDS();
        BiultCLJD bccljd = new BiultCLJD();
        string dataSouce = "";
        ArrayList dataList = new ArrayList();
        DataCFSJ data = new DataCFSJ();

        //string pText;// 文本信息   
        //int pFontSize;//字体大小  
        //string pFontName = "宋体";
        //Microsoft.Office.Interop.Word.WdColor pFontColor=Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        //int pFontBold;//字体粗体  
        //Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment;//方向 

        private void btnGenerate_Click(object sender, EventArgs e) {

            //dataSet = importExcelToDataSet(dataSouce);//导入数据
            //dataFill();
            generate();

        }

        private void generate() {
            string code = "ABCDEFG";
            
            readData();
            int length = dataList.Count;
            double count = 0;
            Thread t1 = new Thread(() => {
                BiultReportForm brf = new BiultReportForm();
                lock (lblSpeed) ;
                for (int i = 0; i < dataList.Count; i++) {
                    data = (DataCFSJ)dataList[i];
                    data.Code = code[cbbCode.SelectedIndex].ToString();
                    if (!Directory.Exists(System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town)){//判断文件目录是否已经存在
                        Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town);//创建文件夹
                    }
                    string filePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town + @"\" + data.No + data.Name + @".doc";
                    brf.CreateAWord();//创建文档
                    bcindex.addIndex(brf, data);//创建目录
                    bcfgzs.addCFGZS(brf, data);//创建处罚告知书
                    bcfjds.addCFJDS(brf, data);//创建处罚决定书
                    if (!data.ConfiscateNo.ToString().Equals("0")) {
                        bccljd.addCLJD(brf, data);//创建处理决定
                    }
                    brf.SaveWord(filePath);//保存
                    count++;
                    lblSpeed.Text = Math.Round(count * 100 / length, 2) + "%";
                    prbSpeed.Value = (int)(count * 100 / length);
                }
                lblSpeed.Text = "100%";
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory());

            });
            t1.Start();


            /*
            Thread t2 = new Thread(() => {
                BiultReportForm brf = new BiultReportForm();
                lock (lblSpeed) ;
                for (int i = dataList.Count / 4; i < dataList.Count * 2 / 4; i++) {
                    data = (DataCFSJ)dataList[i];

                    string filePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.No + data.Name + @".doc";
                    brf.CreateAWord();//创建文档
                    bcf.addCFGZS(brf, data);//创建处罚告知书
                    brf.SaveWord(filePath);//保存
                    count++;
                    lblSpeed.Text = ((count * 100 / length).ToString() + "000").Substring(0, 4) + "%";
                    prbSpeed.Value = (int)(count * 100 / length);
                }
            });
            t2.Start();

            Thread t3 = new Thread(() => {
                BiultReportForm brf = new BiultReportForm();
                lock (lblSpeed) ;
                for (int i = dataList.Count * 2 / 4; i < dataList.Count * 3 / 4; i++) {
                    data = (DataCFSJ)dataList[i];

                    string filePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.No + data.Name + @".doc";
                    brf.CreateAWord();//创建文档
                    bcf.addCFGZS(brf, data);//创建处罚告知书
                    brf.SaveWord(filePath);//保存
                    count++;
                    lblSpeed.Text = ((count * 100 / length).ToString() + "000").Substring(0, 4) + "%";
                    prbSpeed.Value = (int)(count * 100 / length);
                }
            });
            t3.Start();

            Thread t4 = new Thread(() => {
                BiultReportForm brf = new BiultReportForm();
                lock (lblSpeed) ;
                for (int i = dataList.Count * 3 / 4; i < dataList.Count; i++) {
                    data = (DataCFSJ)dataList[i];

                    string filePath = System.IO.Directory.GetCurrentDirectory() + @"\" + data.No + data.Name + @".doc";
                    brf.CreateAWord();//创建文档
                    bcf.addCFGZS(brf, data);//创建处罚告知书
                    brf.SaveWord(filePath);//保存
                    count++;
                    lblSpeed.Text = ((count * 100 / length).ToString() + "000").Substring(0, 4) + "%";
                    prbSpeed.Value = (int)(count * 100 / length);
                }
            });
            t4.Start();*/
        }

        private void dataFill() {

            foreach (DataRow dr in dataSet.Tables[0].Rows) {
                if (IsNumber(dr[0].ToString())) {
                    data = new DataCFSJ();//初始化数据
                    data.No = Int32.Parse(dr[0].ToString());
                    data.Name = dr[1].ToString();//户主姓名
                    data.Location = dr[4].ToString();//地点
                    data.Date = Int32.Parse(dr[5].ToString().Substring(0, 4));//年份
                    data.Area = double.Parse(dr[8].ToString());//超出面积
                    data.Unit = Int32.Parse(dr[9].ToString());//单价
                    data.Price = double.Parse(dr[10].ToString());//处罚金额
                    data.IsnotConfiscate = !string.IsNullOrEmpty(dr[23].ToString());//是否没收
                    data.ConfiscateAreaPrice = double.Parse(dr[25].ToString());//总金额
                    data.Town = cbbTowns.SelectedItem.ToString();//所在乡镇
                    data.ID = dr[2].ToString(); ;//身份证
                    data.IDs = data.ID.Split('、');
                    data.Guid = System.Guid.NewGuid().ToString();
                    if (data.IsnotConfiscate) {
                        data.ConfiscateArea = double.Parse(dr[23].ToString());//没收面积
                        data.ConfiscateAreaUnit = Int32.Parse(dr[24].ToString());//没收单价
                    }
                    if (data.Area / data.Names.Length > 1) {//判断是否要处罚，平均每户小于1平方免于处罚
                        dataList.Add(data);
                    }
                }

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
            openFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                dataSouce = openFileDialog.FileName;
                tbxDataSource.Text = dataSouce;
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
                dataSet = importExcelToDataSet(dataSouce);//导入数据
                dataFill();
                insertData();
                MessageBox.Show("数据导入完毕");
            }
        }

        private void insertData() {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into CFSJ ");
            sql.Append("(户主,身份证,乡镇,所在地,建成年月,超面积,单价,金额,没收面积,没收单价,没收价格,GUID)");
            sql.Append(" values ");
            sql.Append("(@name,@cardid,@town,@location,@date,@area,@unit,@price,@confiscateArea,@confiscateAreaUnit,@confiscateAreaPrice,@guid)");
            CFSJDal db = new CFSJDal(strConnection);
            foreach (DataCFSJ data in dataList) {
                SQLiteParameter[] parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@name",data.Name),
                    new SQLiteParameter("@cardid",data.ID),
                    new SQLiteParameter("@town",data.Town),
                    new SQLiteParameter("@location",data.Location),
                    new SQLiteParameter("@date",data.Date.ToString()),
                    new SQLiteParameter("area",data.Area.ToString()),
                    new SQLiteParameter("@unit",data.Unit.ToString()),
                    new SQLiteParameter("@price",data.Price.ToString()),
                    new SQLiteParameter("@confiscateArea",data.ConfiscateArea.ToString()),
                    new SQLiteParameter("@confiscateAreaUnit",data.ConfiscateAreaUnit.ToString()),
                    new SQLiteParameter("@confiscateAreaPrice",data.ConfiscateAreaPrice.ToString()),
                    new SQLiteParameter("@guid",data.Guid)
                };
                if (data.ConfiscateArea != 0) {
                    StringBuilder _sql = new StringBuilder();
                    _sql.Append("insert into MSSJ ");
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

        private void readData() {
            
            dataList = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.Append("select CFSJ.*, MSSJ.* from  CFSJ left join MSSJ on CFSJ.GUID=MSSJ.GUID");
            CFSJDal db = new CFSJDal(strConnection);
            using (SQLiteDataReader reader = db.ExecuteReader(sql.ToString(), null)) {
                while (reader.Read()) {
                    data = new DataCFSJ();
                    data.No = Int32.Parse(reader[0].ToString()); ;
                    data.Name = reader[1].ToString();
                    data.ID = reader[2].ToString();
                    data.Town = reader[3].ToString();
                    data.Location = reader[4].ToString();
                    data.Date = Int32.Parse(reader[5].ToString());
                    data.Area = double.Parse(reader[6].ToString());
                    data.Unit = Int32.Parse(reader[7].ToString());
                    data.Price = double.Parse(reader[8].ToString());
                    data.ConfiscateArea = double.Parse(reader[9].ToString());
                    data.ConfiscateAreaUnit = Int32.Parse(reader[10].ToString());
                    data.ConfiscateAreaPrice = double.Parse(reader[11].ToString());
                    data.IDs = data.ID.Split('、');
                    data.Guid = reader[12].ToString();
                    if (!string.IsNullOrEmpty(reader[13].ToString())) {
                        data.ConfiscateNo = Int32.Parse(reader[13].ToString());
                    }
                    dataList.Add(data);
                }

            };


        }
    }
}
