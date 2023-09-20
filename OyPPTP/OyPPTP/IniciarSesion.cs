using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
using SL;

namespace OyPPTP
{
    public partial class IniciarSesion : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public IniciarSesion()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void IniciarSesion_Load(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        //private void registrarUsuarioLnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    RegistrarUsuario form = new RegistrarUsuario();
        //    this.Hide();
        //    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IniciarSesion_RegistrarUsuarioClosed);
        //    form.Show();
        //}

        private void iniciar_sesion_Click(object sender, EventArgs e)
        {
            string mail = this.iniciar_sesion_mail_text.Text;
            string password = this.iniciar_sesion_contrasena_text.Text;

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string mailEncrypted = miDAL.EncriptarAES(mail);
            string passwordEncrypted = miDAL.EncriptarMD5(password);

            int idUsuario = BLL.UsuarioBLL.BuscarUsuarioPorMail(mailEncrypted);
            if (idUsuario == -1) {
                MessageBox.Show("La información de la dirección de correo electrónico o la contraseña no es válida.");
                return;
            }

            bool usuarioBloqueado = BLL.UsuarioBLL.ValidarUsuarioBloqueado(idUsuario);
            if (usuarioBloqueado) {
                MessageBox.Show("Tu usuario se encuentra bloqueado. Por favor, contactate con un administrador.");
                return;
            }

            bool passwordCorrecto = BLL.UsuarioBLL.ValidarPasswordUsuario(idUsuario, passwordEncrypted);
            if (!passwordCorrecto) {
                MessageBox.Show("La información de la dirección de correo electrónico o la contraseña no es válida");
                return;
            }

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(1, idUsuario);

            //Aca validamos los digitos verificadores
            bool integridadok = true;
            List<(string, int)> res = miDAL.SumaVerificadoresHorizontalesPorTabla();
            foreach ((string, int) t in res) {
                MessageBox.Show(t.Item1.ToString()+"-"+t.Item2.ToString());
                bool integridadtablaok = miDAL.CoincideVerificadorVertical(t.Item2, t.Item1);
                if (!integridadtablaok)
                {
                    gestorBitacora.RegistrarEvento(8, idUsuario);
                    integridadok = false;
                    MessageBox.Show("Fallo de integridad en la tabla " + t.Item1.ToString());
                }
            }
            if (integridadok) {
                gestorBitacora.RegistrarEvento(9, idUsuario);
            }

            //Aca recuperamos las patentes del usuario
            MessageBox.Show(this.Name);
            GestorPatentes gestorPatentes = new GestorPatentes();
            List<string> patentes = gestorPatentes.ObtenerPatentes(idUsuario, this.Name);

            foreach (string patente in patentes) {
                MessageBox.Show(patente); 
            }

            PantallaInicial form = new PantallaInicial();
            this.Hide();
            form.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void IniciarSesion_FormClosed(object sender, EventArgs e) {
            //MessageBox.Show("IniciarSesion_FormClosed");
        }

        private void IniciarSesion_RegistrarUsuarioClosed(object sender, EventArgs e) {
            this.Show();
        }
    }
}
