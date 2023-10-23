using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SL;
using BLL;

namespace OyPPTP
{
    public partial class GruposUsuarios : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GruposUsuarios()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void GruposUsuarios_Load(object sender, EventArgs e)
        {
            GestorGrupos gestorGrupos = new GestorGrupos();
            List<(int, string)> grupos = gestorGrupos.GetGrupos();

            DAL.DAL miDAL = DAL.DAL.GetDAL();

            foreach ((int, string) grupo in grupos) {
                this.grupo_combo.Items.Add(grupo.Item1.ToString() + " : " + miDAL.DesencriptarAES(grupo.Item2.ToString()));
            }

            this.grupo_combo.SelectedIndexChanged += new System.EventHandler(grupoCombo_SelectedIndexChanged);
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void grupoCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            this.miembros_grilla.Columns.Clear();
            this.otros_usuarios_grilla.Columns.Clear();

            this.miembros_grilla.Columns.Add("id", "ID");
            this.miembros_grilla.Columns.Add("nombre", "Nombre");
            this.miembros_grilla.Columns.Add("apellido", "Apellido");
            this.miembros_grilla.Columns.Add("dni", "DNI");

            this.otros_usuarios_grilla.Columns.Add("id", "ID");
            this.otros_usuarios_grilla.Columns.Add("nombre", "Nombre");
            this.otros_usuarios_grilla.Columns.Add("apellido", "Apellido");
            this.otros_usuarios_grilla.Columns.Add("dni", "DNI");

            int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split(":", 2)[0].Replace(" ", ""));

            GestorGrupos gestorGrupos = new GestorGrupos();
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<(int, string, string, string)> usuarios = miDAL.ListaUsuarios();
            List<(int, string, string, string)> usuariosGrupo = gestorGrupos.GetUsuariosPorGrupo(grupoId);

            foreach ((int, string, string, string) usuario in usuarios) {
                if (usuariosGrupo.Contains(usuario)) {
                    this.miembros_grilla.Rows.Add(
                        usuario.Item1,
                        miDAL.DesencriptarAES(usuario.Item2),
                        miDAL.DesencriptarAES(usuario.Item3),
                        miDAL.DesencriptarAES(usuario.Item4)
                    );
                }
                else
                {
                    this.otros_usuarios_grilla.Rows.Add(
                        usuario.Item1,
                        miDAL.DesencriptarAES(usuario.Item2),
                        miDAL.DesencriptarAES(usuario.Item3),
                        miDAL.DesencriptarAES(usuario.Item4)
                    );
                }
            }

            this.miembros_grilla.AutoResizeColumns();
            this.miembros_grilla.ReadOnly = true;

            this.otros_usuarios_grilla.AutoResizeColumns();
            this.otros_usuarios_grilla.ReadOnly = true;

        }

        private void volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminar_grupo_Click(object sender, EventArgs e)
        {
            if (this.grupo_combo.SelectedItem == null) {
                MessageBox.Show("Por favor, seleccioná un grupo antes de continuar");
                return;
            }
            
            int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split(":", 2)[0].Replace(" ", ""));
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

            GestorPatentes gestorPatentes = new GestorPatentes();
            (bool, string) puedeEliminarGrupo = gestorPatentes.PuedeElminarGrupo(grupoId);
            if (!puedeEliminarGrupo.Item1)
            {
                MessageBox.Show(puedeEliminarGrupo.Item2);
                return;
            }

            GestorGrupos gestorGrupos = new GestorGrupos();
            gestorGrupos.EliminarGrupo(grupoId);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(14, usuario.id);

            int sumaVerificadoresHorizontales = 0;
            DAL.DAL miDAL = DAL.DAL.GetDAL();

            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Grupo");
            miDAL.ActualizarVerificadorVertical("Persona_Grupo", sumaVerificadoresHorizontales);

            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Grupo_Patente");
            miDAL.ActualizarVerificadorVertical("Grupo_Patente", sumaVerificadoresHorizontales);

            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Grupo");
            miDAL.ActualizarVerificadorVertical("Grupo", sumaVerificadoresHorizontales);

            MessageBox.Show("Éxito al eliminar el grupo");
            this.grupo_combo.Items.Remove(this.grupo_combo.SelectedItem);
            this.grupo_combo.ResetText();
            this.miembros_grilla.Rows.Clear();
            this.otros_usuarios_grilla.Rows.Clear();
        }

        private void crear_grupo_Click(object sender, EventArgs e)
        {
            string nombre_nuevo_grupo = this.crear_nuevo_grupo_text.Text.Trim();
            if (nombre_nuevo_grupo == "") {
                MessageBox.Show("El nombre del grupo no puede ser vacio");
                return;    
            }
            GestorGrupos gestorGrupos = new GestorGrupos();
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            gestorGrupos.CrearGrupo(nombre_nuevo_grupo, usuario.id);

            MessageBox.Show("Exito al crear el grupo " + nombre_nuevo_grupo);
            this.Refresh();

        }

        private void quitar_del_grupo_Click(object sender, EventArgs e)
        {
            


        }

        private void agregar_al_grupo_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();
            int idUsuarioSeleccionado;

            if (this.otros_usuarios_grilla.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.otros_usuarios_grilla.SelectedRows[0];

                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Por favor, seleccioná un usuario antes de continuar");
                    return;
                }

                idUsuarioSeleccionado = (int)row.Cells[0].Value;
                if (idUsuarioSeleccionado == usuario.id)
                {
                    MessageBox.Show("No podés agregar o quitar de grupos a tu propio usuario.");
                    return;
                }

                if (this.grupo_combo.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccioná un grupo antes de continuar");
                    return;
                }

                int grupoId = Int32.Parse(this.grupo_combo.SelectedItem.ToString().Split(":", 2)[0].Replace(" ", ""));

                GestorGrupos gestorGrupos = new GestorGrupos();
                gestorGrupos.AgregarUsuarioGrupo(idUsuarioSeleccionado, grupoId);

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.RegistrarEvento(15, usuario.id);

                DAL.DAL miDAL = DAL.DAL.GetDAL();
                string concatenado = "";
                concatenado += grupoId.ToString();
                concatenado += idUsuarioSeleccionado;
                int verificador_horizontal = 0;
                int contador = 1;
                foreach (char caracter in concatenado)
                {
                    verificador_horizontal += (int)caracter * contador;
                    contador++;
                }

                bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontalPersonaGrupo(idUsuarioSeleccionado, grupoId, verificador_horizontal);

                int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Grupo");
                bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Grupo", sumaVerificadoresHorizontales);

                this.otros_usuarios_grilla.Rows.Remove(row);
                this.otros_usuarios_grilla.Refresh();
                this.miembros_grilla.Rows.Add(row);
                this.miembros_grilla.Refresh();
                MessageBox.Show("Éxito al agregar al usuario al grupo");

            }
            else {
                MessageBox.Show("Por favor, seleccioná el usuario a agregar antes de continuar.");
                return;
            }
        }
    }
}
