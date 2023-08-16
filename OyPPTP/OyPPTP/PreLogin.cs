using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class PreLogin : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PreLogin()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void iniciar_sesion_Click(object sender, EventArgs e)
        {
            IniciarSesion form = new IniciarSesion();
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreLogin_IniciarSesionClosed);
            this.Hide();
            form.Show();

        }

        private void registrar_usuario_Click(object sender, EventArgs e)
        {
            IndicarTipoUsuario form = new IndicarTipoUsuario();
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreLogin_IndicarTipoUsuarioClosed);
            this.Hide();
            form.Show();
        }

        private void olvide_mi_contrasena_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show( "Se enviará un mail a la dirección de correo electrónico de tu usuario, a través de él podrás restablecer tu contraseña.");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PreLogin_IniciarSesionClosed(object sender, EventArgs e) {
            //MessageBox.Show("PreLogin_IniciarSesionClosed");
            this.Show();
        }

        private void PreLogin_IndicarTipoUsuarioClosed(object sender, EventArgs e)
        {
            //MessageBox.Show("PreLogin_IniciarSesionClosed");
            this.Show();
        }

        private void PreLogin_FormClosed(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                DialogResult result = MessageBox.Show("Se va a cerrar la aplicación", "Cerrando aplicación");
                System.Windows.Forms.Application.Exit();
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PreLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
