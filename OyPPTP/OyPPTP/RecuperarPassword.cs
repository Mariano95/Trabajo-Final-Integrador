using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAL;
using BLL;
using SL;

namespace UI
{
    public partial class RecuperarPassword : Form
    {
        public RecuperarPassword()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RecuperarPassword_Load(object sender, EventArgs e)
        {

        }

        private void recuperar_password_aceptar_Click(object sender, EventArgs e)
        {

            string dni = this.recuperar_password_dni.Text;
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string dniEncrypted = miDAL.EncriptarAES(dni);

            int idUsuario = miDAL.BuscarUsuario(dniEncrypted);
            if (idUsuario == -1)
            {
                MessageBox.Show("No existe ningún usuario en el sistema para el número de dni ingresado");
                return;
            }

            GestorPassword gestorPassword = new GestorPassword();
            string nuevaPassword = gestorPassword.GenerarPasswordRandom();
            (bool, string) result = UsuarioBLL.EnviarMailUsuario(idUsuario, nuevaPassword);
            
            bool success = result.Item1;
            string message = result.Item2;

            if (!success) {
                MessageBox.Show(message);
                return;
            }

            //Una vez que me aseguro que el usuario recibio el mail, impacto el cambio de contraseña
            string nuevaPasswordEncrypted = miDAL.EncriptarMD5(nuevaPassword);
            UsuarioBLL.ActualizarUsuario(idUsuario, nuevaPasswordEncrypted, logeado:false);

            MessageBox.Show("Se enviará un mail a la dirección de correo electrónico de tu usuario, a través de él podrás restablecer tu contraseña.");
        
        }

        private void recuperar_password_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
