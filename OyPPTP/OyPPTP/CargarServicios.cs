﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class CargarServicios : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public CargarServicios()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text != "Actualizar")
            {
                RegistrarUsuario form = new RegistrarUsuario();
                this.Hide();
                form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CargarServicios_RegistrarUsuarioClosed);
                form.Show();
            }
            else {
                MessageBox.Show("Servicios actualizados correctamente.");
                this.Close();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CargarServicios_Load(object sender, EventArgs e)
        {

        }

        public void precargar() {
            this.checkBox3.Checked = true;
            this.checkBox5.Checked = true;
            this.button1.Text = "Actualizar";
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void CargarServicios_RegistrarUsuarioClosed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
