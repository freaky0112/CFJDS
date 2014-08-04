using System;
using System.Collections.Generic;
using System.Text;

namespace CFJDS {
    /// <summary>
    /// 没收计算
    /// </summary>
    class ConfiscateCalculate {

        public static DataCFSJ getConfiscateData(DataCFSJ data) {

            if (isNotConfiscate(data)) {
                data.QuotaArea = getQuotaArea(data);//没收限额面积
                data.ConfiscateFloorArea = getConfiscateFloorArea(data);//没收占地面积
                data.ConfiscateArea = getConfiscateArea(data);//没收面积
                data.ConfiscateAreaUnit = getConfiscateAreaUnit(data);//没收单价
                data.ConfiscateAreaPrice = getConfiscateAreaPrice(data);//没收金额
            } else {
                data.ConfiscateFloorArea = 0;//没收占地面积
                data.ConfiscateArea = 0;//没收面积
                data.ConfiscateAreaUnit = 0;//没收单价
                data.ConfiscateAreaPrice = 0;//没收金额
            }

            return data;
        }
        /// <summary>
        /// 获取没收金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateAreaPrice(DataCFSJ data) {
            double confiscateAreaPrice = data.ConfiscateArea * data.ConfiscateAreaUnit;
            return confiscateAreaPrice;
        }

        /// <summary>
        /// 是否没收
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool isNotConfiscate(DataCFSJ data) {
            string timeandowner = "";
            if (data.BuildDate < 198700) {
                return false;
            } else if (data.BuildDate < 199900) {
                timeandowner = "99年以前" + data.LandOwner;
            } else if (data.BuildDate < 201304) {
                timeandowner = "99年以后" + data.LandOwner;
            }

            switch (timeandowner) {
                case "99年以前国有":
                    return getConfiscateFloorArea(data) > 20;
                case "99年以后国有":
                    return getConfiscateFloorArea(data) > 10;
                case "99年以前集体":
                    return getConfiscateFloorArea(data) > 45;
                case "99年以后集体":
                    return getConfiscateFloorArea(data) > 20;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 获取没收面积
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateArea(DataCFSJ data) {
            double confiscateArea = new double();
            if (data.Layer <= 7) {
                //高度小于7层时
                confiscateArea = data.ConfiscateFloorArea * data.Layer;
            } else {
                confiscateArea = data.ConfiscateFloorArea * 7;
            }
            return confiscateArea;
        }


        /// <summary>
        /// 或取超限额占地面积
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateFloorArea(DataCFSJ data) {
            double confiscateFloorArea=data.Area-getBigNumber(data.LegalArea,getQuotaArea(data));
            return confiscateFloorArea;
        }
        /// <summary>
        /// 获取审批限额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int getQuotaArea(DataCFSJ data) {
            int quotaArea = 0;
            foreach (string account in data.Accounts.Split('+')) {
                int member = 0;
                if (!string.IsNullOrEmpty(account)) {              
                    member = Int32.Parse(account);
                }
                if (data.Control.Equals("四级")) {
                    if (member <= 3) {//四级控制区人数1-3为90平方米
                        quotaArea += 90;
                    } else if (member > 5) {//四级控制区人数1-3为120平方米
                        quotaArea += 120;
                    } else {//四级控制区人数4-5为90平方米
                        quotaArea += 100;
                    }
                } else {//其余均为90平方米
                    quotaArea += 90;
                }
            }
            return quotaArea;

        }
        /// <summary>
        /// 获取没收单价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int getConfiscateAreaUnit(DataCFSJ data) {
            int ConfiscateAreaUnit=0;
            int[,] price = {{800,1200,600,900,400,600,200,300,80,120,40,60,0,0},{1200,1800,800,1200,600,900,400,600,150,225,70,105,40,60},{1500,2250,1000,1500,800,1200,600,900,200,300,100,150,50,75}};
            string orign = data.Control+data.LandOwner;
            int a = 0; int b = 12;
            if (data.BuildDate < 199900) {
                b = 0;
            } else if (data.BuildDate < 201010) {
                b = 1;
            } else {
                b = 2;
            }

            if (data.Control.Equals("一级I类")) {
                a = 0;
            } else if (data.Control.Equals("一级II类")) {
                a = 2;
            } else if (data.Control.Equals("一级III类")) {
                a = 4;
            } else if (data.Control.Equals("二级")) {
                a = 6;
            } else if (data.Control.Equals("三级非集镇")) {
                a = 8;
            } else if (data.Control.Equals("三级集镇")) {
                a = 10;
            } else {
                a = 12;
            }
            if (data.LandOwner.Equals("国有")) {
                a = a + 1;
            }
            ConfiscateAreaUnit = price[b, a];
            return ConfiscateAreaUnit;

        }

        /// <summary>
        /// 比较a,b两数大小返回大值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double getBigNumber(double a, double b) {
            if (a > b) {
                return a;
            } else {
                return b;
            }
        }
    }
}
