using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class frmInstructions : Form
    {
        public frmInstructions()
        {
            InitializeComponent();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            //stops the timer and close the form
            this.closeTimer.Stop();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }
        private void frmInstructions_Load(object sender, EventArgs e)
        {

        }
    }
}
