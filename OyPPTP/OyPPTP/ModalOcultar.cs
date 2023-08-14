using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class ModalOcultar : Form
    {
        public ModalOcultar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario actualizado con éxito");
            this.Close();
        }

        public void PrecargarReactivacion() {
            this.label1.Text = "Tu usuario aparecerá nuevamente en búsquedas, continuar?";
        }
    }
}
