using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SL;
using BLL;

namespace OyPPTP
{
    public partial class RestaurarSistema : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public RestaurarSistema()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iniciar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BAK (*.bak)|*.bak";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                GestorBackup gestorBackup = new GestorBackup();
                this.iniciar.Enabled = false;
                this.cancelar.Enabled = false;
                this.restaurar_sistema.Visible = false;
                this.aguarde_label.Visible = true;
                this.Refresh();
                bool success = gestorBackup.RestaurarSistema(filename);
                if (!success) {
                    MessageBox.Show("Error al restaurar sistema");
                    this.iniciar.Enabled = true;
                    this.cancelar.Enabled = true;
                    this.restaurar_sistema.Visible = true;
                    this.aguarde_label.Visible = false;
                    this.Refresh();
                    return;
                }

                UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.RegistrarEvento(12, usuario.id);

                DAL.DAL miDAL = DAL.DAL.GetDAL();
                bool integridadok = true;
                List<(string, int)> res = miDAL.SumaVerificadoresHorizontalesPorTabla();
                foreach ((string, int) t in res)
                {
                    MessageBox.Show(t.Item1.ToString() + "-" + t.Item2.ToString());
                    bool integridadtablaok = miDAL.CoincideVerificadorVertical(t.Item2, t.Item1);
                    if (!integridadtablaok)
                    {
                        gestorBitacora.RegistrarEvento(8, usuario.id);
                        integridadok = false;
                        MessageBox.Show("Fallo de integridad en la tabla " + t.Item1.ToString());
                    }
                }
                if (integridadok)
                {
                    gestorBitacora.RegistrarEvento(9, usuario.id);
                }

                MessageBox.Show("Datos del sistema restaurados con éxito desde " + filename);
            }
            this.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void RestaurarSistema_Load(object sender, EventArgs e)
        {

        }
    }
}
