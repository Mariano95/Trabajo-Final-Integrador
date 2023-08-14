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
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public IndicarTipoUsuario()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e)
        {
            CargarServicios form = new CargarServicios();
            this.Hide();
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndicarTipoUsuario_CargarServiciosClosed);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarUsuario form = new RegistrarUsuario();
            this.Hide();
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndicarTipoUsuario_RegistrarUsuarioClosed);
            form.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void IndicarTipoUsuario_CargarServiciosClosed(object sender, EventArgs e) {
            this.Show();
        }

        private void IndicarTipoUsuario_RegistrarUsuarioClosed(object sender, EventArgs e)
        {
            this.Show();
        }


        private void IndicarTipoUsuario_FormClosed(object sender, EventArgs e) { 
            
        }
    }
}
