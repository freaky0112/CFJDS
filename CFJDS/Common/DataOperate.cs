using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;

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
            string id = null; ;
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

        public static ArrayList dataRead(string type, string id) {
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
                    data.IllegaUnit = Int32.Parse(reader[21].ToString());
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
    }


}
