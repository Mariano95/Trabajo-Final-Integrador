using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class IndicarTipoUsuario : Form
    {
        public IndicarTipoUsuario()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CargarServicios form = new CargarServicios();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarUsuario form = new RegistrarUsuario();
            form.Show();
        }
    }
}
