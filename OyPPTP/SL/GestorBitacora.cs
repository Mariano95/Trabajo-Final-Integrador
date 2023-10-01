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

        public bool RegistrarEvento(int evento, int idUsuario)
        {
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

            return true;
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

    }
}
