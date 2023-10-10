using System;
using System.Collections.Generic;


namespace DTO
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string domicilio { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        public List<String> servicios { get; set; }
        public bool usuarioOculto { get; set; }
        public int fallosAutenticacionConsecutivos { get; set; }
        public bool bloqueado { get; set; }

        public UsuarioDTO() { }

        public UsuarioDTO(int id, string nombre, string apellido, string dni, string domicilio, string email, string password, string rol, List<String> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.domicilio = domicilio;
            this.email = email;
            this.password = password;
            this.rol = rol;
            this.servicios = servicios;
            this.usuarioOculto = usuarioOculto;
            this.fallosAutenticacionConsecutivos = fallosAutenticacionConsecutivos;
            this.bloqueado = bloqueado;
        }

    }
}
