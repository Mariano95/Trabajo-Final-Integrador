using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OyPPTP
{
    public partial class PantallaInicial : Form
    {
        List<string> patentes;

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PantallaInicial(List<string> patentes)
        {
            InitializeComponent();
            this.patentes = patentes;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PantallaInicial_Load(object sender, EventArgs e)
        {

            foreach ( ToolStripMenuItem dropdown in this.menuStrip1.Items)
            {
                foreach (ToolStripItem item in dropdown.DropDownItems) {
                    if (patentes.Contains(item.Name))
                    {
                        item.Enabled = true;
                    }
                }

            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void menuStripOptionClicked(object sender, EventArgs e) {
            
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            switch (selectedItem.Name) {
                case "modificarServicios":
                    CargarServicios form = new CargarServicios();
                    form.precargar();
                    this.Hide();
                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_CargarServiciosClosed);
                    form.Show();
                    break;
                case "modificarDatosPersonales":
                    RegistrarUsuario form2 = new RegistrarUsuario();
                    form2.precargar();
                    this.Hide();
                    form2.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_RegistrarUsuarioClosed);
                    form2.Show();
                    break;
                case "modificarPassword":
                    CambiarPassword form4 = new CambiarPassword();
                    this.Hide();
                    form4.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_CambiarPasswordClosed);
                    form4.Show();
                    break;
                case "buscarTrabajadores":
                    BuscarTrabajadores form3 = new BuscarTrabajadores();
                    this.Hide();
                    form3.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_BuscarTrabajadoresClosed);
                    form3.Show();
                    break;
                case "citacionesRecibidas":
                    GrilaCitaciones form5 = new GrilaCitaciones();
                    this.Hide();
                    form5.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_GrilaCitacionesClosed);
                    form5.Show();
                    form5.PrecargaTrabajador();
                    break;
                case "citacionesEnviadas":
                    GrilaCitaciones form6 = new GrilaCitaciones();
                    form6.PrecargaParticular();
                    this.Hide();
                    form6.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_GrilaCitacionesClosed);
                    form6.Show();
                    break;
                case "ocultarUsuario":
                    ModalOcultar form7 = new ModalOcultar();
                    if (this.menuStrip1.Items.Find("ocultarUsuario", true)[0].Text == "Reactivar usuario")
                    {
                        this.menuStrip1.Items.Find("ocultarUsuario", true)[0].Text = "Ocultar usuario";
                        form7.PrecargarReactivacion();
                    }
                    else {
                        this.menuStrip1.Items.Find("ocultarUsuario", true)[0].Text = "Reactivar usuario";
                    }
                    form7.Show();
                    break;
                case "bitacora":
                    PreBitacora form8 = new PreBitacora();
                    this.Hide();
                    form8.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_PreBitacoraClosed);
                    form8.Show();
                    break;
                case "backup":
                    Backup form9 = new Backup();
                    this.Hide();
                    form9.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_BackupClosed);
                    form9.Show();
                    break;
                case "restaurarSistema":
                    RestaurarSistema form10 = new RestaurarSistema();
                    this.Hide();
                    form10.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_RestaurarSistemaClosed);
                    form10.Show();
                    break;
                case "ingles":
                    MessageBox.Show("Idioma cambiado con éxito.");
                    break;
                case "portugues":
                    MessageBox.Show("Idioma cambiado con éxito.");
                    break;
                case "cerrarSesion":
                    PreLogin form11 = new PreLogin();
                    this.Close();
                    form11.Show();
                    break;
                case "desbloquearUsuario":
                    DesbloquearUsuario form12 = new DesbloquearUsuario();
                    this.Hide();
                    form12.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_DesbloquearUsuarioClosed);
                    form12.Show();
                    break;
                case "patentesPorGrupo":
                    PatentesPorGrupo form13 = new PatentesPorGrupo();
                    this.Hide();
                    form13.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_PatentesPorGrupoClosed);
                    form13.Show();
                    break;
                case "patentesPorUsuario":
                    PatentesPorUsuario form14 = new PatentesPorUsuario();
                    this.Hide();
                    form14.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_PatentesPorUsuarioClosed);
                    form14.Show();
                    break;
                case "gruposUsuarios":
                    GruposUsuarios form15 = new GruposUsuarios();
                    this.Hide();
                    form15.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_GruposUsuariosClosed);
                    form15.Show();
                    break;
                case "crearUsuarioAdmin":
                    RegistrarUsuario form16 = new RegistrarUsuario();
                    this.Hide();
                    form16.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_RegistrarUsuarioClosed);
                    form16.Show();
                    break;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PantallaInicial_CargarServiciosClosed(object sender, EventArgs e) {
            this.Show();
        }

        private void PantallaInicial_RegistrarUsuarioClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_CambiarPasswordClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_BuscarTrabajadoresClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_GrilaCitacionesClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_PreBitacoraClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_BackupClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_RestaurarSistemaClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_DesbloquearUsuarioClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_PatentesPorGrupoClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_PatentesPorUsuarioClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_GruposUsuariosClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void PantallaInicial_FormClosed(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                DialogResult result = MessageBox.Show("Se va a cerrar la aplicación", "Cerrando aplicación");
                System.Windows.Forms.Application.Exit();
            }
        }        

    }
}
