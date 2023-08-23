using System;
using System.Configuration;
using DAL;

namespace SL
{
    public class GestorConexion
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    CONSTRUCTOR     /////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GestorConexion() {}

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////    FUNCIONES PUBLICAS     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public bool ProbarConexionBD()
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();

            //byte[] stringConexionEncrypted = RecuperarStringConexion();
            //string stringConexionDecrypted = miDAL.DesencriptarAES(stringConexionEncrypted);

            //Esto es para probar hasta que tenga bien resuelto donde guardar la key de encriptado.
            //Borrar esta linea de abajo y descomentar las dos de arriba.
            string stringConexionDecrypted = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            bool resultado = miDAL.ConectarBD(stringConexionDecrypted);
            return resultado;
        }

        public bool ConectarBD(string stringConexion) {

            DAL.DAL miDAL = DAL.DAL.GetDAL();

            bool resultado = miDAL.ConectarBD(stringConexion);
            return resultado;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////    FUNCIONES AXILIARES     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private byte[] RecuperarStringConexion()
        {
            string miConnectionStringB64 = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            byte[] bytes = System.Convert.FromBase64String(miConnectionStringB64);
            return bytes;
        }

    }

}
