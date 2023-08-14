using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class GrilaCitaciones : Form
    {
        public GrilaCitaciones()
        {
            InitializeComponent();
        }

        public void PrecargaTrabajador() {
            this.dataGridView1.Columns.Add("estado", "Estado");
            this.dataGridView1.Columns.Add("usuario", "Usuario");
            this.dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn cambioEstadoButton = new DataGridViewButtonColumn();
            cambioEstadoButton.Name = "Cambio estado";
            cambioEstadoButton.Text = "Cambiar estado";
            cambioEstadoButton.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn comentarButton = new DataGridViewButtonColumn();
            comentarButton.Name = "Comentarios";
            comentarButton.Text = "Comentarios";
            comentarButton.UseColumnTextForButtonValue = true;
            dataGridView1.CellClick += CellClickHandler;

            DataGridViewButtonColumn calificarButton = new DataGridViewButtonColumn();
            calificarButton.Name = "Calificar usuario";
            calificarButton.Text = "Calificar usuario";
            calificarButton.UseColumnTextForButtonValue = true;
            dataGridView1.CellClick += CellClickHandler;

            this.dataGridView1.Rows.Add(
                "Pendiente de aceptacion",
                "Particular1",
                "19/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Aceptada",
                "Particular2",
                "26/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Rechazada",
                "Particular3",
                "19/05/2022 11:30AM"
            );

            this.dataGridView1.Columns.Insert(3, cambioEstadoButton);
            this.dataGridView1.Columns.Insert(4, comentarButton);
            this.dataGridView1.Columns.Insert(5, calificarButton);

            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
        }

        public void PrecargaParticular() {
            this.dataGridView1.Columns.Add("estado", "Estado");
            this.dataGridView1.Columns.Add("trabajador", "Trabajador");
            this.dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn cambioEstadoButton = new DataGridViewButtonColumn();
            cambioEstadoButton.Name = "Cambio estado";
            cambioEstadoButton.Text = "Cambiar estado";
            cambioEstadoButton.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn comentarButton = new DataGridViewButtonColumn();
            comentarButton.Name = "Comentarios";
            comentarButton.Text = "Comentarios";
            comentarButton.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn cambioFechaButton = new DataGridViewButtonColumn();
            cambioFechaButton.Name = "Modificar fecha de inicio/fin";
            cambioFechaButton.Text = "Modificar fecha de inicio/fin";
            cambioFechaButton.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn calificarButton = new DataGridViewButtonColumn();
            calificarButton.Name = "Calificar usuario";
            calificarButton.Text = "Calificar usuario";
            calificarButton.UseColumnTextForButtonValue = true;
            dataGridView1.CellClick += CellClickHandler;

            this.dataGridView1.Rows.Add(
                "Cumplida",
                "Trabajador3",
                "30/05/2020 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Pendiente de aceptacion",
                "Trabajador1",
                "19/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Aceptada",
                "Trabajador2",
                "26/05/2022 11:30AM"
            );

            this.dataGridView1.Rows.Add(
                "Rechazada",
                "Trabajador3",
                "30/05/2022 11:30AM"
            );

            this.dataGridView1.Columns.Insert(3, cambioEstadoButton);
            this.dataGridView1.Columns.Insert(4, comentarButton);
            this.dataGridView1.Columns.Insert(5, cambioFechaButton);
            this.dataGridView1.Columns.Insert(6, calificarButton);


            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
        }

        private void GrilaCitaciones_Load(object sender, EventArgs e)
        {
            
        }

        private void CellClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridView1.Columns["Cambio estado"].Index)
            {
                CambioEstadoCitacion form = new CambioEstadoCitacion();
                form.Show();
            }

            if (e.ColumnIndex == this.dataGridView1.Columns["Comentarios"].Index)
            {
                ComentarCitacion form2 = new ComentarCitacion();
                form2.Show();
            }

            if (this.dataGridView1.Columns.Contains("Modificar fecha de inicio/fin")) {
                if (e.ColumnIndex == this.dataGridView1.Columns["Modificar fecha de inicio/fin"].Index)
                {
                    CitarTrabajador form = new CitarTrabajador();
                    form.PrecargaCambioFecha();
                    form.Show();
                }
            }

            if (e.ColumnIndex == this.dataGridView1.Columns["Calificar usuario"].Index)
            {
                CalificarUsuario form3 = new CalificarUsuario();
                form3.Show();
            }

        }

    }
}
