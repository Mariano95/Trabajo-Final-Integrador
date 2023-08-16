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
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GrilaCitaciones()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void PrecargaTrabajador() {
            this.grilla_citaciones.Columns.Add("estado", "Estado");
            this.grilla_citaciones.Columns.Add("usuario", "Usuario");
            this.grilla_citaciones.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn cambioEstadoButton = new DataGridViewButtonColumn();
            cambioEstadoButton.Name = "Cambio estado";
            cambioEstadoButton.Text = "Cambiar estado";
            cambioEstadoButton.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn comentarButton = new DataGridViewButtonColumn();
            comentarButton.Name = "Comentarios";
            comentarButton.Text = "Comentarios";
            comentarButton.UseColumnTextForButtonValue = true;
            grilla_citaciones.CellClick += CellClickHandler;

            DataGridViewButtonColumn calificarButton = new DataGridViewButtonColumn();
            calificarButton.Name = "Calificar usuario";
            calificarButton.Text = "Calificar usuario";
            calificarButton.UseColumnTextForButtonValue = true;
            grilla_citaciones.CellClick += CellClickHandler;

            this.grilla_citaciones.Rows.Add(
                "Pendiente de aceptacion",
                "Particular1",
                "19/05/2022 11:30AM"
            );

            this.grilla_citaciones.Rows.Add(
                "Aceptada",
                "Particular2",
                "26/05/2022 11:30AM"
            );

            this.grilla_citaciones.Rows.Add(
                "Rechazada",
                "Particular3",
                "19/05/2022 11:30AM"
            );

            this.grilla_citaciones.Columns.Insert(3, cambioEstadoButton);
            this.grilla_citaciones.Columns.Insert(4, comentarButton);
            this.grilla_citaciones.Columns.Insert(5, calificarButton);

            this.grilla_citaciones.AutoResizeColumns();
            this.grilla_citaciones.ReadOnly = true;
        }

        public void PrecargaParticular() {
            this.grilla_citaciones.Columns.Add("estado", "Estado");
            this.grilla_citaciones.Columns.Add("trabajador", "Trabajador");
            this.grilla_citaciones.Columns.Add("fecha", "Fecha");

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
            grilla_citaciones.CellClick += CellClickHandler;

            this.grilla_citaciones.Rows.Add(
                "Cumplida",
                "Trabajador3",
                "30/05/2020 11:30AM"
            );

            this.grilla_citaciones.Rows.Add(
                "Pendiente de aceptacion",
                "Trabajador1",
                "19/05/2022 11:30AM"
            );

            this.grilla_citaciones.Rows.Add(
                "Aceptada",
                "Trabajador2",
                "26/05/2022 11:30AM"
            );

            this.grilla_citaciones.Rows.Add(
                "Rechazada",
                "Trabajador3",
                "30/05/2022 11:30AM"
            );

            this.grilla_citaciones.Columns.Insert(3, cambioEstadoButton);
            this.grilla_citaciones.Columns.Insert(4, comentarButton);
            this.grilla_citaciones.Columns.Insert(5, cambioFechaButton);
            this.grilla_citaciones.Columns.Insert(6, calificarButton);


            this.grilla_citaciones.AutoResizeColumns();
            this.grilla_citaciones.ReadOnly = true;
        }

        private void GrilaCitaciones_Load(object sender, EventArgs e)
        {
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CellClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.grilla_citaciones.Columns["Cambio estado"].Index)
            {
                CambioEstadoCitacion form = new CambioEstadoCitacion();
                form.Show();
            }

            if (e.ColumnIndex == this.grilla_citaciones.Columns["Comentarios"].Index)
            {
                ComentarCitacion form2 = new ComentarCitacion();
                form2.Show();
            }

            if (this.grilla_citaciones.Columns.Contains("Modificar fecha de inicio/fin")) {
                if (e.ColumnIndex == this.grilla_citaciones.Columns["Modificar fecha de inicio/fin"].Index)
                {
                    CitarTrabajador form = new CitarTrabajador();
                    form.PrecargaCambioFecha();
                    form.Show();
                }
            }

            if (e.ColumnIndex == this.grilla_citaciones.Columns["Calificar usuario"].Index)
            {
                CalificarUsuario form3 = new CalificarUsuario();
                form3.Show();
            }

        }

    }
}
