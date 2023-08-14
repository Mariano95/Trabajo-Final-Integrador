using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class IniciarSesion : Form
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void registrarUsuarioLnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrarUsuario form = new RegistrarUsuario();
            form.Show();
        }

        private void iniciarSesionBtn_Click(object sender, EventArgs e)
        {
            PantallaInicial form = new PantallaInicial();
            form.Show();
        }

        private void IniciarSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
