using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BLL;

namespace OyPPTP
{
    public partial class CambiarPassword : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public CambiarPassword()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void actualizar_Click(object sender, EventArgs e)
        {
            string nuevaPassword = this.nueva_contrasena_text.Text;

            if (!ValidarPassword(nuevaPassword)) {
                MessageBox.Show("La contraseña ingresada no es válida. La misma debe poseer entre 8 y 32 caracteres, al menos una letra minúscula y otra mayúsucula, debe poseer al menos un número y uno o más de los siguientes carateres especiales $,%,! o *");
                return;
            }

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string passwordEncrypted = miDAL.EncriptarMD5(nuevaPassword);

            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            UsuarioBLL.ActualizarUsuario(usuario.id, passwordEncrypted);

            MessageBox.Show("Éxito al modificar la contraseña");
            this.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    FORM LOAD     ////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CambiarPassword_Load(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    METODOS PRIVADOS     ////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private bool ValidarPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$%!*])[A-Za-z\d$%!*]{8,32}$";
            return Regex.IsMatch(password, pattern);
        }

    }
}
