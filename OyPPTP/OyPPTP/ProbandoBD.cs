using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using SL;
//
using System.Configuration;
using DAL;

namespace OyPPTP
{
    public partial class ProbandoBD : Form
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public ProbandoBD()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void ProbandoBD_Load(object sender, EventArgs e)
        {
            //string cstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //MessageBox.Show(cstring);
            //DAL.DAL miDAL = DAL.DAL.GetDAL();
            ////byte[] stringConexion = miDAL.EncriptarAES(cstring);
            ////MessageBox.Show(stringConexion.ToString());
            ////string stringConexionb64 = System.Convert.ToBase64String(stringConexion);
            ////MessageBox.Show(stringConexionb64);
            //byte[] bytes = System.Convert.FromBase64String(cstring);
            //string ok = miDAL.DesencriptarAES(bytes);
            ////string ok = miDAL.DesencriptarAES(stringConexion);
            //MessageBox.Show(ok);


        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void ProbandoBD_Shown(object sender, EventArgs e) 
        {
            this.Refresh();

            //Instancio DAL desde ahora para ya tener el singleton
            DAL.DAL miDAL = DAL.DAL.GetDAL();

            GestorConexion gestorConexion = new GestorConexion();
            bool conexion = gestorConexion.ProbarConexionBD();

            if (conexion)
            {
                MessageBox.Show("Éxito al inicializar la base de datos");
                PreLogin form1 = new PreLogin();
                this.Hide();
                form1.Show();
            }
            else {
                MessageBox.Show("No se pudo inicializar la base de datos");
                CredencialesBD form1 = new CredencialesBD();
                this.Hide();
                form1.Show();
            }
            

        }
    }
}

