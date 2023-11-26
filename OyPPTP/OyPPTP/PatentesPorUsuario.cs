using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using SL;

namespace OyPPTP
{
    public partial class PatentesPorUsuario : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PatentesPorUsuario()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PatentesPorUsuario_Load(object sender, EventArgs e)
        {

            List<(int, string, string, string)> listaUsuarios = UsuarioBLL.GetUsuarios();

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            foreach ((int, string, string, string) usuario in listaUsuarios)
            {
                int id = usuario.Item1;
                string nombre = miDAL.DesencriptarAES(usuario.Item2);
                string apellido = miDAL.DesencriptarAES(usuario.Item3);
                string dni = miDAL.DesencriptarAES(usuario.Item4);
                this.usuario_combo.Items.Add(id.ToString() + "- DNI " + dni + " : " + nombre + " " + apellido);
            }
            this.usuario_combo.SelectedIndexChanged += new System.EventHandler(UsuarioCombo_SelectedIndexChanged);

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UsuarioCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            this.patentesOtorgadas.Columns.Clear();
            this.patentesNoOtorgadas.Columns.Clear();

            int usuarioId = Int32.Parse(this.usuario_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));

            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            if (usuario.id == usuarioId)
            {
                MessageBox.Show("No es posible modificar las patentes de tu propio usuario");
                return;
            }

            GestorPatentes gestorPatentes = new GestorPatentes();
            List<(int, string)> listaPatentes = gestorPatentes.GetPatentes();

            
            List<(int, string)> patentesUsuario = UsuarioBLL.GetPatentesUsuario(usuarioId);

            this.patentesOtorgadas.Columns.Add("id", "ID");
            this.patentesOtorgadas.Columns.Add("nombre", "Nombre");

            this.patentesNoOtorgadas.Columns.Add("id", "ID");
            this.patentesNoOtorgadas.Columns.Add("nombre", "Nombre");

            foreach ((int, string) patente in listaPatentes) {
                if (patentesUsuario.Contains(patente))
                {
                    this.patentesOtorgadas.Rows.Add(
                        patente.Item1, patente.Item2
                    );
                }
                else {
                    this.patentesNoOtorgadas.Rows.Add(
                        patente.Item1, patente.Item2
                    );
                }
            }

            this.patentesOtorgadas.AutoResizeColumns();
            this.patentesOtorgadas.ReadOnly = true;

            this.patentesNoOtorgadas.AutoResizeColumns();
            this.patentesNoOtorgadas.ReadOnly = true;
        }

        private void quitarPatente_Click(object sender, EventArgs e)
        {
            if (this.usuario_combo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccioná un usuario antes de continuar");
                return;
            }

            int usuarioId = Int32.Parse(this.usuario_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
            string patente = "";
            (bool, string) result;
            bool success = true;
            string message = "";

            foreach (DataGridViewRow row in this.patentesOtorgadas.SelectedRows) {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }
                result = UsuarioBLL.QuitarPatente(usuarioId, (int)row.Cells[0].Value);
                success = result.Item1;
                message = result.Item2;
                patente = row.Cells[1].Value.ToString();
                if (!success)
                {
                    MessageBox.Show("No se puede quitar la patente " + patente + " : " + message);
                }
                else {
                    this.patentesOtorgadas.Rows.Remove(row);
                    this.patentesOtorgadas.Refresh();
                    this.patentesNoOtorgadas.Rows.Add(row);
                    this.patentesNoOtorgadas.Refresh();
                    this.patentesNoOtorgadas.AutoResizeColumns();
                    MessageBox.Show("Éxito al quitar la patente " + patente + " al usuario.");
                }
            }

        }

        private void otorgarPatente_Click(object sender, EventArgs e)
        {
            if (this.usuario_combo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccioná un usuario antes de continuar");
                return;
            }

            int usuarioId = Int32.Parse(this.usuario_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
            string patente = "";
            (bool, string) result;
            bool success = true;
            string message = "";

            foreach (DataGridViewRow row in this.patentesNoOtorgadas.SelectedRows)
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }
                result = UsuarioBLL.AgregarPatente(usuarioId, (int)row.Cells[0].Value);
                success = result.Item1;
                message = result.Item2;
                patente = row.Cells[1].Value.ToString();
                if (!success)
                {
                    MessageBox.Show("No se puede agregar la patente " + patente + " : " + message);
                }
                else
                {
                    this.patentesNoOtorgadas.Rows.Remove(row);
                    this.patentesNoOtorgadas.Refresh();
                    this.patentesOtorgadas.Rows.Add(row);
                    this.patentesOtorgadas.Refresh();
                    this.patentesOtorgadas.AutoResizeColumns();
                    MessageBox.Show("Éxito al agregar la patente " + patente + " al usuario.");
                }
            }
        }

        private void eliminar_usuario_Click(object sender, EventArgs e)
        {
            if (this.usuario_combo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccioná un usuario antes de continuar");
                return;
            }

            int usuarioId = Int32.Parse(this.usuario_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
            GestorPatentes gestorPatentes = new GestorPatentes();
            (bool, string) puedeEliminarUsuario = gestorPatentes.PuedeEliminarUsuario(usuarioId);
            if (!puedeEliminarUsuario.Item1)
            {
                MessageBox.Show(puedeEliminarUsuario.Item2);
                return;
            }

            GestorUsuarios gestorUsuarios = new GestorUsuarios();
            bool exito = gestorUsuarios.EliminarUsuario(usuarioId);
            if (!exito) {
                MessageBox.Show("Error al eliminar usuario");
                return;
            }

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            int sumaVerificadoresHorizontales;
            bool verificador_vertical_ok;
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona", sumaVerificadoresHorizontales);
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Grupo");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Grupo", sumaVerificadoresHorizontales);
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Patente");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Patente", sumaVerificadoresHorizontales);

            GestorBitacora gestorBitacora = new GestorBitacora();
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            gestorBitacora.RegistrarEvento(26, usuario.id, id_usuario_afectado: usuarioId);

            MessageBox.Show("Usuario eliminado con éxito");
            this.usuario_combo.Items.Remove(this.usuario_combo.SelectedItem);
            this.usuario_combo.ResetText();
            this.patentesOtorgadas.Rows.Clear();
            this.patentesNoOtorgadas.Rows.Clear();

        }
    }
}