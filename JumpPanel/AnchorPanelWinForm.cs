
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JumpPanel
{

    public class AnchorPanel
    {
        List<PanelMenu> lst = new List<PanelMenu>();

        Control MenuPan { get; set; }

        ScrollableControl XSControl;

        /// <summary>
        /// 构造函数,别忘了调用LoadContent
        /// </summary>
        /// <param name="PanMenu">导航容器</param>
        /// <param name="xtraScrollableControl">面板容器，子面板必须也是容器，并且Dock为TOP</param>
        public AnchorPanel(Panel PanMenu, ScrollableControl xtraScrollableControl)
        {

            MenuPan = PanMenu;
            XSControl = xtraScrollableControl;
            XSControl.Scroll += XSControl_Scroll;
            XSControl.MouseWheel += XSControl_MouseWheel;
            XSControl.SizeChanged += XSControl_SizeChanged;
            XSControl.VerticalScroll.LargeChange = 20;
        }


        void XSControl_SizeChanged(object sender, EventArgs e)
        {
            if (LastAnchor != null && LastAnchorIniHeight < (sender as Control).Height)
            {
                LastAnchor.AnchorContainer.Height = (sender as Control).Height;
            }
        }


        #region 容器滚动条移动事件
        void XSControl_MouseWheel(object sender, MouseEventArgs e)
        {

            XSControl_Scroll(sender, null);
        }

        void XSControl_Scroll(object sender, ScrollEventArgs e)
        {
            CurrentLable = GetMenu((sender as ScrollableControl).VerticalScroll.Value);
        }
        #endregion

        #region 添加锚点

        PanelMenu LastAnchor;
        int LastAnchorIniHeight;


        /// <summary>
        /// 添加锚点,追加再顶部
        /// </summary>
        /// <param name="col">默认为控件的Top，Height属性</param>
        /// <param name="Caption">如果Caption为空则取Col的Text属性</param>
        /// <param name="LastControl">是否是最后一一个锚点，为了保证最后一个锚点定位在顶部，需要动态设置最后一个锚点的高度，如果最后一个锚点区域高度小于容器高度，则设置其高度为容器高度</param>
        private void AddAnchorTop(Control col, string Caption, bool LastControl)
        {
            Control lbl = generateNav(col, Caption, LastControl);

            MenuPan.Controls.Add(lbl);

        }

        private Control generateNav(Control col, string Caption, bool LastControl)
        {
            Label lbl = new Label()
            {
                AutoSize = false,
                Dock = System.Windows.Forms.DockStyle.Top,
                Location = new System.Drawing.Point(0, 0),
                /*lbl.Size = new System.Drawing.Size(219, 37);*/
                Height = 37,
                TabIndex = 0,
                Text = Caption,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Tag = col.Top.ToString()
            };

            IniEventLable(lbl);
            if (LastControl)
            {
                LastAnchor = new PanelMenu(lbl, col);
                LastAnchorIniHeight = col.Height;
                lst.Add(LastAnchor);
            }
            else
                lst.Add(new PanelMenu(lbl, col));
            return lbl;
        }

        /// <summary>
        /// 添加锚点,追加再顶部
        /// </summary>
        /// <param name="col">默认为控件的Top，Height属性</param>
        /// <param name="Caption">如果Caption为空则取Col的Text属性</param>
        /// <param name="LastControl">是否是最后一一个锚点，为了保证最后一个锚点定位在顶部，需要动态设置最后一个锚点的高度，如果最后一个锚点区域高度小于容器高度，则设置其高度为容器高度</param>
        private void AddAnchorButtom(Control col, string Caption, bool LastControl)
        {

            Control lbl = generateNav(col, Caption, LastControl);

            MenuPan.Controls.Add(lbl);
            MenuPan.Controls.SetChildIndex(lbl, 0);
        }


        /// <summary>
        /// 初始化夹在容器数据
        /// </summary>
        public void LoadContent()
        {
            if (XSControl.Controls[0].Height < XSControl.Height)
            {
                XSControl.Controls[0].Height = XSControl.Height;
            }
            //Dock==Top  index是倒序    
            foreach (Control col in XSControl.Controls)
            {
                if (XSControl.Controls.IndexOf(col) == 0)
                    this.AddAnchorTop(col, col.Tag + "", true);
                else
                    this.AddAnchorTop(col, col.Tag + "", false);
            }
        }

        #endregion

        /// <summary>
        /// 根据滚动条位置获得对应的锚点空间
        /// </summary>
        /// <param name="ScrollValue">滚动条的值</param>
        /// <returns></returns>
        public Label GetMenu(int ScrollValue)
        {
            Label lbl = null;
            foreach (PanelMenu menu in lst)
            {
                if (menu.Top <= ScrollValue && menu.Buttom > ScrollValue)
                    lbl = menu.Label;
            }
            if (lbl == null)
            {
                return null;
            }
            return lbl;
        }

        /// <summary>
        /// 初始化锚点的事件
        /// </summary>
        /// <param name="lbl"></param>
        void IniEventLable(Label lbl)
        {
            lbl.MouseEnter += lbl_MouseEnter;
            lbl.MouseLeave += lbl_MouseLeave;

            lbl.MouseClick += lbl_MouseClick;
        }

        #region 锚点单击
        Label _CurrentLable;
        public Label CurrentLable
        {
            set
            {
                if (value == null) return;
                if (_CurrentLable == value) return;
                value.BackColor = Color.LightPink;
                if (_CurrentLable != null)
                    _CurrentLable.BackColor = Color.Transparent;
                _CurrentLable = value;
            }
            get { return _CurrentLable; } //{ return CurrentLable; }
        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                CurrentLable = sender as Label;

                XSControl.VerticalScroll.Value = int.Parse((sender as Label).Tag.ToString()) - CurrentLable.Top;
            }
        }

        /// <summary>
        /// 设置鼠标进入时背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            if ((sender as Label) != CurrentLable)
                (sender as Label).BackColor = Color.FromArgb(0xFF, 0xFF, 0x99);
        }

        /// <summary>
        /// 鼠标移出，还原背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            if ((sender as Label) != CurrentLable)
                (sender as Label).BackColor = Color.Transparent;
        }
        #endregion
    }

    public class PanelMenu
    {
        public PanelMenu(Label label, Control anchorContainer)
        {
            Label = label;
            AnchorContainer = anchorContainer;
            Top = anchorContainer.Top;
        }

        public PanelMenu(Label label, int top, int height)
        {
            Label = label;
            Top = top;
            Height = height;
        }

        /// <summary>
        /// 锚点定位的容器对象，通常是Panel
        /// </summary>
        public Control AnchorContainer { get; set; }
        /// <summary>
        /// 锚点，Lable
        /// </summary>
        public Label Label { get; set; }


        public int Top
        {
            get;
            set;
        }
        private int _height;
        public int Height
        {
            get
            {
                if (AnchorContainer != null)
                    return AnchorContainer.Height;
                else
                    return _height;
            }
            set { _height = value; }
        }

        public int Buttom { get { return Top + Height; } }
    }
}
