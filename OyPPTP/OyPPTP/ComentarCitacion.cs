using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class ComentarCitacion : Form
    {
        public ComentarCitacion()
        {
            InitializeComponent();
        }

        private void ComentarCitacion_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("usuario", "Usuario");
            this.dataGridView1.Columns.Add("comentario", "Comentario");
            this.dataGridView1.Columns.Add("fecha", "Fecha");

            this.dataGridView1.Rows.Add(
                "Trabajador1",
                "Texto del primer comentario",
                "19/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Particular1",
                "Texto del segundo comentario",
                "20/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Trabajador1",
                "Texto del tercer comentario",
                "20/05/2022 11:35AM"
            );


            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comentario guardado con éxito");
            this.Close();
        }
    }
}
