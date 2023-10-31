using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using SL;
//////////////////////////////////
using System.Configuration;
using DAL;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;

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
            //Pruebo encodear
            //string cstring = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //MessageBox.Show(cstring);
            //DAL.DAL miDAL = DAL.DAL.GetDAL();
            //string stringConexion = miDAL.EncriptarAES(cstring);
            //MessageBox.Show(stringConexion.ToString());
            ////string stringConexionb64 = System.Convert.ToBase64String(stringConexion);
            ////MessageBox.Show(stringConexionb64);
            //string ok = miDAL.DesencriptarAES(stringConexion);
            //MessageBox.Show(ok);

            ////Pruebo desencodear
            //string cstringencodedb64 = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            //MessageBox.Show(cstringencodedb64);
            //DAL.DAL miDAL2 = DAL.DAL.GetDAL();
            //string stringConexionok = miDAL2.DesencriptarAES(cstringencodedb64);
            //MessageBox.Show(stringConexionok);
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

                //Inicializar tablas bd
                //miDAL.IncializarEventos();
                //miDAL.InicializarPatentes();

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

