using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class CitarTrabajador : Form
    {
        public CitarTrabajador()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Citacion generada con éxito.");
        }

        public void PrecargaCambioFecha() {
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.comboBox1.Enabled = false;
            this.comboBox1.Visible = false;
            this.button1.Text = "Actualizar";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CitarTrabajador_Load(object sender, EventArgs e)
        {

        }
    }
}
