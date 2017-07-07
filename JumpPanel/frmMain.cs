using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JumpPanel
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }

        AnchorPanel APanelwinform;

        private void Form3_Load(object sender, EventArgs e)
        {
            //初始化容器，
            APanelwinform = new AnchorPanel(panLeft, panContent);
            //初始化容器，子内容页面的标题用tag内容来表示
            APanelwinform.LoadContent();
            
            //设置当前子面板
            APanelwinform.CurrentLable = APanelwinform.GetMenu(0);
        }


    }


}
