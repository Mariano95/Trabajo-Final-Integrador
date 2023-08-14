using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class BuscarTrabajadores : Form
    {
        public BuscarTrabajadores()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BuscarTrabajadores_Load(object sender, EventArgs e)
        {
            this.serviciosCombo.Items.Add("Servicio 1");
            this.serviciosCombo.Items.Add("Servicio 2");
            this.serviciosCombo.Items.Add("Servicio 3");
            this.serviciosCombo.Items.Add("Servicio 4");
            this.serviciosCombo.Items.Add("Servicio 5");
        }

        private void buscarTrabajadoresBtn_Click(object sender, EventArgs e)
        {
            GrillaTrabajadores form = new GrillaTrabajadores();
            form.Show();
        }
    }
}
