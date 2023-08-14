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
        public PantallaInicial()
        {
            InitializeComponent();
        }

        private void PantallaInicial_Load(object sender, EventArgs e)
        {

        }

        private void menuStripOptionClicked(object sender, EventArgs e) {
            
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            switch (selectedItem.Name) {
                case "modificarServicios":
                    CargarServicios form = new CargarServicios();
                    form.precargar();
                    form.Show();
                    break;
                case "modificarDatosPersonales":
                    RegistrarUsuario form2 = new RegistrarUsuario();
                    form2.precargar();
                    form2.Show();
                    break;
                case "modificarPassword":
                    CambiarPassword form4 = new CambiarPassword();
                    form4.Show();
                    break;
                case "buscarTrabajadores":
                    BuscarTrabajadores form3 = new BuscarTrabajadores();
                    form3.Show();
                    break;
                case "citacionesRecibidas":
                    GrilaCitaciones form5 = new GrilaCitaciones();
                    form5.Show();
                    form5.PrecargaTrabajador();
                    break;
                case "citacionesEnviadas":
                    GrilaCitaciones form6 = new GrilaCitaciones();
                    form6.PrecargaParticular();
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
                    form8.Show();
                    break;
                case "backup":
                    Backup form9 = new Backup();
                    form9.Show();
                    break;
                case "restaurarSistema":
                    RestaurarSistema form10 = new RestaurarSistema();
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
                    form12.Show();
                    break;
                case "patentesPorGrupo":
                    PatentesPorGrupo form13 = new PatentesPorGrupo();
                    form13.Show();
                    break;
                case "patentesPorUsuario":
                    PatentesPorUsuario form14 = new PatentesPorUsuario();
                    form14.Show();
                    break;
                case "gruposUsuarios":
                    GruposUsuarios form15 = new GruposUsuarios();
                    form15.Show();
                    break;
                case "crearUsuarioAdmin":
                    RegistrarUsuario form16 = new RegistrarUsuario();
                    form16.Show();
                    break;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
