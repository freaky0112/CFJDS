using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CFJDS {
    public partial class DataQueryModify : Form {
        public DataQueryModify(DataCFSJ OriginData) {
            InitializeComponent();
            data = OriginData;
            initializData(data);
        }
        DataCFSJ data = new DataCFSJ();

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void initializData(DataCFSJ data) {
            tbxID.Text = "A" + String.Format("{0:0000}", data.ID);
            tbxName.Text = data.Name.ToString();
            tbxCardID.Text = data.CardID.ToString();
            tbxTown.Text = data.Town.ToString();
            tbxAccounts.Text = data.Accounts.ToString();
            tbxLocation.Text = data.Location.ToString();
            tbxControl.Text = data.Control.ToString();
            tbxLandOwner.Text = data.LandOwner.ToString();
            tbxArea.Text = data.Area.ToString();
            tbxLayer.Text = data.Layer.ToString();
            tbxBuildDate.Text = data.BuildDate.ToString();
            tbxConform.Text = data.Conform.ToString();
            //tbxAvailable.Text = data.Available.ToString();
            tbxLlegalArea.Text = data.LegalArea.ToString();
            tbxIllegaArea.Text = data.IllegaArea.ToString();
            tbxIllegaUnit.Text = data.IllegaUnit.ToString();
            tbxPrice.Text = data.Price.ToString();
            tbxConfiscateFloorArea.Text = data.ConfiscateFloorArea.ToString();
            tbxConfiscateArea.Text = data.ConfiscateArea.ToString();
            tbxConfiscateAreaUnit.Text = data.ConfiscateAreaUnit.ToString();
            tbxConfiscateAreaPrice.Text = data.ConfiscateAreaPrice.ToString();

        }

        private void btnCalculate_Click(object sender, EventArgs e) {
            assigment();
            data = ConfiscateCalculate.getConfiscateData(data);
            tbxConfiscateFloorArea.Text = data.ConfiscateFloorArea.ToString();
            tbxConfiscateArea.Text = data.ConfiscateArea.ToString();
            tbxConfiscateAreaUnit.Text = data.ConfiscateAreaUnit.ToString();
            tbxConfiscateAreaPrice.Text = data.ConfiscateAreaPrice.ToString();
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        private void modifyData(bool isNotConfiscate) {
            CFSJDal db = new CFSJDal(Common.strConnection);



            try {
                StringBuilder sql = new StringBuilder();
                sql.Append("update 鹤城所 set ");
                sql.Append("户主 = @Name , ");
                sql.Append("身份证号= @CardID , ");
                sql.Append("乡镇= @Town , ");
                sql.Append("户口人数= @Accounts , ");
                sql.Append("土地座落= @Location , ");
                sql.Append("控制区= @Control , ");
                sql.Append("土地性质= @LandOwner , ");
                sql.Append("占地面积= @Area , ");
                sql.Append("层数= @Layer , ");
                sql.Append("建成年月= @BuildDate , ");
                sql.Append("土地利用总体规划= @Conform , ");
                sql.Append("建房资格= @Available , ");
                sql.Append("审批面积= @LegalArea , ");
                sql.Append("超建面积= @IllegaArea , ");
                sql.Append("单价= @IllegaUnit , ");
                sql.Append("金额= @Price , ");
                sql.Append("没收占地面积= @ConfiscateFloorArea , ");
                sql.Append("没收建筑面积= @ConfiscateArea , ");
                sql.Append("没收单价= @ConfiscateAreaUnit , ");
                sql.Append("没收金额= @ConfiscateAreaPrice ");
                sql.Append("where ");
                sql.Append("GUID= @Guid ");

                SQLiteParameter[] parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Name",data.Name.ToString()),
                    new SQLiteParameter("@CardID",data.CardID.ToString()),
                    new SQLiteParameter("@Town",data.Town.ToString()),
                    new SQLiteParameter("@Accounts",data.Accounts.ToString()),
                    new SQLiteParameter("@Location",data.Location.ToString()),
                    new SQLiteParameter("@Control",data.Control.ToString()),
                    new SQLiteParameter("@LandOwner",data.LandOwner.ToString()),
                    new SQLiteParameter("@Area",data.Area.ToString()),
                    new SQLiteParameter("@Layer",data.Layer.ToString()),
                    new SQLiteParameter("@BuildDate",data.BuildDate.ToString()),
                    new SQLiteParameter("@Conform",data.Conform.ToString()),
                    new SQLiteParameter("@Available",data.Available.ToString()),
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
                db.ExecuteNonQuery(sql.ToString(), parameters);

                StringBuilder _sql = new StringBuilder();
                _sql.Append(@"select count(*) from 鹤城所没收 where GUID='");
                _sql.Append(data.Guid);
                _sql.Append(@"'");
                string i = "a";
                using (SQLiteDataReader reader = db.ExecuteReader(_sql.ToString(), null)) {
                    while (reader.Read()) {
                        i = reader[0].ToString();
                    }
                };
                if (isNotConfiscate && i.Equals("0")) {//须莫收且没有数据
                    _sql = new StringBuilder();
                    _sql.Append("insert into 鹤城所没收 ");
                    _sql.Append("(GUID) values ");
                    _sql.Append("(@guid)");
                    SQLiteParameter[] pt = new SQLiteParameter[]{
                        new SQLiteParameter("@guid",data.Guid)
                    };
                    db.ExecuteNonQuery(_sql.ToString(), pt);
                } else if (!isNotConfiscate && !i.Equals("0")) {//不须莫收且有数据
                    _sql = new StringBuilder();
                    _sql.Append("delete from 鹤城所没收 ");
                    _sql.Append("where GUID = ");
                    _sql.Append("(@guid)");
                    SQLiteParameter[] pt = new SQLiteParameter[]{
                        new SQLiteParameter("@guid",data.Guid)
                    };
                    db.ExecuteNonQuery(_sql.ToString(), pt);
                }
                MessageBox.Show("数据修改成功");
            } catch (Exception ex) {
                throw ex;
            }
            
        }

        private void btnModify_Click(object sender, EventArgs e) {
            assigment();
            try {
                modifyData(data.ConfiscateAreaPrice > 0);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
                this.Close();
           
        }

        private void assigment() {
            data.Name = tbxName.Text;
            data.CardID = tbxCardID.Text;
            data.Town = tbxTown.Text;
            data.Accounts = tbxAccounts.Text;
            data.Location = tbxLocation.Text;
            data.Control = tbxControl.Text;
            data.LandOwner = tbxLandOwner.Text;
            data.Area =double.Parse( tbxArea.Text);
            data.Layer = double.Parse(tbxLayer.Text);
            data.BuildDate = Int32.Parse(tbxBuildDate.Text);
            data.Conform = tbxConform.Text;
            data.LegalArea = double.Parse(tbxLlegalArea.Text);
            data.IllegaArea = double.Parse(tbxIllegaArea.Text);
            data.IllegaUnit = Int32.Parse(tbxIllegaUnit.Text);
            data.Price = double.Parse(tbxPrice.Text);
            data.ConfiscateFloorArea = double.Parse(tbxConfiscateFloorArea.Text);
            data.ConfiscateArea = double.Parse(tbxConfiscateArea.Text);
            data.ConfiscateAreaUnit = Int32.Parse(tbxConfiscateAreaUnit.Text);
            data.ConfiscateAreaPrice = double.Parse(tbxConfiscateAreaPrice.Text);
        }


    }
}
