using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class GruposUsuarios : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GruposUsuarios()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void GruposUsuarios_Load(object sender, EventArgs e)
        {
            this.miembrosGrupo.Columns.Add("nombre", "Nombre");
            this.miembrosGrupo.Columns.Add("apellido", "Apellido");
            this.miembrosGrupo.Columns.Add("dni", "DNI");

            this.miembrosGrupo.Rows.Add(
                "Usuario1",
                "Apellido1",
                "11111111"
            );

            this.miembrosGrupo.Rows.Add(
                "Usuario2",
                "Apellido2",
                "22222222"
            );

            this.miembrosGrupo.Rows.Add(
                "Usuario3",
                "Apellido3",
                "33333333"
            );

            this.miembrosGrupo.Rows.Add(
                "Usuario4",
                "Apellido4",
                "44444444"
            );


            this.miembrosGrupo.AutoResizeColumns();
            this.miembrosGrupo.ReadOnly = true;

            this.otrosUsuarios.Columns.Add("nombre", "Nombre");
            this.otrosUsuarios.Columns.Add("apellido", "Apellido");
            this.otrosUsuarios.Columns.Add("dni", "DNI");


            this.otrosUsuarios.Rows.Add(
                "Usuario5",
                "Apellido5",
                "55555555"
            );

            this.otrosUsuarios.Rows.Add(
                "Usuario6",
                "Apellido6",
                "66666666"
            );

            this.otrosUsuarios.Rows.Add(
                "Usuario7",
                "Apellido7",
                "77777777"
            );

            this.otrosUsuarios.Rows.Add(
                "Usuario8",
                "Apellido8",
                "88888888"
            );


            this.otrosUsuarios.AutoResizeColumns();
            this.otrosUsuarios.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
