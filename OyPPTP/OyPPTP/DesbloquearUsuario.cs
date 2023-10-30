using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAL;
using BLL;

namespace OyPPTP
{
    public partial class DesbloquearUsuario : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public DesbloquearUsuario()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void desbloquear_usuario_Click(object sender, EventArgs e)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string mailUsuario = miDAL.EncriptarAES(this.mail_usuario_text.Text);

            bool result = UsuarioBLL.DesbloquearUsuario(mailUsuario);

            if (!result) {
                MessageBox.Show("La dirección de email ingresada no corresponde a ningún usuario del sistema.");
                return;
            }

            MessageBox.Show("Usuario desbloqueado con éxito.");
            this.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void DesbloquearUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
