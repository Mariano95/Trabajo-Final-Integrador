using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace SL
{
    public class GestorBitacora
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GestorBitacora() { }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    METODOS PUBLICOS     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public bool RegistrarEvento(int evento, int idUsuario, string tabla = "", int id_grupo = -1, int id_patente = -1, int id_usuario_afectado = -1)
        {
            //Primero escribo en Bitacora
            DateTime fechaEvento = ObtenerFechaNuevoEvento();
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            int id = miDAL.InsertarEventoBitacora(evento, idUsuario, fechaEvento);
            if (id == -1)
                return false;

            int verificador_horizontal = CalcularVerificadorHorizontal(id, idUsuario, fechaEvento, evento);
            bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Bitacora", id, verificador_horizontal);
            if (!verificador_horizontal_ok)
            {
                return false;
            }
            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Bitacora");
            bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Bitacora", sumaVerificadoresHorizontales);
            if (!verificador_vertical_ok)
            {
                return false;
            }

            //Luego en Bitacora_Detalle
            int id_detalle = miDAL.InsertarBitacoraDetalle(evento, id, tabla, id_grupo, id_usuario_afectado, id_patente);

            if (id_detalle != -1) {

                verificador_horizontal = CalcularVerificadorHorizontalDetalle(id_detalle, id, tabla, id_grupo, id_usuario_afectado, id_patente);
                verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Bitacora_Detalle", id_detalle, verificador_horizontal);
                if (!verificador_horizontal_ok)
                {
                    return false;
                }
                sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Bitacora_Detalle");
                verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Bitacora_Detalle", sumaVerificadoresHorizontales);
                if (!verificador_vertical_ok)
                {
                    return false;
                }

            }

            return true;
        }

        public List<(int, string)> ListaEventos()
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaEventos();
        }

        public List<(string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string)> ObtenerBitacora(DateTime fechaDesde, DateTime fechaHasta, int eventoId, int usuarioId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ObtenerBitacora(fechaDesde, fechaHasta, eventoId, usuarioId);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    METODOS PRIVADOS     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private DateTime ObtenerFechaNuevoEvento() {
            return DateTime.Now;
        }

        private int CalcularVerificadorHorizontal(int id, int idUsuario, DateTime fecha, int evento) {
            string concatenado = "";
            concatenado += id.ToString();
            concatenado += idUsuario.ToString();
            concatenado += fecha.ToString();
            concatenado += evento.ToString();

            int digito = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                digito += (int)caracter * contador;
                contador++;
            }
            return digito;
        }

        private int CalcularVerificadorHorizontalDetalle(int id_detalle, int id, string tabla, int id_grupo, int id_usuario_afectado, int id_patente) {
            string concatenado = "";
            concatenado += id_detalle.ToString();
            concatenado += id.ToString();
            if (tabla != "") {
                concatenado += tabla;
            }
            if (id_grupo != -1) {
                concatenado += id_grupo.ToString();
            }
            if (id_usuario_afectado != -1) {
                concatenado += id_usuario_afectado.ToString();
            }
            if (id_patente != -1) {
                concatenado += id_patente.ToString();
            }
            
            int digito = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                digito += (int)caracter * contador;
                contador++;
            }
            return digito;
        }

    }
}
