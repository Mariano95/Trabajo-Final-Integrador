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
    public partial class Backup : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public Backup()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iniciar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BAK (*.bak)|*.bak";
            sfd.FileName = "TFI_DB_Backup.bak";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string savePath = Path.GetDirectoryName(sfd.FileName);
                GestorBackup gestorBackup = new GestorBackup();
                (bool, string) result = gestorBackup.GenerarArchivoBackup(sfd.FileName);
                bool success = result.Item1;
                string message = result.Item2;
                if (!success) {
                    MessageBox.Show("Error al generar el backup: " + message);
                    return;
                }

                UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.RegistrarEvento(11, usuario.id);

                MessageBox.Show("Copia de seguridad creada con éxito en " + savePath);
            }
            this.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Backup_Load(object sender, EventArgs e)
        {

        }
    }
}
