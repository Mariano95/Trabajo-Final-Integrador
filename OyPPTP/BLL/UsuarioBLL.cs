﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using DAL;
using DTO;
using SL;
using BLL;
using DTO;

namespace BLL
{
    public class UsuarioBLL
    {
        private static UsuarioBLL usuario_singleton;
        public int id { get; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public string domicilio { get; set; }
        public string email { get; set; }
        private string password { get; set; }
        private string rol { get; set; }
        private List<String> servicios { get; set; }
        private bool usuarioOculto { get; set; }
        private int fallosAutenticacionConsecutivos { get; set; }
        private bool bloqueado { get; set; }

        public static UsuarioBLL GetUsuarioBLL(int id, string nombre, string apellido, string dni, string domicilio, string email, string password, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
        {
            if (usuario_singleton == null)
            {
                usuario_singleton = new UsuarioBLL(id, nombre, apellido, dni, domicilio, email, password, rol, servicios, usuarioOculto, fallosAutenticacionConsecutivos, bloqueado);
            }
            return usuario_singleton;
        }

        public static UsuarioBLL GetUsuarioBLL(int id, string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
        {
            if (usuario_singleton == null)
            {
                usuario_singleton = new UsuarioBLL(id, nombre, apellido, dni, domicilio, email, rol, servicios, usuarioOculto, fallosAutenticacionConsecutivos, bloqueado);
            }
            return usuario_singleton;
        }

        public static UsuarioBLL GetUsuarioBLL()
        {
            if (usuario_singleton == null)
            {
                usuario_singleton = new UsuarioBLL();
            }
            return usuario_singleton;
        }

        public static void ResetSingleton() {
            usuario_singleton = null;
        }

        private UsuarioBLL(int id, string nombre, string apellido, string dni, string domicilio, string email, string password, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
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

        private UsuarioBLL(int id, string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado) {
            this.id = id;
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

        private UsuarioBLL(string nombre, string apellido, string dni, string domicilio, string email, string rol, List<string> servicios, bool usuarioOculto, int fallosAutenticacionConsecutivos, bool bloqueado)
        {
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

        private UsuarioBLL()
        {
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

        public static List<(int, string, string, string)> GetUsuarios() {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaUsuarios();
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

        public static bool BuscarOtroUsuario(int id, string dni, string email) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<int> idUsuarios = miDAL.BuscarIdUsuario(dni, email);
            if (idUsuarios.Count > 1) {
                return true;
            }
            if (idUsuarios.Count == 1 && ! idUsuarios.Contains(id)) {
                return true;
            }
            int a = 1;
            return false;
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
                DTO.fallosAutenticacionConsecutivos += 1;
                if (iniciosSesionFallidos >= 3)
                {
                    miDAL.BloquearUsuario(idUsuario);
                    DTO.bloqueado = true;
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
                UsuarioBLL usuarioSingleton = GetUsuarioBLL(idUsuario, DTO.nombre, DTO.apellido, DTO.dni, DTO.domicilio, DTO.email, null, null, DTO.usuarioOculto, 0, DTO.bloqueado);
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

        public static List<(int, string)> GetPatentesUsuario(int usuarioId) {
            GestorPatentes gestorPatentes = new GestorPatentes();

            List<(int, string)> patentes = new List<(int, string)>();
            patentes.AddRange(gestorPatentes.GetPatentesUsuarioGrupo(usuarioId));

            return patentes;

        }

        public static (bool,string) QuitarPatente(int usuarioId, int patenteId) {
            GestorPatentes gestorPatentes = new GestorPatentes();
            (bool, string) result = gestorPatentes.PuedeQuitar(usuarioId, patenteId);
            if (!result.Item1) {
                return (false, result.Item2);
            }
            gestorPatentes.QuitarPatente(usuarioId, patenteId);

            UsuarioBLL usuarioResponsable = UsuarioBLL.GetUsuarioBLL();
            
            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(18, usuarioResponsable.id, id_patente: patenteId, id_usuario_afectado: usuarioId);

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Patente");
            bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Patente", sumaVerificadoresHorizontales);

            return (true, "Exito al quitar la patente");
        }

        public static (bool, string) AgregarPatente(int usuarioId, int patenteId)
        {
            UsuarioBLL usuarioResponsable = UsuarioBLL.GetUsuarioBLL();
            
            GestorPatentes gestorPatentes = new GestorPatentes();
            gestorPatentes.AgregarPatente(usuarioId, patenteId);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(17, usuarioResponsable.id, id_patente: patenteId, id_usuario_afectado: usuarioId);

            int sumaVerificadoresHorizontales = 0;
            bool verificador_vertical_ok;
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Patente");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Patente", sumaVerificadoresHorizontales);

            string concatenado = "";
            concatenado += usuarioId.ToString();
            concatenado += patenteId.ToString();
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            
            bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontalPersonaPatente(usuarioId, patenteId, verificador_horizontal);
            
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Patente");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Patente", sumaVerificadoresHorizontales);
            

            return (true, "Exito al agregar la patente");
        }

        public static bool DesbloquearUsuario(string mailEncrypted) {
            int idUsuario = BuscarUsuarioPorMail(mailEncrypted);
            if (idUsuario == -1) {
                return false;
            }

            Desbloquear(idUsuario);

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            UsuarioDTO DTO = miDAL.GetUsuarioById(idUsuario);
            UsuarioBLL usuario = new UsuarioBLL(DTO.nombre, DTO.apellido, DTO.dni, DTO.domicilio, DTO.email, null, null, DTO.usuarioOculto, DTO.fallosAutenticacionConsecutivos, DTO.bloqueado);

            int verificador_horizontal = CalcularVerificadorHorizontal(idUsuario, usuario, DTO.password, 0);
            miDAL.ActualizarVerificadorHorizontal("Persona", idUsuario, verificador_horizontal);
            
            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona");
            miDAL.ActualizarVerificadorVertical("Persona", sumaVerificadoresHorizontales);

            return true;

        }

        public static bool ActualizarUsuario(int usuarioId, string nuevoPassword, bool logeado = true) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.ActualizarUsuario(usuarioId, nuevoPassword);

            int verificador_horizontal;
            int sumaVerificadoresHorizontales;
            UsuarioBLL usuario;

            if (logeado)
            {
                usuario = UsuarioBLL.GetUsuarioBLL();
            }
            else {
                UsuarioDTO DTO = miDAL.GetUsuarioById(usuarioId);
                usuario = new UsuarioBLL(DTO.nombre, DTO.apellido, DTO.dni, DTO.domicilio, DTO.email, null, null, DTO.usuarioOculto, DTO.fallosAutenticacionConsecutivos, DTO.bloqueado);

            }
            
            verificador_horizontal = CalcularVerificadorHorizontal(usuarioId, usuario, nuevoPassword, usuario.fallosAutenticacionConsecutivos);
            miDAL.ActualizarVerificadorHorizontal("Persona", usuarioId, verificador_horizontal);

            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona");
            miDAL.ActualizarVerificadorVertical("Persona", sumaVerificadoresHorizontales);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(7, usuarioId);

            return true;
        }

        public static bool ActualizarUsuario(int usuarioId, string nombreEncrypted, string apellidoEncrypted, string dniEncrypted, string domicilioEncrypted, string emailEncrypted) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            bool exito = miDAL.ActualizarUsuario(usuarioId, nombreEncrypted, apellidoEncrypted, dniEncrypted, domicilioEncrypted, emailEncrypted);
            if (!exito) {
                return false;
            }

            UsuarioBLL usuario = UsuarioBLL.GetUsuarioBLL();

            usuario.nombre = nombreEncrypted;
            usuario.apellido = apellidoEncrypted;
            usuario.dni = dniEncrypted;
            usuario.domicilio = domicilioEncrypted;
            usuario.email = emailEncrypted;
            int verificador_horizontal = CalcularVerificadorHorizontal(usuarioId, usuario, usuario.password, usuario.fallosAutenticacionConsecutivos);
            miDAL.ActualizarVerificadorHorizontal("Persona", usuarioId, verificador_horizontal);

            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona");
            miDAL.ActualizarVerificadorVertical("Persona", sumaVerificadoresHorizontales);

            return true;
        }

        public static (bool, string) EnviarMailUsuario(int idUsuario, string nuevaPassword) {
            try
            {
                //Recuperando usuario
                DAL.DAL miDAL = DAL.DAL.GetDAL();
                UsuarioDTO usuarioDTO = miDAL.GetUsuarioById(idUsuario);
                string email = miDAL.DesencriptarAES(usuarioDTO.email); 
                
                //Enviando mail al usuario
                string remitente = "portal.ati@outlook.com";
                string password = "atipass2023";
                string destinatario = email;
                string asunto = "Portal de servicios ATI - Contraseña restaurada";
                string cuerpo = "Tu nueva contraseña es " + nuevaPassword;

                SmtpClient clienteSmtp = new SmtpClient("smtp.office365.com", 587);
                clienteSmtp.EnableSsl = true;
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.Credentials = new NetworkCredential(remitente, password);

                MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpo);
                clienteSmtp.Send(mensaje);
                return (true, "Correo enviado");
            }
            catch (Exception ex) {
                return (false, ex.Message);
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

        private static void Desbloquear(int idUsuario) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.DesbloquearUsuario(idUsuario);
        }

    }
}
