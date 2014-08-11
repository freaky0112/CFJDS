using System;
using System.Collections.Generic;
using System.Text;

namespace CFJDS {
    class Common {
        public static string  strConnection = System.IO.Directory.GetCurrentDirectory() + @"\CFSJ.db";//数据目录

        public static string[] updateFirst = {"户主"};//首次升级，不存在表名户主

        public static string[] Titles = { "处罚编号", "户主", "没收编号", "民族", "身份证号", "乡镇", "户口人数", "土地座落", "控制区", "土地性质", "占地面积", "耕地面积", "层数", "建成年月", "审批面积", "超建面积", "单价", "耕地单价", "金额", "没收占地面积", "没收建筑面积", "没收单价", "没收金额", "是否已处罚" };

        public static string code = "";
        /// <summary>
        /// 获取没收表名
        /// </summary>
        /// <param name="code">所名</param>
        /// <returns></returns>
        public static string getConfiscateTable(string code) {
            return code + "没收";
        }

        public static bool IsNumber(string strNumber) {
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

        public static DataCFSJ PriceCalculate(DataCFSJ data) {
            //double price;
            //超建面积等于占地面积减去合法面积
            data.IllegaArea = data.Area - data.LegalArea;
            //超建面积减去耕地面积乘以单价，加上耕地面积乘以耕地单价
            data.Price = (data.IllegaArea - data.FarmArea) * data.IllegaUnit + data.FarmArea * data.FarmUnit;
            return data;
        }
    }
}
