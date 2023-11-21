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
using DTO;

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

            UsuarioDTO usuarioDTO = miDAL.GetUsuarioById(idUsuario);
            UsuarioBLL.GetUsuarioBLL(
                usuarioDTO.id = idUsuario,
                usuarioDTO.nombre,
                usuarioDTO.apellido,
                usuarioDTO.dni,
                usuarioDTO.domicilio,
                usuarioDTO.email,
                "", //Rol 
                new List<string>(), //Servicios 
                usuarioDTO.usuarioOculto,
                usuarioDTO.fallosAutenticacionConsecutivos,
                usuarioDTO.bloqueado
            );

            bool usuarioBloqueado = BLL.UsuarioBLL.ValidarUsuarioBloqueado(idUsuario);
            if (usuarioBloqueado) {
                MessageBox.Show("Tu usuario se encuentra bloqueado. Por favor, contactate con un administrador.");
                UsuarioBLL.ResetSingleton();
                return;
            }

            bool passwordCorrecto = BLL.UsuarioBLL.ValidarPasswordUsuario(idUsuario, passwordEncrypted);
            if (!passwordCorrecto) {
                MessageBox.Show("La información de la dirección de correo electrónico o la contraseña no es válida");
                UsuarioBLL.ResetSingleton();
                return;
            }
            
            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(1, idUsuario);

            //Aca validamos los digitos verificadores
            bool integridadok = true;
            List<string> excepciones = new List<string>();
            excepciones.Add("Bitacora");
            excepciones.Add("Bitacora_Detalle");
            List<string> excepciones_2da_ejecucion = new List<string>();
            List<string> excepciones_3ra_ejecucion = new List<string>();
            //Dejo las tablas de bitacora para el final porque son propensas a sufrir cambios durante este analisis
            List<(string, int)> res = miDAL.SumaVerificadoresHorizontalesPorTabla(excepciones);
            foreach ((string, int) t in res) {
                //MessageBox.Show(t.Item1.ToString()+"-"+t.Item2.ToString());
                excepciones_2da_ejecucion.Add(t.Item1);
                excepciones_3ra_ejecucion.Add(t.Item1);
                bool integridadtablaok = miDAL.CoincideVerificadorVertical(t.Item2, t.Item1);
                if (!integridadtablaok)
                {
                    gestorBitacora.RegistrarEvento(8, idUsuario, tabla: t.Item1.ToString());
                    integridadok = false;
                    //MessageBox.Show("Fallo de integridad en la tabla " + t.Item1.ToString());
                }
            }

            //Ahora hago la verificacion con Bitacora
            excepciones_2da_ejecucion.Add("Bitacora_Detalle");
            res = miDAL.SumaVerificadoresHorizontalesPorTabla(excepciones_2da_ejecucion);
            foreach ((string, int) t in res)
            {
                //MessageBox.Show(t.Item1.ToString() + "-" + t.Item2.ToString());
                bool integridadtablaok = miDAL.CoincideVerificadorVertical(t.Item2, t.Item1);
                if (!integridadtablaok)
                {
                    gestorBitacora.RegistrarEvento(8, idUsuario, tabla: t.Item1.ToString());
                    integridadok = false;
                    //MessageBox.Show("Fallo de integridad en la tabla " + t.Item1.ToString());
                }
            }

            //Y finalmente con Bitacora_Detalle
            excepciones_3ra_ejecucion.Add("Bitacora");
            res = miDAL.SumaVerificadoresHorizontalesPorTabla(excepciones_3ra_ejecucion);
            foreach ((string, int) t in res)
            {
                //MessageBox.Show(t.Item1.ToString() + "-" + t.Item2.ToString());
                bool integridadtablaok = miDAL.CoincideVerificadorVertical(t.Item2, t.Item1);
                if (!integridadtablaok)
                {
                    gestorBitacora.RegistrarEvento(8, idUsuario, tabla: t.Item1.ToString());
                    integridadok = false;
                    //MessageBox.Show("Fallo de integridad en la tabla " + t.Item1.ToString());
                }
            }

            if (integridadok) {
                gestorBitacora.RegistrarEvento(9, idUsuario);
            }

            //Aca recuperamos las patentes del usuario
            GestorPatentes gestorPatentes = new GestorPatentes();
            List<string> patentes = gestorPatentes.ObtenerPatentes(idUsuario, "PantallaInicial");
            PantallaInicial form = new PantallaInicial(patentes);
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
