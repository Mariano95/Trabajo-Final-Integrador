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

        }

        private void otorgarPatente_Click(object sender, EventArgs e)
        {

        }
    }
}
