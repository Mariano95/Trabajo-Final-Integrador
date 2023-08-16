using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            MessageBox.Show("Usuario guardado con exito");

            if (this.continuar.Text != "Actualizar")
            {
                PreLogin form = new PreLogin();
                this.Hide();
                form.Show();
            }
            else
            {
                this.Close();
            }

            
        }

        public void precargar() {
            this.nombre_text.Text = "Mariano";
            this.apellido_text.Text = "Martin";
            this.domicilio_text.Text = "Darragueyra 1417";
            this.email_text.Text = "marianomartin806@gmail.com";
            this.contrasena_text.Enabled = false;
            this.contrasena_text.Visible = false;
            this.contrasena.Visible = false;
            this.dni_text.Text = "38636383";
            this.continuar.Text = "Actualizar";
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

    }
}
