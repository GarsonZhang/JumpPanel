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
            APanelwinform = new AnchorPanel(panLeft, panContent);

            if (panPage7.Height < panContent.Height) panPage7.Height = panContent.Height;
            panLeft.Controls.Clear();
            
            //绑定子容器
            APanelwinform.AddAnchor(panPage1, "标题1", false);
            APanelwinform.AddAnchor(panPage2, "标题2", false);
            APanelwinform.AddAnchor(panPage3, "标题3", false);
            APanelwinform.AddAnchor(panPage4, "标题4", false);
            APanelwinform.AddAnchor(panPage5, "标题5", false);
            APanelwinform.AddAnchor(panPage6, "标题6", false);
            APanelwinform.AddAnchor(panPage7, "标题7", true);
            APanelwinform.CurrentLable = APanelwinform.GetMenu(0);
        }
       

    }


}
