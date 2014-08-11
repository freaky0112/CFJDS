using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace CFJDS {
    class update {

        public static void updateSqlite(string code) {
            Boolean isExist = dataOperate.getTableExist(Common.updateFirst[0]);//判断表是否存在
            if (!isExist) {//如果该表不存在
                firstUpdate(code);
            }
        }

        private static void firstUpdate(string code) {
            ArrayList datalist = new ArrayList();
            ArrayList tables = dataOperate.getTables();//获取现有表名
            if (tables.Count != 0) {
                datalist = dataOperate.dataRead_Old("ID", "");//提取数据
                dropTables(tables);//清空所有表
            }
            firstTableCreate(code);
        }
        /// <summary>
        /// 删除现有表
        /// </summary>
        /// <param name="tables">表名集合</param>
        private static void dropTables(ArrayList tables) {
            foreach (string tableName in tables) {
                dataOperate.dropTable(tableName);
            }
        }

        private static void firstTableCreate(string code) {
            //初始化
            StringBuilder sql = new StringBuilder();
            string tableName;
            //XXX所表
            tableName = code;//所名
            sql.Append("CREATE TABLE ");
            sql.Append(tableName);
            sql.Append("(               ");
            sql.Append("    ID       INTEGER PRIMARY KEY ASC");
            sql.Append("                     NOT NULL       ");
            sql.Append("                     UNIQUE,        ");
            sql.Append("    乡镇       TEXT,                ");
            sql.Append("    住址       TEXT,                ");
            sql.Append("    户口人数     TEXT,              ");
            sql.Append("    一户一宅     TEXT,              ");
            sql.Append("    土地座落     TEXT,              ");
            sql.Append("    控制区      TEXT,               ");
            sql.Append("    土地性质     TEXT,              ");
            sql.Append("    层数       TEXT,                ");
            sql.Append("    单层5米2    BOOLEAN,            ");
            sql.Append("    总高度22米   BOOLEAN,           ");
            sql.Append("    八层以上面积   TEXT,            ");
            sql.Append("    建成年月     TEXT,              ");
            sql.Append("    土地利用总体规划 BOOLEAN,       ");
            sql.Append("    建房资格     TEXT,              ");
            sql.Append("    是否已处罚    BOOLEAN,          ");
            sql.Append("    备注       TEXT,                ");
            sql.Append("    GUID     TEXT    UNIQUE         ");
            sql.Append(");                                  ");
            dataOperate.craetTable(tableName, sql.ToString());
            //没收表名
            tableName = Common.getConfiscateTable(code);
            sql = new StringBuilder();
            sql.Append("CREATE TABLE ");
            sql.Append(tableName);
            sql.Append("(               ");
            sql.Append("    ID     INTEGER PRIMARY KEY ASC AUTOINCREMENT,");
            sql.Append("    没收占地面积 TEXT,                           ");
            sql.Append("    没收建筑面积 TEXT,                           ");
            sql.Append("    没收单价   TEXT,                             ");
            sql.Append("    没收金额   TEXT,                             ");
            sql.Append("    GUID   TEXT    NOT NULL                      ");
            sql.Append("                   UNIQUE                        ");
            sql.Append(");                                               ");
            dataOperate.craetTable(tableName, sql.ToString());
            //户主表
            tableName = "户主";
            sql = new StringBuilder();
            sql.Append("CREATE TABLE ");
            sql.Append(tableName);
            sql.Append("(               ");
            sql.Append("    姓名   TEXT,      ");
            sql.Append("    民族   TEXT,      ");
            sql.Append("    身份证号 TEXT,    ");
            sql.Append("    性别   TEXT,      ");
            sql.Append("    出生日期 TEXT,    ");
            sql.Append("    一户一宅 BOOLEAN, ");
            sql.Append("    GUID TEXT NOT NULL");
            sql.Append(");                    ");
            dataOperate.craetTable(tableName, sql.ToString());
            //土地违法数据表
            tableName = "土地违法";
            sql = new StringBuilder();
            sql.Append("CREATE TABLE ");
            sql.Append(tableName);
            sql.Append("(               ");
            sql.Append("    占地面积 TEXT,     ");
            sql.Append("    审批面积 TEXT,     ");
            sql.Append("    审批时间 TEXT,     ");
            sql.Append("    超建面积 TEXT,     ");
            sql.Append("    单价   TEXT,       ");
            sql.Append("    耕地面积 TEXT,     ");
            sql.Append("    耕地单价 TEXT,     ");
            sql.Append("    金额   TEXT,       ");
            sql.Append("    GUID TEXT NOT NULL ");
            sql.Append("              UNIQUE   ");
            sql.Append(");                     ");
            dataOperate.craetTable(tableName, sql.ToString());
        }
    }
}
