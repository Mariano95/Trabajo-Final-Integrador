using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class Bitacora : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public Bitacora()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Bitacora_Load(object sender, EventArgs e)
        {
            this.grilla_bitacora.Columns.Add("usuario", "Usuario");
            this.grilla_bitacora.Columns.Add("evento", "Evento");
            this.grilla_bitacora.Columns.Add("criticidad", "Criticidad");
            this.grilla_bitacora.Columns.Add("hora", "Hora");

            this.grilla_bitacora.Rows.Add(
                "Usuario 1",
                "Inicio de sesión fallido",
                "2",
                "30/05/2020 11:30AM"
            );

            this.grilla_bitacora.Rows.Add(
                "Usuario 1",
                "Inicio de sesión fallido",
                "3",
                "30/05/2020 11:32AM"
            );

            this.grilla_bitacora.Rows.Add(
                "Usuario 1",
                "Inicio de sesión exitoso",
                "1",
                "30/05/2020 11:35AM"
            );

            this.grilla_bitacora.Rows.Add(
                "Usuario 1",
                "Citación creada",
                "1",
                "30/05/2020 11:56AM"
            );



            this.grilla_bitacora.AutoResizeColumns();
            this.grilla_bitacora.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void cerrar_bitacora_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cambiar_filtros_Click(object sender, EventArgs e)
        {
            PreBitacora preBitacora = new PreBitacora();
            preBitacora.Show();
        }
    }
}
