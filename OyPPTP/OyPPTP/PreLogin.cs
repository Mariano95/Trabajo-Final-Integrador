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
        public PreLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniciarSesion form = new IniciarSesion();
            form.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IndicarTipoUsuario form = new IndicarTipoUsuario();
            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show( "Se enviará un mail a la dirección de correo electrónico de tu usuario, a través de él podrás restablecer tu contraseña.");
        }
    }
}
