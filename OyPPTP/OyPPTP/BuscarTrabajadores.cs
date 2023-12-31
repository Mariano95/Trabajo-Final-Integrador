﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class BuscarTrabajadores : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public BuscarTrabajadores()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void BuscarTrabajadores_Load(object sender, EventArgs e)
        {
            this.tipo_de_servicio_combo.Items.Add("Servicio 1");
            this.tipo_de_servicio_combo.Items.Add("Servicio 2");
            this.tipo_de_servicio_combo.Items.Add("Servicio 3");
            this.tipo_de_servicio_combo.Items.Add("Servicio 4");
            this.tipo_de_servicio_combo.Items.Add("Servicio 5");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void buscar_trabajadores_Click(object sender, EventArgs e)
        {
            GrillaTrabajadores form = new GrillaTrabajadores();
            form.Show();
        }
    }
}
