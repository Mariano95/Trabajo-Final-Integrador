﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class PatentesPorUsuario : Form
    {
        public PatentesPorUsuario()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PatentesPorUsuario_Load(object sender, EventArgs e)
        {
            this.patentesOtorgadas.Columns.Add("nombre", "Nombre");


            this.patentesOtorgadas.Rows.Add(
                "Patente1"
            );

            this.patentesOtorgadas.Rows.Add(
                "Patente2"
            );

            this.patentesOtorgadas.Rows.Add(
                "Patente3"
            );

            this.patentesOtorgadas.Rows.Add(
                "Patent4"
            );


            this.patentesOtorgadas.AutoResizeColumns();
            this.patentesOtorgadas.ReadOnly = true;

            this.patentesNoOtorgadas.Columns.Add("nombre", "Nombre");


            this.patentesNoOtorgadas.Rows.Add(
                "Patente5"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente6"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente7"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente8"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente9"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente10"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente11"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente12"
            );

            this.patentesNoOtorgadas.Rows.Add(
                "Patente13"
            );


            this.patentesNoOtorgadas.AutoResizeColumns();
            this.patentesNoOtorgadas.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
