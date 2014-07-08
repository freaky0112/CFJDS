using System;
using System.Collections.Generic;
using System.Text;

namespace CFJDS {
    class BiultIndex {
        private string pText = "";// 文本信息   
        private int pFontSize = 24;//字体大小  
        private string pFontName = "宋体";
        private Microsoft.Office.Interop.Word.WdColor pFontColor = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        private Microsoft.Office.Interop.Word.WdUnderline pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;//下划线
        private int pFontBold = 0;//字体粗体  
        Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment = 0;//方向 

        public void addIndex(BiultReportForm brf, DataCFSJ data) {

            addTitle(brf, data);
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
        private  void addTitle(BiultReportForm brf, DataCFSJ data) {
            pText = "";
            pFontSize = 42;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            addLine(brf);
            addLine(brf);            
            pText = "青田县国土资源局";
            
            pFontBold = 1;//设置粗体
            addLine(brf);
            pFontSize = 24;
            pText = "青土资告字〔2014〕第" + data.Code + String.Format("{0:0000}", data.ID) + "号";
            addLine(brf);
            pText = "青土资罚〔2014〕" + data.Code + String.Format("{0:0000}", data.ID) + "号";
            addLine(brf);
            pText = data.Name;
            addLine(brf);
        }
    }
}
