using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class RegistrarUsuario : Form
    {
        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usuario guardado con exito");

            if (this.button1.Text != "Actualizar")
            {
                PreLogin form = new PreLogin();
                form.Show();
            }
            else
            {
                this.Close();
            }

            
        }

        public void precargar() {
            this.textBox1.Text = "Mariano";
            this.textBox2.Text = "Martin";
            this.textBox3.Text = "Darragueyra 1417";
            this.textBox4.Text = "marianomartin806@gmail.com";
            this.textBox5.Enabled = false;
            this.textBox5.Visible = false;
            this.label7.Visible = false;
            this.textBox6.Text = "38636383";
            this.button1.Text = "Actualizar";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
