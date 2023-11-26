using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BLL;
using DAL;

namespace OyPPTP
{
    public partial class RegistrarUsuario : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void continuar_Click(object sender, EventArgs e)
        {
            string password = this.contrasena_text.Text;

            if (this.continuar.Text == "Continuar") {
                if (!ValidarPassword(password))
                {
                    MessageBox.Show("La contraseña ingresada no es válida. La misma debe poseer entre 8 y 32 caracteres, al menos una letra minúscula y otra mayúsucula, debe poseer al menos un número y uno o más de los siguientes carateres especiales $,%,! o *");
                    return;
                }
            }
            
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            
            string nombre = this.nombre_text.Text;
            string nombreEncrypted = miDAL.EncriptarAES(nombre);
            
            string apellido = this.apellido_text.Text;
            string apellidoEncrypted = miDAL.EncriptarAES(apellido);

            string dni = this.dni_text.Text;
            string dniEncrypted = miDAL.EncriptarAES(dni);

            string domicilio = this.domicilio_text.Text;
            string domicilioEncrypted = miDAL.EncriptarAES(domicilio);

            string email = this.email_text.Text;
            string emailEncrypted = miDAL.EncriptarAES(email);

            if (this.continuar.Text == "Continuar")
            {

                string passwordEncrypted = miDAL.EncriptarMD5(password);

                bool existeUsuario = UsuarioBLL.BuscarUsuario(dniEncrypted, emailEncrypted);

                if (existeUsuario)
                {
                    MessageBox.Show("Ya existe otro usuario con la misma dirección de email o el mismo número de DNI.");
                    return;
                }

                bool usuarioGuardado = UsuarioBLL.AltaUsuario(nombreEncrypted, apellidoEncrypted, dniEncrypted, domicilioEncrypted, emailEncrypted, passwordEncrypted);

                if (usuarioGuardado)
                {
                    MessageBox.Show("Usuario guardado con exito");
                }
                else
                {
                    MessageBox.Show("Hubo un error al guardar el usuario");
                }

                PreLogin form = new PreLogin();
                this.Hide();
                form.Show();
            }

            if (this.continuar.Text == "Actualizar") {

                //Obtengo los datos del usuario logueado
                UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

                //Reviso si existe algun otro usuario aparte del logueado que tenga el mismo mail o dni que esta tratando de asignarse
                bool existeOtroUsuario = UsuarioBLL.BuscarOtroUsuario(usuario.id, dniEncrypted, emailEncrypted);
                if (existeOtroUsuario) {
                    MessageBox.Show("Ya existe otro usuario con la misma dirección de email o el mismo número de DNI");
                    return;
                }

                //Ahora sí actualizo el usuario
                bool exito = UsuarioBLL.ActualizarUsuario(usuario.id, nombreEncrypted, apellidoEncrypted, dniEncrypted, domicilioEncrypted, emailEncrypted);
                if (!exito)
                {
                    MessageBox.Show("Error al actualizar usuario");
                    return;
                }

                MessageBox.Show("Datos del usuario actualizados con exito");;
                return;
            }

    }

        public void precargar() {
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

            DAL.DAL miDAL = DAL.DAL.GetDAL();

            this.nombre_text.Text = miDAL.DesencriptarAES(usuario.nombre);
            this.apellido_text.Text = miDAL.DesencriptarAES(usuario.apellido);
            this.domicilio_text.Text = miDAL.DesencriptarAES(usuario.domicilio);
            this.email_text.Text = miDAL.DesencriptarAES(usuario.email);
            this.contrasena_text.Enabled = false;
            this.contrasena_text.Visible = false;
            this.contrasena.Visible = false;
            this.dni_text.Text = miDAL.DesencriptarAES(usuario.dni);
            this.continuar.Text = "Actualizar";
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////    FUNCIONES AUXILIARES     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private bool ValidarPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$%!*])[A-Za-z\d$%!*]{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
