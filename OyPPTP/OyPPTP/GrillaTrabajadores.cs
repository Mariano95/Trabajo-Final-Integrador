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
            this.grilla_trabajadores.Columns.Add("nombre", "Nombre");
            this.grilla_trabajadores.Columns.Add("apellido", "Apellido");
            this.grilla_trabajadores.Columns.Add("dni", "DNI");
            this.grilla_trabajadores.Columns.Add("email", "Email");
            this.grilla_trabajadores.Columns.Add("citacionesCumplidas", "Citaciones cumplidas");
            this.grilla_trabajadores.Columns.Add("promedioCalifaciones", "Promedio calificaciones");

            DataGridViewButtonColumn citarButtonColumn = new DataGridViewButtonColumn();
            citarButtonColumn.Name = "Acciones";
            citarButtonColumn.Text = "Citar trabajador";
            citarButtonColumn.UseColumnTextForButtonValue = true;
            grilla_trabajadores.CellClick += CellClickHandler;


            this.grilla_trabajadores.Rows.Add(
                "Trabajador1",
                "Apellido1",
                "11111111",
                "Trabajador1@gmail.com",
                "2",
                "70"
            );

            this.grilla_trabajadores.Rows.Add(
                "Trabajador2",
                "Apellido2",
                "22222222",
                "Trabajador2@gmail.com",
                "9",
                "80"
            );

            this.grilla_trabajadores.Rows.Add(
                "Trabajador3",
                "Apellido3",
                "33333333",
                "Trabajador3@gmail.com",
                "25",
                "20"
            );

            this.grilla_trabajadores.Columns.Insert(6, citarButtonColumn);

            this.grilla_trabajadores.AutoResizeColumns();
            this.grilla_trabajadores.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CellClickHandler(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == this.grilla_trabajadores.Columns["Acciones"].Index)
            {
                CitarTrabajador form = new CitarTrabajador();
                form.Show();
            }
        }

        private void grilla_trabajadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
