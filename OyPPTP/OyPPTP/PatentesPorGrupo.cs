using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SL;
using BLL;

namespace OyPPTP
{
    public partial class PatentesPorGrupo : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PatentesPorGrupo()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrupoCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.patentesOtorgadas.Columns.Clear();
            this.patentesNoOtorgadas.Columns.Clear();

            int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            GestorGrupos gestorGrupos = new GestorGrupos();
            List<int> gruposUsuario = gestorGrupos.GetGruposPorUsuario(usuario.id);
            if (gruposUsuario.Contains(grupoId)) {
                MessageBox.Show("No es posible modificar las patentes de un grupo al cual pertenecés");
                return;
            }

            GestorPatentes gestorPatentes = new GestorPatentes();
            List<(int, string)> listaPatentes = gestorPatentes.GetPatentes();
            List<(int, string)> patentesGrupo = gestorPatentes.GetPatentesUsuarioGrupo(grupoId:grupoId);

            this.patentesOtorgadas.Columns.Add("id", "ID");
            this.patentesOtorgadas.Columns.Add("nombre", "Nombre");

            this.patentesNoOtorgadas.Columns.Add("id", "ID");
            this.patentesNoOtorgadas.Columns.Add("nombre", "Nombre");

            foreach ((int, string) patente in listaPatentes)
            {
                if (patentesGrupo.Contains(patente))
                {
                    this.patentesOtorgadas.Rows.Add(
                        patente.Item1, patente.Item2
                    );
                }
                else
                {
                    this.patentesNoOtorgadas.Rows.Add(
                        patente.Item1, patente.Item2
                    );
                }
            }

            this.patentesOtorgadas.AutoResizeColumns();
            this.patentesOtorgadas.ReadOnly = true;

            this.patentesNoOtorgadas.AutoResizeColumns();
            this.patentesNoOtorgadas.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

            private void PatentesPorGrupo_Load(object sender, EventArgs e)
        {

            GestorGrupos gestorGrupos = new GestorGrupos();
            List<(int, string)> listaGrupos = gestorGrupos.GetGrupos();

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            foreach ((int, string) grupo in listaGrupos)
            {
                int id = grupo.Item1;
                string nombre = miDAL.DesencriptarAES(grupo.Item2);
                this.grupo_combo.Items.Add(id.ToString() + " - Grupo " + nombre);
            }
            this.grupo_combo.SelectedIndexChanged += new System.EventHandler(GrupoCombo_SelectedIndexChanged);
        }

        private void quitarPatente_Click(object sender, EventArgs e)
        {
            if (this.grupo_combo.SelectedItem == null) {
                MessageBox.Show("Por favor, seleccioná un grupo antes de continuar");
                return;
            }
            
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));

            GestorGrupos gestorGrupos = new GestorGrupos();

            string patente = "";
            (bool, string) result;
            bool success = true;
            string message = "";

            foreach (DataGridViewRow row in this.patentesOtorgadas.SelectedRows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }
                result = gestorGrupos.QuitarPatente(usuario.id, grupoId, (int)row.Cells[0].Value);
                success = result.Item1;
                message = result.Item2;
                patente = row.Cells[1].Value.ToString();
                if (!success)
                {
                    MessageBox.Show("No se puede quitar la patente " + patente + " : " + message);
                }
                else
                {
                    this.patentesOtorgadas.Rows.Remove(row);
                    this.patentesOtorgadas.Refresh();
                    this.patentesNoOtorgadas.Rows.Add(row);
                    this.patentesNoOtorgadas.Refresh();
                    this.patentesNoOtorgadas.AutoResizeColumns();
                    MessageBox.Show("Éxito al quitar la patente " + patente + " al grupo.");
                }
            }            

        }

        private void otorgarPatente_Click(object sender, EventArgs e)
        {
            if (this.grupo_combo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccioná un grupo antes de continuar");
                return;
            }

            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));

            GestorGrupos gestorGrupos = new GestorGrupos();

            string patente = "";
            (bool, string) result;
            bool success = true;
            string message = "";

            foreach (DataGridViewRow row in this.patentesNoOtorgadas.SelectedRows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }
                result = gestorGrupos.AgregarPatente(usuario.id, grupoId, (int)row.Cells[0].Value);
                success = result.Item1;
                message = result.Item2;
                patente = row.Cells[1].Value.ToString();
                if (!success)
                {
                    MessageBox.Show("No se puede agregar la patente " + patente + " : " + message);
                }
                else
                {
                    this.patentesNoOtorgadas.Rows.Remove(row);
                    this.patentesNoOtorgadas.Refresh();
                    this.patentesOtorgadas.Rows.Add(row);
                    this.patentesOtorgadas.Refresh();
                    this.patentesOtorgadas.AutoResizeColumns();
                    MessageBox.Show("Éxito al agregar la patente " + patente + " al grupo.");
                }
            }
        }
    }
}
