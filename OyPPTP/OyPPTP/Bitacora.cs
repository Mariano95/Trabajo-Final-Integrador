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
            this.dataGridView1.Columns.Add("usuario", "Usuario");
            this.dataGridView1.Columns.Add("evento", "Evento");
            this.dataGridView1.Columns.Add("criticidad", "Criticidad");
            this.dataGridView1.Columns.Add("hora", "Hora");

            this.dataGridView1.Rows.Add(
                "Usuario 1",
                "Inicio de sesión fallido",
                "2",
                "30/05/2020 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Usuario 1",
                "Inicio de sesión fallido",
                "3",
                "30/05/2020 11:32AM"
            );

            this.dataGridView1.Rows.Add(
                "Usuario 1",
                "Inicio de sesión exitoso",
                "1",
                "30/05/2020 11:35AM"
            );

            this.dataGridView1.Rows.Add(
                "Usuario 1",
                "Citación creada",
                "1",
                "30/05/2020 11:56AM"
            );



            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PreBitacora preBitacora = new PreBitacora();
            preBitacora.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
