using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class PreBitacora : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PreBitacora()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            this.Hide();
            bitacora.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreBitacora_BitacoraClosed);
            bitacora.Show();
        }

        private void PreBitacora_BitacoraClosed(object sender, EventArgs e) {
            this.Show();
        }
    }
}
