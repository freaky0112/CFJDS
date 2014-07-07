using System;
using System.Collections.Generic;
using System.Text;

namespace CFJDS {
    class BiultCFJDS {
        /// <summary>
        /// 初始化应用对象
        /// </summary>
        private string pText = "";// 文本信息   
        private int pFontSize = 24;//字体大小  
        private string pFontName = "宋体";
        private Microsoft.Office.Interop.Word.WdColor pFontColor = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        private Microsoft.Office.Interop.Word.WdUnderline pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;//下划线
        private int pFontBold = 0;//字体粗体  
        Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment = 0;//方向 

        private EcanNum ecanNum = new EcanNum();//数字大写转换
        /// <summary>
        /// 输入处罚决定书
        /// </summary>
        /// <param name="brf"></param>
        public void addCFJDS(BiultReportForm brf, DataCFSJ data) {

            addTitle(brf,data);
            addText(brf, data);
            brf.NewPage();
        }

        /// <summary>
        /// 输入并插入下一行
        /// </summary>
        private void addLine(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
            brf.NewLine();
        }
        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="brf"></param>
        private void addTxt(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
        }
        /// <summary>
        /// 标题
        /// </summary>
        /// <param name="brf"></param>    
        private void addTitle(BiultReportForm brf,DataCFSJ data) {
            pText = "青田县国土资源局";
            pFontSize = 18;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            pFontBold = 1;//设置粗体
            addLine(brf);
            pFontSize = 24;
            pText = "土地违法行为行政处罚决定书";
            addLine(brf);
            pFontSize = 12;
            pFontBold = 0;//设置细体
            pText = "青土资罚〔2014〕第"+data.Code + String.Format("{0:0000}", data.No) + "号";
            addLine(brf);
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
            pText = "                                                       ";
            pFontSize = 15;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustifyHi;
            brf.SetLineSpacing(4f);
            addLine(brf);
        }
        /// <summary>
        /// 输入正文
        /// </summary>
        /// <param name="brf"></param>
        /// <param name="data"></param>
        private void addText(BiultReportForm brf, DataCFSJ data) {
            ptextAlignment = 0;
            pFontSize = 15;
            pFontName = "仿宋_GB2312";
            brf.SetLineSpacing(27f);
            for (int i = 0; i < data.Names.Length; i++) {
                string name = data.Names[i];
                pText = "    被处罚人：" + name + " " + "，性别：";
                pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                addTxt(brf);
                if (data.IDs != null) {
                    if (!string.IsNullOrEmpty(data.IDs[i])) {
                        pText = data.Sex[i];
                    } else {
                        pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                        pText = "    ";
                    }
                } else {
                    pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                    pText = "    ";
                }
                addTxt(brf);//性别
                pText = "，出生于";
                pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                addTxt(brf);
                if (data.IDs != null) {
                    if (!string.IsNullOrEmpty(data.IDs[i])) {
                        pText = data.BirthDate[i].Year + "年" + data.BirthDate[i].Month + "月" + data.BirthDate[i].Day + "日";
                    } else {
                        pText = "    年    月    日";
                        pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                    }
                } else {
                    pText = "    年    月    日";
                    pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                }
                addTxt(brf);//出生年月日

                pText = "，身份证号码：";
                pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                addTxt(brf);
                if (data.IDs != null) {
                    if (!string.IsNullOrEmpty(data.IDs[i])) {
                        pText = data.IDs[i];
                    } else {
                        pText = "                            ";

                        pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                    }
                } else {
                    pText = "                            ";

                    pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                }
                addTxt(brf);//身份证号码
                pText = "，地址：" + data.Town + data.Location.Substring(0, data.Location.IndexOf('村') + 1) + "。";
                pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                addLine(brf);
            }
            pText = "    案    由：非法占用土地。";
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            addLine(brf);

            pText = "    被处罚人未经批准于";
            pText += data.Date.ToString();
            pText += " 年擅自在青田县";
            pText += data.Town;
            pText += data.Location;
            pText += "非法占用土地" + data.Area.ToString() + "平方米，并在该土地上建造建筑物（房屋）。经核对青田县";
            pText += data.Town;
            pText += "土地利用总体规划，该地块符合土地利用总体规划。以上事实有调查摸底登记表、违法建筑照片、违法建筑处置公示清单等证据证实。其行为违反了《中华人民共和国土地管理法》、《浙江省实施〈中华人民共和国土地管理法〉办法》等有关法律法规有关规定。本局依照《青田县人民政府关于印发青田县实施〈浙江省违法建筑处置规定〉细则（暂行）》（青政发〔2014〕62号）有关规定，拟对你的违法行为作如下行政处罚：";
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            addLine(brf);
            pText = "    ";
            addTxt(brf);
            //如果没收
            if (data.IsnotConfiscate) {
                pText = "1.  没收你非法占用 ";
                pText += data.ConfiscateArea.ToString();
                pText += "平方米的建筑物。";
                addLine(brf);
                pText = "    2.  ";
                addTxt(brf);
            }

            pText = "对你非法占用土地的行为处以罚款，罚款额为非法占用其他土地每平方米";
            pText += data.Unit.ToString();
            pText += "元，合计人民币" + ecanNum.CmycurD(data.Price.ToString()) + "（￥" + Math.Round(data.Price, 2) + ")。";
            addLine(brf);
            pText = "    如不服本处罚决定的，可在接到本处罚决定书之日起六十日内向丽水巿国土资源局或青田县人民政府提出行政复议申请，或者在三个月内向人民法院提起行政诉讼，逾期既不申请复议又不提起诉讼，又不履行本处罚决定的，本局将申请人民法院强制执行，费用由被处罚人支付。";
            //处罚总金额大于5W有听证            
            addLine(brf);
            pText = "青田县国土资源局        ";
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            addLine(brf);
            pText = "2014年   月   日        ";
            addLine(brf);
        }
    }
}
