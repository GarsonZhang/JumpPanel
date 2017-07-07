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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }
        //AnchorPanelDex APanel;
        AnchorPanel APanelwinform;

        private void Form3_Load(object sender, EventArgs e)
        {
           // APanel = new AnchorPanelDex(panel1, xtraScrollableControl1);
            APanelwinform = new AnchorPanel(panel10, panel2);

            if (panel9.Height < panel2.Height) panel9.Height = panel2.Height;
            panel10.Controls.Clear();
            
            //绑定子容器
            APanelwinform.AddAnchor(panel3, "标题1", false);
            APanelwinform.AddAnchor(panel4, "标题2", false);
            APanelwinform.AddAnchor(panel5, "标题3", false);
            APanelwinform.AddAnchor(panel6, "标题4", false);
            APanelwinform.AddAnchor(panel7, "标题5", false);
            APanelwinform.AddAnchor(panel8, "标题6", false);
            APanelwinform.AddAnchor(panel9, "标题7", true);
            APanelwinform.CurrentLable = APanelwinform.GetMenu(0);
        }
       

    }


}
