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
    public partial class PreBitacora : Form
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public PreBitacora()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    HANDLER FUNCTIONS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void buscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = this.fecha_inicio_date.Value;
            DateTime fechaHasta = this.fecha_fin_date.Value;
            int eventoId;
            string eventoNombre;
            if (this.evento_combo.SelectedItem == null)
            {
                eventoId = -1;
                eventoNombre = "Sin filtro";
            }
            else {
                eventoId = Int32.Parse(this.evento_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
                eventoNombre = this.evento_combo.SelectedItem.ToString().Split("-", 2)[1];
            }
            int usuarioId;
            string  usuarioNombre;
            if (this.usuario_combo.SelectedItem == null)
            {
                usuarioId = -1;
                usuarioNombre = "Sin filtro";
            }
            else {
                usuarioId = Int32.Parse(this.usuario_combo.SelectedItem.ToString().Split("-", 2)[0].Replace(" ", ""));
                usuarioNombre = this.usuario_combo.SelectedItem.ToString().Split(":", 2)[1];
            }

            GestorBitacora gestorBitacora = new GestorBitacora();
            List<(string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string)> eventos = gestorBitacora.ObtenerBitacora(fechaDesde, fechaHasta, eventoId, usuarioId);

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            string usuario;
            string email;
            string usuarioAfectado;
            string emailUsuarioAfectado;
            string evento;
            int criticidad;
            string tabla;
            string grupo;
            string patente;
            DateTime hora;

            List<(string, string, string, int, DateTime, string, string, string, string, string)> registrosBitacora = new List<(string, string, string, int, DateTime, string, string, string, string, string)>();

            foreach ((string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string) evento_ in eventos) {
                usuario = miDAL.DesencriptarAES(evento_.Item3) + " - " + miDAL.DesencriptarAES(evento_.Item1) + " " + miDAL.DesencriptarAES(evento_.Item2);
                email = miDAL.DesencriptarAES(evento_.Item4);
                usuarioAfectado = miDAL.DesencriptarAES(evento_.Item12) + " - " + miDAL.DesencriptarAES(evento_.Item10) + " " + miDAL.DesencriptarAES(evento_.Item11);
                emailUsuarioAfectado = miDAL.DesencriptarAES(evento_.Item13);
                evento = miDAL.DesencriptarAES(evento_.Item5);
                criticidad = evento_.Item6;
                hora = evento_.Item7;
                tabla = evento_.Item8;
                grupo = miDAL.DesencriptarAES(evento_.Item9);
                patente = evento_.Item14;
                registrosBitacora.Add((usuario, email, evento, criticidad, hora, usuarioAfectado, emailUsuarioAfectado, tabla, grupo, patente));
            }

            DateTime fechaHoraImpresion = DateTime.Now;
            Bitacora bitacora = new Bitacora(
                "Bitácora", 
                fechaHoraImpresion.ToString(), 
                fechaHoraImpresion.ToString(), 
                fechaDesde.Day.ToString() + "/" + fechaDesde.Month.ToString() + "/" + fechaDesde.Year.ToString(),
                fechaHasta.Day.ToString() + "/" + fechaHasta.Month.ToString() + "/" + fechaHasta.Year.ToString(),
                usuarioNombre, 
                eventoNombre, 
                registrosBitacora
            );
            this.Hide();
            bitacora.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreBitacora_BitacoraClosed);
            bitacora.Show();
        }

        private void PreBitacora_BitacoraClosed(object sender, EventArgs e) {
            this.Show();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void PreBitacora_Load(object sender, EventArgs e)
        {
            List<(int, string, string, string)> listaUsuarios = UsuarioBLL.GetUsuarios();

            DAL.DAL miDAL = DAL.DAL.GetDAL();

            foreach ((int, string, string, string) usuario in listaUsuarios)
            {
                int id = usuario.Item1;
                string nombre = miDAL.DesencriptarAES(usuario.Item2);
                string apellido = miDAL.DesencriptarAES(usuario.Item3);
                string dni = miDAL.DesencriptarAES(usuario.Item4);
                this.usuario_combo.Items.Add(id.ToString() + " - DNI " + dni + " : " + nombre + " " + apellido);
            }

            GestorBitacora gestorBitacora = new GestorBitacora();
            List<(int, string)> listaEventos = gestorBitacora.ListaEventos();
            foreach ((int, string) evento in listaEventos)
            {
                int id = evento.Item1;
                string nombre = miDAL.DesencriptarAES(evento.Item2);
                this.evento_combo.Items.Add(id.ToString() + " - " + nombre);
            }
        }
    }
}
