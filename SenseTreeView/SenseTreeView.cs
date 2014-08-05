using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SenseTreeView {
    public partial class SenseTreeView : TreeView {
        #region 控件属性

        //显示字体
        private Font _NodeFont;
        public Font NodeFont {
            get {
                return _NodeFont;
            }
            set {
                _NodeFont = value;
            }
        }

        //选择TreeView TreeNode时的背景色
        private Brush _BackgrountBrush;
        public Brush BackgroundBrush {
            get {
                return _BackgrountBrush;
            }
            set {
                _BackgrountBrush = value;
            }
        }

        //选择TreeView TreeNode时背景色的边框画笔
        private Pen _BackgroundPen;
        public Pen BackgroundPen {
            get {
                return _BackgroundPen;
            }
            set {
                _BackgroundPen = value;
            }
        }

        //TreeView中TreeNode展开时的节点显示图标，
        private Image _NodeExpandedImage;
        public Image NodeExpandedImage {
            get {
                return _NodeExpandedImage;
            }
            set {
                _NodeExpandedImage = value;
            }
        }
        //TreeView中TreeNode合拢时的节点显示图标
        private Image _NodeCollapseImage;
        public Image NodeCollapseImage {
            get {
                return _NodeCollapseImage;
            }
            set {
                _NodeCollapseImage = value;
            }
        }
        //TreeView中TreeNode的节点显示图标的大小
        private Size _NodeImageSize;
        public Size NodeImageSize {
            get {
                return _NodeImageSize;
            }
            set {
                _NodeImageSize = value;
            }
        }

        //节点显示图标离左边界的位置
        private int _NodeOffset;
        public int NodeOffset {
            get {
                return _NodeOffset;
            }
            set {
                _NodeOffset = value;
            }
        }

        #endregion

        #region 构造函数

        public SenseTreeView() {
            //设置窗体Style
            //this.SetStyle(ControlStyles.UserPaint, true);               //支持用户重绘窗体
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);    //在内存中先绘制界面
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);   //双缓冲，防止绘制时抖动
            //this.SetStyle(ControlStyles.DoubleBuffer, true);            //双缓冲，防止绘制时抖动 
            //this.UpdateStyles();

            //不显示树形节点显示连接线
            this.ShowLines = false;

            //设置绘制TreeNode的模式
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            //不显示TreeNode前的“+”和“-”按钮
            this.ShowPlusMinus = false;

            //不支持CheckedBox
            this.CheckBoxes = false;

            //设置TreeNode的行高
            SendMessage(this.Handle, TVM_SETITEMHEIGHT, 20, 0);

            //设置默认BackgroundBrush
            BackgroundBrush = new SolidBrush(Color.FromArgb(90, Color.FromArgb(205, 226, 252)));

            //设置默认BackgroundPen
            BackgroundPen = new Pen(Color.FromArgb(130, 249, 252), 1);

            //设置默认NodeFont
            NodeFont = new Font("宋体", 12, FontStyle.Regular);

            //设置默认节点显示图标及Size
            NodeExpandedImage = null;
            NodeCollapseImage = null;
            NodeImageSize = new Size(18, 18);

            //设置默认节点显示图标便宜位置
            NodeOffset = 5;
        }

        #endregion

        #region 节点绘制函数

        //绘制TreeView树中TreeNode
        protected override void OnDrawNode(DrawTreeNodeEventArgs e) {
            TreeNode tn = e.Node as TreeNode;
            if (tn == null) {
                return;
            }

            //设置Image绘制Rectangle
            Point pt = new Point(tn.Bounds.X + NodeOffset, tn.Bounds.Y);
            Rectangle rt = new Rectangle(pt, NodeImageSize);

            if ((e.State & TreeNodeStates.Selected) != 0) {
                //绘制TreeNode选择后的背景框
                e.Graphics.FillRectangle(BackgroundBrush, 2, tn.Bounds.Y, this.Width - 7, tn.Bounds.Height - 1);

                //绘制TreeNode选择后的边框线条
                e.Graphics.DrawRectangle(BackgroundPen, 1, tn.Bounds.Y, this.Width - 6, tn.Bounds.Height - 1);
            }

            //绘制节点图片
            if (NodeExpandedImage != null && NodeCollapseImage != null) {
                if (tn.Nodes.Count != 0) {
                    if (tn.IsExpanded == true) {
                        e.Graphics.DrawImage(NodeExpandedImage, rt);
                    } else {
                        e.Graphics.DrawImage(NodeCollapseImage, rt);
                    }
                }

                rt.X += 15;
            }

            //绘制节点自身图片                
            if (e.Node.SelectedImageIndex != -1 && this.ImageList != null) {
                rt.X += 5;
                e.Graphics.DrawImage(this.ImageList.Images[e.Node.SelectedImageIndex], rt);
            }

            //绘制节点的文本
            rt.X += 20;
            rt.Y += 1;
            rt.Width = this.Width - rt.X;
            e.Graphics.DrawString(e.Node.Text, NodeFont, Brushes.Black, rt);
        }

        #endregion

        #region 鼠标消息响应函数

        //响应鼠标按下消息
        protected override void OnMouseDown(MouseEventArgs e) {
            TreeNode clickedNode = this.GetNodeAt(e.X, e.Y);

            if (clickedNode != null && NodeBounds(clickedNode).Contains(e.X, e.Y)) {
                this.SelectedNode = clickedNode;
            }
        }

        //响应鼠标双击消息
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            TreeNode clickedNode = this.GetNodeAt(e.X, e.Y);

            if (clickedNode != null && NodeBounds(clickedNode).Contains(e.X, e.Y)) {
                this.SelectedNode = clickedNode;

                //判断节点的状态
                if (clickedNode.Nodes.Count != 0) {
                    if (clickedNode.IsExpanded) {
                        clickedNode.Collapse();
                    } else {
                        clickedNode.Expand();
                    }
                }
            }
        }

        #endregion

        #region 私有函数

        //返回TreeView中TreeNode的整行区域
        private Rectangle NodeBounds(TreeNode node) {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;

            //if (node.Tag != null)
            //{
            //    // Retrieve a Graphics object from the TreeView handle
            //    // and use it to calculate the display width of the tag.
            //    Graphics g = this.CreateGraphics();
            //    int tagWidth = (int)g.MeasureString(node.Tag.ToString(), NodeFont).Width + 6;

            //    // Adjust the node bounds using the calculated value.
            //    bounds.Offset(tagWidth / 2, 0);
            //    bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
            //    g.Dispose();
            //}

            bounds.Width = this.Width;

            return bounds;
        }

        #endregion

        #region 引用函数

        const int TV_FRIST = 0x1100;
        const int TVM_SETITEMHEIGHT = TV_FRIST + 27;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int nMsg, int wParam, int Param);

        #endregion
    }
}
