using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SL;

namespace OyPPTP
{
    public partial class CredencialesBD : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public CredencialesBD()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void conectar_Click(object sender, EventArgs e)
        {
            string servidor = this.servidor_text.Text;
            string usuario = this.usuario_text.Text;
            string contrasena = this.contrasena_text.Text;
            string stringConexion = "Data Source=" + servidor + ";Initial Catalog=TFI_DB;User ID=" + usuario + ";Password=" + contrasena;
            MessageBox.Show(stringConexion);

            GestorConexion gestorConexion = new GestorConexion();

            this.Hide();
            LoadingForm loadingForm = new LoadingForm();
            loadingForm.Show();
            loadingForm.Refresh();

            bool conexion = gestorConexion.ConectarBD(stringConexion);
            if (conexion) {
                MessageBox.Show("Éxito al inicializar la base de datos");
                PreLogin form1 = new PreLogin();
                loadingForm.Close();
                form1.Show();
            }
            else {
                MessageBox.Show("No se pudo inicializar la base de datos");
                loadingForm.Close();
                this.Show();
            }            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CredencialesBD_Load(object sender, EventArgs e)
        {
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CredencialesBD_FormClosed(object sender, EventArgs e) {

            //Consulto primero por la visibilidad del form, para que
            //el evento de cierre de uno de sus hijos no dispare esta rutina de cierre
            if (this.Visible){
                DialogResult result = MessageBox.Show("Se va a cerrar la aplicación", "Cerrando aplicación");
                System.Windows.Forms.Application.Exit();
            }


        }

    }
}
