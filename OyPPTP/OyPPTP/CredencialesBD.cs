using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class CredencialesBD : Form
    {
        public CredencialesBD()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PreLogin form1 = new PreLogin();
            form1.Show();
        }

        private void CredencialesBD_Load(object sender, EventArgs e)
        {
        }
    }
}
