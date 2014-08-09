using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;
using System.Data.OleDb;

namespace CFJDS {
    class dataOperate {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="data">数据</param>
        public static void dataInsert(DataCFSJ data) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            string id = getID("鹤城所");//判断ID是否有空缺
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into 鹤城所 (");
            sql.Append("ID,");
            sql.Append("户主,");
            sql.Append("身份证号,");
            sql.Append("乡镇,");
            sql.Append("户口人数,");
            sql.Append("土地座落,");
            sql.Append("控制区,");
            sql.Append("土地性质,");
            sql.Append("占地面积,");
            sql.Append("层数,");
            sql.Append("建成年月,");
            sql.Append("土地利用总体规划,");
            sql.Append("建房资格,");
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
            sql.Append("@ID,");
            sql.Append("@Name,");
            sql.Append("@CardID,");
            sql.Append("@Town,");
            sql.Append("@Accounts,");
            sql.Append("@Location,");
            sql.Append("@Control,");
            sql.Append("@LandOwner,");
            sql.Append("@Area,");
            sql.Append("@Layer,");
            sql.Append("@BuildDate,");
            sql.Append("@Conform,");
            sql.Append("@Available,");
            sql.Append("@LegalArea,");
            sql.Append("@IllegaArea,");
            sql.Append("@IllegaUnit,");
            sql.Append("@Price,");
            sql.Append("@ConfiscateFloorArea,");
            sql.Append("@ConfiscateArea,");
            sql.Append("@ConfiscateAreaUnit,");
            sql.Append("@ConfiscateAreaPrice,");
            sql.Append("@Guid)");


            SQLiteParameter[] parameters = new SQLiteParameter[]{
                    new SQLiteParameter("ID",id),
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
            if (data.ConfiscateAreaPrice != 0) {
                string _ID = getID("鹤城所没收");
                StringBuilder _sql = new StringBuilder();
                _sql.Append("insert into 鹤城所没收 ");
                _sql.Append("(ID,GUID) values ");
                _sql.Append("(@id,@guid)");
                SQLiteParameter[] pt = new SQLiteParameter[]{                    
                        new SQLiteParameter("@id",_ID),
                        new SQLiteParameter("@guid",data.Guid)
                    };
                db.ExecuteNonQuery(_sql.ToString(), pt);
            }
            db.ExecuteNonQuery(sql.ToString(), parameters);
        }
        /// <summary>
        /// 获取空缺ID
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>返回空缺ID，返回值为Null即向后新增</returns>
        private static string getID(string table) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            StringBuilder sqlMax = new StringBuilder();
            string id = null;
            ;
            sqlMax.Append("select min(ID-1) from ");
            sqlMax.Append(table);
            sqlMax.Append(" where ID not in(select 1+id from ");
            sqlMax.Append(table);
            sqlMax.Append(") and id not in (select min(id) from ");
            sqlMax.Append(table);
            sqlMax.Append(")");
            using (SQLiteDataReader reader = db.ExecuteReader(sqlMax.ToString(), null)) {
                while (reader.Read()) {
                    if (!string.IsNullOrEmpty(reader[0].ToString())) {
                        id = reader[0].ToString();
                    }
                }
            }
            return id;
        }

        public static ArrayList dataRead_Old(string type, string id) {
            ArrayList dataList = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.Append("select 鹤城所.*, 鹤城所没收.ID from  鹤城所 left join 鹤城所没收 on 鹤城所.GUID=鹤城所没收.GUID ");
            if (!string.IsNullOrEmpty(id)) {
                sql.Append("where ");
                switch (type) {
                    case "编号：":
                        if (id.Split('-').Length == 1) {
                            sql.Append("鹤城所.id = ");
                            sql.Append(id);
                        } else {
                            string a = id.Split('-')[0];
                            string b = id.Split('-')[1];
                            //sql.Append("");
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
                        break;
                    case "户主：":
                        sql.Append("户主 like '%");
                        sql.Append(id);
                        sql.Append("%'");
                        break;
                    case "土地坐落：":
                        sql.Append("土地座落 like '%");
                        sql.Append(id);
                        sql.Append("%'");
                        break;
                }
            }
            CFSJDal db = new CFSJDal(Common.strConnection);
            using (SQLiteDataReader reader = db.ExecuteReader(sql.ToString(), null)) {
                while (reader.Read()) {
                    DataCFSJ data = new DataCFSJ();
                    data.ID = Int32.Parse(reader[0].ToString());
                    data.Name = reader[1].ToString();
                    data.CardID = reader[3].ToString();
                    data.Town = reader[4].ToString();
                    data.Accounts = reader[6].ToString();
                    data.Location = reader[8].ToString();
                    data.Control = reader[9].ToString();
                    data.LandOwner = reader[10].ToString();
                    data.Area = double.Parse(reader[11].ToString());

                    data.Layer = double.Parse(reader[14].ToString());
                    data.BuildDate = Int32.Parse(reader[15].ToString());
                    data.Conform = reader[16].ToString();
                    data.Available = reader[17].ToString();
                    data.LegalArea = double.Parse(reader[18].ToString());
                    data.IllegaArea = double.Parse(reader[20].ToString());
                    data.IllegaUnit = double.Parse(reader[21].ToString());
                    //耕地面积
                    if (!string.IsNullOrEmpty(reader[12].ToString())) {
                        data.FarmArea = double.Parse(reader[12].ToString());
                        data.FarmUnit = Int32.Parse(reader[22].ToString());
                    }
                    data.Price = double.Parse(reader[23].ToString());
                    data.ConfiscateFloorArea = double.Parse(reader[24].ToString());
                    data.ConfiscateArea = double.Parse(reader[25].ToString());
                    data.ConfiscateAreaUnit = Int32.Parse(reader[26].ToString());
                    data.ConfiscateAreaPrice = double.Parse(reader[27].ToString());
                    if (!string.IsNullOrEmpty(reader[28].ToString())) {
                        data.Signed = (Boolean)reader[28];
                    }
                    data.Guid = reader[29].ToString();
                    data.CardIDs = data.CardID.Split('、');
                    if (!string.IsNullOrEmpty(reader[30].ToString())) {
                        data.ConfiscateID = Int32.Parse(reader[30].ToString());
                    }
                    dataList.Add(data);
                }

            };
            return dataList;
        }

        public static void dataDelte(DataCFSJ data, string table) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            SQLiteParameter[] pt = new SQLiteParameter[]{
                new SQLiteParameter("@GUID",data.Guid)
            };
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ");
            sql.Append(table);
            sql.Append(" where GUID = @GUID");
            db.ExecuteNonQuery(sql.ToString(), pt);
        }

        public static void dataSigned(DataCFSJ data, Boolean signed) {
            try {
                data.Signed = signed;
                CFSJDal db = new CFSJDal(Common.strConnection);
                //int Signed = 0;
                //if (data.Signed) {
                //    Signed = 1;
                //}
                SQLiteParameter[] pt = new SQLiteParameter[]{
                new SQLiteParameter("@GUID",data.Guid),
                new SQLiteParameter("@Signed",data.Signed)
            };
                StringBuilder sql = new StringBuilder();
                sql.Append("update 鹤城所 set ");
                sql.Append("是否已处罚=@Signed  ");
                sql.Append("where ");
                sql.Append("GUID= @Guid ");
                db.ExecuteNonQuery(sql.ToString(), pt);
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static Boolean getTableExist(string tableName) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) FROM sqlite_master where type='table' and name=@tablename");
            SQLiteParameter[] pt = new SQLiteParameter[]{
                new SQLiteParameter("@tablename",tableName)
            };
            int isExist = 0;

            isExist = Int32.Parse(db.ExecuteScalar(sql.ToString(), pt).ToString());
            if (isExist == 0) {
                return false;
            } else {
                return true;
            }
        }
        /// <summary>
        /// 获取当前数据库所有表名
        /// </summary>
        /// <returns></returns>
        public static ArrayList getTables() {
            ArrayList tables = new ArrayList();
            CFSJDal db = new CFSJDal(Common.strConnection);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT name FROM sqlite_master ");
            sql.Append("WHERE type='table' ");
            sql.Append("ORDER BY name;");
            using (SQLiteDataReader reader = db.ExecuteReader(sql.ToString(), null)) {
                while (reader.Read()) {
                    if (reader[0].ToString() != "sqlite_sequence") {
                        tables.Add(reader[0].ToString());
                    }
                }
            }
            return tables;
        }
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        public static void dropTable(string tableName) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            StringBuilder sql = new StringBuilder();
            sql.Append("DROP TABLE ");
            sql.Append(tableName);
            SQLiteParameter[] pt=new SQLiteParameter[]{
                new SQLiteParameter("@tablename",tableName)
            };
            db.ExecuteNonQuery(sql.ToString(), null);
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="talbeName"></param>
        /// <param name="sql"></param>
        public static void craetTable(string talbeName, string sql) {
            CFSJDal db = new CFSJDal(Common.strConnection);
            db.ExecuteNonQuery(sql.ToString(), null);
        }

        public static void dataExport(ArrayList dataList, string savePath) {
            String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0;";

            OleDbConnection cn = new OleDbConnection(sConnectionString);
            StringBuilder sqlCreate = new StringBuilder();
            sqlCreate.Append("CREATE TABLE 导出数据 (");
            foreach(string title in Common.Titles){
                sqlCreate.Append("[");
                sqlCreate.Append(title);
                sqlCreate.Append("] TEXT,");
            }
            sqlCreate=new StringBuilder(sqlCreate.ToString().TrimEnd(','));
            sqlCreate.Append(")");
            OleDbCommand cmd = new OleDbCommand(sqlCreate.ToString(), cn);
            try {
                cn.Open();
                try {

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw ex;
                }
                foreach (DataCFSJ data in dataList) {
                    StringBuilder sqlInsert = new StringBuilder();
                    sqlInsert.Append(@"INSERT INTO 导出数据 VALUES('");
                    sqlInsert.Append(data.ID);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Name);
                    sqlInsert.Append(@"','");
                    if (data.ConfiscateID > 0) {
                        sqlInsert.Append(data.ConfiscateID);
                    } else {
                        sqlInsert.Append("");
                    }
                    sqlInsert.Append(@"','");
                    sqlInsert.Append("");
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.CardID);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Town);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Accounts);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Location);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Control);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.LandOwner);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Area);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.FarmArea);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Layer);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.BuildDate);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.LegalArea);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.IllegaArea);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.IllegaUnit);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.FarmUnit);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.Price);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.ConfiscateFloorArea);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.ConfiscateArea);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.ConfiscateAreaUnit);
                    sqlInsert.Append(@"','");
                    sqlInsert.Append(data.ConfiscateAreaPrice);
                    sqlInsert.Append(@"','");
                    if (data.Signed) {
                        sqlInsert.Append("√");
                    }
                    sqlInsert.Append("')");
                    try {

                        cmd.CommandText = sqlInsert.ToString();
                        cmd.ExecuteNonQuery();
                    } catch (Exception ex) {
                        throw ex;
                    }
                }
            } catch (Exception ex) {
                throw ex;
            } finally {
                cn.Close();
            }
        }
    }


}
