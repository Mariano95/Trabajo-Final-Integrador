using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class UsuarioBLL
    {
        private static UsuarioBLL usuario_singleton;
        private string nombre;
        private string apellido;
        private string dni;
        private string domicilio;
        private string email;
        private string rol;
        private List<String> servicios;
        private bool usuarioOculto;
        private bool bloqueado;



        private UsuarioBLL(string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, bool bloqueado) {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.domicilio = domicilio;
            this.email = email;
            this.rol = rol;
            this.servicios = servicios;
            this.usuarioOculto = usuarioOculto;
            this.bloqueado = bloqueado;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////    METODOS PUBLICOS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static bool AltaUsuario(string nombre, string apellido, string dni, string domicilio, string email, string pasword)
        {
            usuario_singleton = new UsuarioBLL(nombre, apellido, dni, domicilio, email, null, null, false, false);
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            int id = miDAL.InsertarUsuario(usuario_singleton.nombre, usuario_singleton.apellido, usuario_singleton.dni, usuario_singleton.domicilio, usuario_singleton.email, pasword);
            if (id == -1)
                return false;
            int verificador_horizontal = CalcularVerificadorHorizontal(id, usuario_singleton, pasword, 0);
            bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Persona", id, verificador_horizontal);
            if (!verificador_horizontal_ok)
            {
                return false;
            }
            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona");
            bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona", sumaVerificadoresHorizontales);
            if (!verificador_vertical_ok)
            {
                return false;
            }

            return true;
        }

            public static bool BuscarUsuario(string dni, string email)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.BuscarUsuario(dni, email);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////    METODOS PRIVADOS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private static int CalcularVerificadorHorizontal(int id, UsuarioBLL usuario, string password, int fallos_autenticacion_consecutivos)
        {
            //Primero voy a concatenar todos los campos del usuario en un unico string
            string concatenado = "";
            concatenado += id.ToString();
            concatenado += usuario.nombre;
            concatenado += usuario.apellido;
            concatenado += usuario.dni;
            concatenado += usuario.domicilio;
            concatenado += usuario.email;
            concatenado += password;
            if (usuario.usuarioOculto)
                concatenado += "0";
            else
                concatenado += "1";
            concatenado += fallos_autenticacion_consecutivos.ToString();
            if (usuario.bloqueado)
                concatenado += "1";
            else
                concatenado += "0";


            int digito = 0;
            //El contador es para usar como peso de cada caracter
            int contador = 1;
            //Ahora convierto cada caracter de la cadena a su codificacion ascii y la sumo al contador
            foreach (char caracter in concatenado) {
                digito += (int)caracter * contador;
                contador++;
            }
            return digito;
        }



    }
}
