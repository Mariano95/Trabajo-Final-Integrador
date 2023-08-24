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

        public static bool AltaUsuario(string nombre, string apellido, string dni, string domicilio, string email, string pasword) {
            usuario_singleton = new UsuarioBLL(nombre, apellido, dni, domicilio, email, null, null, false, false);
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.InsertarUsuario(nombre, apellido, dni, domicilio, email, pasword);
        }

        public static bool BuscarUsuario(string dni, string email)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.BuscarUsuario(dni, email);
        }

        



    }
}
