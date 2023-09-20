using System;
using System.Collections.Generic;
using DAL;
using DTO;
using SL;

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
        private int fallosAutenticacionConsecutivos;
        private bool bloqueado;

        public static UsuarioBLL GetUsuarioBLL(string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
        {
            if (usuario_singleton == null)
            {
                usuario_singleton = new UsuarioBLL(nombre, apellido, dni, domicilio, email, rol, servicios, usuarioOculto, fallosAutenticacionConsecutivos, bloqueado);
            }
            return usuario_singleton;
        }

        private UsuarioBLL(string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado) {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.domicilio = domicilio;
            this.email = email;
            this.rol = rol;
            this.servicios = servicios;
            this.usuarioOculto = usuarioOculto;
            this.fallosAutenticacionConsecutivos = fallosAutenticacionConsecutivos;
            this.bloqueado = bloqueado;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////    METODOS PUBLICOS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static bool AltaUsuario(string nombre, string apellido, string dni, string domicilio, string email, string pasword)
        {
            UsuarioBLL usuario = new UsuarioBLL(nombre, apellido, dni, domicilio, email, null, null, false, 0, false);
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            int id = miDAL.InsertarUsuario(usuario.nombre, usuario.apellido, usuario.dni, usuario.domicilio, usuario.email, pasword);
            if (id == -1)
                return false;
            int verificador_horizontal = CalcularVerificadorHorizontal(id, usuario, pasword, 0);
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

        public static int BuscarUsuarioPorMail(string emailEncrypted)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.BuscarUsuarioPorMail(emailEncrypted);
        }

        public static bool ValidarUsuarioBloqueado(int idUsuario) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ValidarUsuarioBloqueado(idUsuario);
        }

        public static bool ValidarPasswordUsuario(int idUsuario, string passwordEncrypted) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            bool passwordOk = miDAL.ValidarPasswordUsuario(idUsuario, passwordEncrypted);
            UsuarioDTO DTO = miDAL.GetUsuarioById(idUsuario);
            if (!passwordOk)
            {
                int iniciosSesionFallidos = miDAL.IniciosSesionFallidos(idUsuario);

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.RegistrarEvento(2, idUsuario);

                iniciosSesionFallidos += 1;
                if (iniciosSesionFallidos >= 3)
                {
                    miDAL.BloquearUsuario(idUsuario);
                }
                miDAL.UpdateCantidadIniciosSesion(idUsuario, iniciosSesionFallidos);

                //Aca voy por el constructor directamente y no por el acceso del singleton, porque este usuario no esta en condiciones de iniciar sesion
                UsuarioBLL usuario = new UsuarioBLL(DTO.nombre, DTO.apellido, DTO.dni, DTO.domicilio, DTO.email, null, null, DTO.usuarioOculto, DTO.fallosAutenticacionConsecutivos, DTO.bloqueado);

                int verificador_horizontal = CalcularVerificadorHorizontal(idUsuario, usuario, DTO.password, iniciosSesionFallidos);
                bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Persona", idUsuario, verificador_horizontal);
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
                return false;

            }
            else {
                //Aca uso getUsuario porque ya el usuario esta ok para iniciar sesion, entonces cargo el singleton
                UsuarioBLL usuarioSingleton = GetUsuarioBLL(DTO.nombre, DTO.apellido, DTO.dni, DTO.domicilio, DTO.email, null, null, DTO.usuarioOculto, 0, DTO.bloqueado);
                miDAL.UpdateCantidadIniciosSesion(idUsuario, 0);
                int verificador_horizontal = CalcularVerificadorHorizontal(idUsuario, usuarioSingleton, DTO.password, 0);
                bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Persona", idUsuario, verificador_horizontal);
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
                concatenado += "False";
            else
                concatenado += "True";
            concatenado += fallos_autenticacion_consecutivos.ToString();
            if (usuario.bloqueado)
                concatenado += "True";
            else
                concatenado += "False";


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
