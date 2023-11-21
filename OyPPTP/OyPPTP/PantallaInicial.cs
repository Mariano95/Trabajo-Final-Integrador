using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SL;
using BLL;
using System.Diagnostics;

namespace OyPPTP
{
    public partial class PantallaInicial : Form
    {
        List<string> patentes;
        int idIdioma;
        string leyenda_error_sin_navegador;
        string leyenda_error_carga_ayuda;
        string leyenda_error_sin_ayuda;

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PantallaInicial(List<string> patentes)
        {
            InitializeComponent();
            this.patentes = patentes;
            this.idIdioma = 3; //Español por defecto
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PantallaInicial_Load(object sender, EventArgs e)
        {

            this.KeyPreview = true;
            inicializarCampos();
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void inicializarCampos() {
            GestorIdioma gestorIdioma = new GestorIdioma();
            List<(int, string)> idiomas = gestorIdioma.ObtenerListadoIdiomas(this.idIdioma);
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string texto;

            texto = gestorIdioma.ObtenerTextos("pantalla_inicial_label_1", this.idIdioma);
            if (texto == ""){
                this.pantalla_inicial_label_1.Text = "Hola, ";
            }
            else {
                this.pantalla_inicial_label_1.Text = texto;
            }
            this.pantalla_inicial_label_1.Text += miDAL.DesencriptarAES(usuario.apellido).ToUpper() + " " + miDAL.DesencriptarAES(usuario.nombre).ToUpper();

            texto = gestorIdioma.ObtenerTextos("pantalla_inicial_label_2", this.idIdioma);
            if (texto != "")
            {
                this.pantalla_inicial_label_2.Text = texto;
            }

            texto = gestorIdioma.ObtenerTextos("leyenda_error_sin_navegador", this.idIdioma);
            if (texto != "")
            {
                this.leyenda_error_sin_navegador = texto;
            }
            else 
            {
                this.leyenda_error_sin_navegador = "";
            }

            texto = gestorIdioma.ObtenerTextos("leyenda_error_carga_ayuda", this.idIdioma);
            if (texto != "")
            {
                this.leyenda_error_carga_ayuda = texto;
            }
            else
            {
                this.leyenda_error_carga_ayuda = "";
            }

            texto = gestorIdioma.ObtenerTextos("leyenda_error_sin_ayuda", this.idIdioma);
            if (texto != "")
            {
                this.leyenda_error_sin_ayuda = texto;
            }
            else
            {
                this.leyenda_error_sin_ayuda = "";
            }

            this.Refresh();

            foreach ((int, string) idioma in idiomas)
            {
                this.cambiarIdioma.DropDownItems.AddRange(
                    new System.Windows.Forms.ToolStripItem[] {
                        new System.Windows.Forms.ToolStripMenuItem(idioma.Item2, null, new System.EventHandler(this.menuStripOptionClicked))
                    }
                );
            }

            foreach (ToolStripMenuItem dropdown in this.menuStrip1.Items)
            {
                texto = gestorIdioma.ObtenerTextos(dropdown.Name, this.idIdioma);
                if (texto != "") {
                    dropdown.Text = texto;
                }

                foreach (ToolStripItem item in dropdown.DropDownItems)
                {
                    texto = gestorIdioma.ObtenerTextos(item.Name, this.idIdioma);
                    if (texto != "")
                    {
                        item.Text = texto;
                    }
                    if (patentes.Contains(item.Name))
                    {
                        item.Enabled = true;
                    }
                }

            }
        }
        
        private void menuStripOptionClicked(object sender, EventArgs e)
        {

            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            switch (selectedItem.Name)
            {
                case "modificarServicios":
                    MessageBox.Show("Aún no implementado");
                    return;
                    CargarServicios form = new CargarServicios();
                    form.precargar();
                    this.Hide();
                    form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_CargarServiciosClosed);
                    form.Show();
                    break;
                case "modificarDatosPersonales":
                    MessageBox.Show("Aún no implementado");
                    return;
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
                    MessageBox.Show("Aún no implementado");
                    return;
                    BuscarTrabajadores form3 = new BuscarTrabajadores();
                    this.Hide();
                    form3.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_BuscarTrabajadoresClosed);
                    form3.Show();
                    break;
                case "citacionesRecibidas":
                    MessageBox.Show("Aún no implementado");
                    return;
                    GrilaCitaciones form5 = new GrilaCitaciones();
                    this.Hide();
                    form5.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_GrilaCitacionesClosed);
                    form5.Show();
                    form5.PrecargaTrabajador();
                    break;
                case "citacionesEnviadas":
                    MessageBox.Show("Aún no implementado");
                    return;
                    GrilaCitaciones form6 = new GrilaCitaciones();
                    form6.PrecargaParticular();
                    this.Hide();
                    form6.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_GrilaCitacionesClosed);
                    form6.Show();
                    break;
                case "ocultarUsuario":
                    MessageBox.Show("Aún no implementado");
                    return;
                    ModalOcultar form7 = new ModalOcultar();
                    if (this.menuStrip1.Items.Find("ocultarUsuario", true)[0].Text == "Reactivar usuario")
                    {
                        this.menuStrip1.Items.Find("ocultarUsuario", true)[0].Text = "Ocultar usuario";
                        form7.PrecargarReactivacion();
                    }
                    else
                    {
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
                case "cerrarSesion":
                    PreLogin form11 = new PreLogin();
                    this.Hide();
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
                    MessageBox.Show("Aún no implementado");
                    return;
                    RegistrarUsuario form16 = new RegistrarUsuario();
                    this.Hide();
                    form16.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_RegistrarUsuarioClosed);
                    form16.Show();
                    break;
            }
            switch (selectedItem.Text)
            {
                case "English":
                    this.idIdioma = 1;
                    MessageBox.Show("Language set successfully.");
                    this.cambiarIdioma.DropDownItems.Clear();
                    inicializarCampos();
                    break;
                case "Português":
                    MessageBox.Show("Idioma ainda não implementado");
                    break;
                case "Español":
                    this.idIdioma = 3;
                    MessageBox.Show("Idioma cambiado con éxito.");
                    this.cambiarIdioma.DropDownItems.Clear();
                    inicializarCampos();
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
            else{
                GestorBitacora gestorBitacora = new GestorBitacora();
                UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
                gestorBitacora.RegistrarEvento(4, usuario.id);
                UsuarioBLL.ResetSingleton();
            }
        }

        private void PantallaInicial_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.F12)
            {

                GestorAyuda gestorAyuda = new GestorAyuda();
                string link_ayuda = gestorAyuda.ObtenerLinkAyuda(this.Name, this.idIdioma);

                if (link_ayuda == "") {
                    MessageBox.Show(this.leyenda_error_sin_ayuda);
                }

                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = link_ayuda,
                    UseShellExecute = true
                };

                try
                {
                    Process.Start(info);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259) {
                        MessageBox.Show(this.leyenda_error_sin_navegador);
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show(this.leyenda_error_carga_ayuda);
                }

            }
        }

    }
}
