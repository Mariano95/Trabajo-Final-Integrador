using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class GrillaTrabajadores : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GrillaTrabajadores()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void GrillaTrabajadores_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("nombre", "Nombre");
            this.dataGridView1.Columns.Add("apellido", "Apellido");
            this.dataGridView1.Columns.Add("dni", "DNI");
            this.dataGridView1.Columns.Add("email", "Email");
            this.dataGridView1.Columns.Add("citacionesCumplidas", "Citaciones cumplidas");
            this.dataGridView1.Columns.Add("promedioCalifaciones", "Promedio calificaciones");
            this.dataGridView1.Columns.Add("distanciaKm", "Distancia en kilómetros");

            DataGridViewButtonColumn citarButtonColumn = new DataGridViewButtonColumn();
            citarButtonColumn.Name = "Acciones";
            citarButtonColumn.Text = "Citar trabajador";
            citarButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.CellClick += CellClickHandler;


            this.dataGridView1.Rows.Add(
                "Trabajador1",
                "Apellido1",
                "11111111",
                "Trabajador1@gmail.com",
                "2",
                "70",
                "5"
            );

            this.dataGridView1.Rows.Add(
                "Trabajador2",
                "Apellido2",
                "22222222",
                "Trabajador2@gmail.com",
                "9",
                "80",
                "10"
            );

            this.dataGridView1.Rows.Add(
                "Trabajador3",
                "Apellido3",
                "33333333",
                "Trabajador3@gmail.com",
                "25",
                "20",
                "15"
            );

            this.dataGridView1.Columns.Insert(7, citarButtonColumn);

            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CellClickHandler(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == this.dataGridView1.Columns["Acciones"].Index)
            {
                CitarTrabajador form = new CitarTrabajador();
                form.Show();
            }
        }
    }
}
