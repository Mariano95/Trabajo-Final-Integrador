using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class ProbandoBD : Form
    {
        public ProbandoBD()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ProbandoBD_Load(object sender, EventArgs e)
        {
            

        }

        private void ProbandoBD_Shown(object sender, EventArgs e) 
        {
            System.Threading.Thread.Sleep(4000);
            this.Hide();
            CredencialesBD form1 = new CredencialesBD();
            form1.Show();

        }

    }
}
