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
            this.miembros_grilla.Columns.Add("nombre", "Nombre");
            this.miembros_grilla.Columns.Add("apellido", "Apellido");
            this.miembros_grilla.Columns.Add("dni", "DNI");

            this.miembros_grilla.Rows.Add(
                "Usuario1",
                "Apellido1",
                "11111111"
            );

            this.miembros_grilla.Rows.Add(
                "Usuario2",
                "Apellido2",
                "22222222"
            );

            this.miembros_grilla.Rows.Add(
                "Usuario3",
                "Apellido3",
                "33333333"
            );

            this.miembros_grilla.Rows.Add(
                "Usuario4",
                "Apellido4",
                "44444444"
            );


            this.miembros_grilla.AutoResizeColumns();
            this.miembros_grilla.ReadOnly = true;

            this.otros_usuarios_grilla.Columns.Add("nombre", "Nombre");
            this.otros_usuarios_grilla.Columns.Add("apellido", "Apellido");
            this.otros_usuarios_grilla.Columns.Add("dni", "DNI");


            this.otros_usuarios_grilla.Rows.Add(
                "Usuario5",
                "Apellido5",
                "55555555"
            );

            this.otros_usuarios_grilla.Rows.Add(
                "Usuario6",
                "Apellido6",
                "66666666"
            );

            this.otros_usuarios_grilla.Rows.Add(
                "Usuario7",
                "Apellido7",
                "77777777"
            );

            this.otros_usuarios_grilla.Rows.Add(
                "Usuario8",
                "Apellido8",
                "88888888"
            );


            this.otros_usuarios_grilla.AutoResizeColumns();
            this.otros_usuarios_grilla.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
