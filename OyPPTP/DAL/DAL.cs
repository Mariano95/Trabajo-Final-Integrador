using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DAL
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    ATRIBUTOS     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private static DAL singleton;
        private byte[] key;
        private byte[] IV;
        private SqlConnection miConnection;

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR     //////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static DAL GetDAL(){
            if (singleton == null) {
                singleton = new DAL();
            }
            return singleton;
        }

        private DAL() {    
            AesManaged aes = new AesManaged();
            this.key = System.Convert.FromBase64String("LVz1Hiu7b1K7aQ2a4WeieE+b3tvrEUcI/nvOunFLzNk=");
            this.IV = System.Convert.FromBase64String("Kr1qHrXodi5Vf0eDwYCt9Q==");
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////    METODOS PUBLICOS CRIPTOGRAFIA     //////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public string EncriptarAES(string raw) {
            byte[] encrypted = Encrypt(raw, this.key, this.IV);
            return System.Convert.ToBase64String(encrypted);
        }

        public string DesencriptarAES(string encryptedb64)
        {
            byte[] encrypted = System.Convert.FromBase64String(encryptedb64);
            string decrypted = Decrypt(encrypted, this.key, this.IV);
            return decrypted;
        }

        public string EncriptarMD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    METODOS GENERICOS BD     ////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////


        public bool ConectarBD(string stringConexion) {
            this.miConnection = new SqlConnection(stringConexion);
            return conectar();
        }

        public bool ActualizarVerificadorHorizontal(string tabla, int id, int verificador_horizontal)
        {

            string updateCommandText = "" +
                "UPDATE [dbo].[" + tabla + "]" +
                "SET " +
                    "[" + tabla + "_verificador_horizontal] = @verificador_horizontal " +
                "WHERE " +
                    tabla + "_id = @id";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            updateCommand.Parameters.AddWithValue("@id", id);
            ExecuteNonQuery(updateCommand);
            return true;


        }

        public int ObtenerSumaVerificadoresHorizontales(string tabla)
        {
            string selectCommandText = "" +
                "SELECT SUM([" + tabla + "_verificador_horizontal]) " +
                "FROM [dbo].[" + tabla + "]";

            return ExecuteScalar(selectCommandText);
        }

        public bool ActualizarVerificadorVertical(string tabla, int sumaVerificadoresHorizontales)
        {

            string updateCommandText = "" +
                "UPDATE [dbo].[Verificadores_Verticales]" +
                "SET " +
                    "[verificadores_verticales_numero] = @sumaVerificadoresHorizontales " +
                "WHERE " +
                    "verificadores_verticales_tabla = @tabla";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@sumaVerificadoresHorizontales", EncriptarAES(sumaVerificadoresHorizontales.ToString()));
            updateCommand.Parameters.AddWithValue("@tabla", tabla);
            ExecuteNonQuery(updateCommand);
            return true;

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////    METODOS INICIALIZACION BD     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public void IncializarEventos() {
            
            int id = 1;
            string nombre = EncriptarAES("Inicio de sesion exitoso");
            string descripcion = EncriptarAES("El usuario ha iniciado sesion en el portal de servicios exitosamente");
            int criticidad = 1;
            string concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 2;
            nombre = EncriptarAES("Inicio de sesion fallido");
            descripcion = EncriptarAES("El usuario fallo al iniciar sesion en el portal de servicios");
            criticidad = 2;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 3;
            nombre = EncriptarAES("Usuario bloqueado por maximo de inicios de sesion fallidos consecutivos");
            descripcion = EncriptarAES("El usuario fallo al iniciar sesion en el portal de servicios tres veces seguidas, por lo tanto fue bloqueado");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 4;
            nombre = EncriptarAES("Fin de sesion de usuario");
            descripcion = EncriptarAES("El usuario finalizo sesion en el portal de servicios");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 5;
            nombre = EncriptarAES("Ocultamiento de un usuario");
            descripcion = EncriptarAES("El usuario se oculto y ya no podra visualizarse en el portal");
            criticidad = 2;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 6;
            nombre = EncriptarAES("Reactivacion de un usuario");
            descripcion = EncriptarAES("El usuario se reactivo y nuevamente puede visualizarse en el portal");
            criticidad = 2;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 7;
            nombre = EncriptarAES("Cambio de contraseña de un usuario");
            descripcion = EncriptarAES("El usuario modifico su contraseña");
            criticidad = 2;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 8;
            nombre = EncriptarAES("Irregularidad en la base de datos");
            descripcion = EncriptarAES("Fallo la validacion de integridad de los datos");
            criticidad = 5;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 9;
            nombre = EncriptarAES("Verificacion exitosa de la integridad de la base de datos");
            descripcion = EncriptarAES("Se valido con exito la integridad de los datos");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 10;
            nombre = EncriptarAES("Impresion de la bitacora");
            descripcion = EncriptarAES("Se ha realizado una impresion de los datos de la bitacora");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 11;
            nombre = EncriptarAES("Creacion de un backup de la base de datos del sistema");
            descripcion = EncriptarAES("Se ha realizado un backup de la base de datos del sistema");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 12;
            nombre = EncriptarAES("Restauracion de la base de datos");
            descripcion = EncriptarAES("Se ha restaurado la base de datos del sistema");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 13;
            nombre = EncriptarAES("Creacion de un grupo de usuarios");
            descripcion = EncriptarAES("Se ha creado un nuevo grupo de usuarios");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 14;
            nombre = EncriptarAES("Eliminacion de un grupo de usuarios");
            descripcion = EncriptarAES("Se ha eliminado un grupo de usuarios");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 15;
            nombre = EncriptarAES("Usuario agregado a grupo");
            descripcion = EncriptarAES("Se ha agregado al usuario a un grupo");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 16;
            nombre = EncriptarAES("Usuario eliminado de grupo");
            descripcion = EncriptarAES("Se ha eliminado al usuario de un grupo");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 17;
            nombre = EncriptarAES("Nueva patente agregada a un usuario");
            descripcion = EncriptarAES("Se ha agregado una nueva patente al usuario");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 18;
            nombre = EncriptarAES("Patente removida a usuario");
            descripcion = EncriptarAES("Se ha eliminado una patente al usuario");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 19;
            nombre = EncriptarAES("Patente agregada a grupo");
            descripcion = EncriptarAES("Se ha agregado una patente a un grupo");
            criticidad = 3;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 20;
            nombre = EncriptarAES("Patente removida de un grupo");
            descripcion = EncriptarAES("Se ha eliminado una patente de un grupo");
            criticidad = 4;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 21;
            nombre = EncriptarAES("Nueva citacion creada");
            descripcion = EncriptarAES("Se ha creado una nueva citacion");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 22;
            nombre = EncriptarAES("Citacion rechazada");
            descripcion = EncriptarAES("Usuario rechazo citacion");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 23;
            nombre = EncriptarAES("Citacion cancelada");
            descripcion = EncriptarAES("Usuario cancelo citacion");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 24;
            nombre = EncriptarAES("Citacion cumplida");
            descripcion = EncriptarAES("Usuario cumplio citacion");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            id = 25;
            nombre = EncriptarAES("Cambio de fechas de citacion");
            descripcion = EncriptarAES("Hubo una modificacion en la fecha de la citacion");
            criticidad = 1;
            concatenado = "";
            concatenado = id.ToString() + nombre + descripcion + criticidad.ToString();
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertEvento(id, nombre, descripcion, criticidad, verificador_horizontal);

            int sumaVerificadoresHorizontales = ObtenerSumaVerificadoresHorizontales("Evento");
            bool verificador_vertical_ok = ActualizarVerificadorVertical("Evento", sumaVerificadoresHorizontales);

        }

        private void InsertEvento(int id, string nombre, string descripcion, int criticidad, int verificador_horizontal) {
            string insertCommandText = "" +
                    "INSERT INTO [dbo].[Evento] " +
                        "([evento_id]" +
                        ",[evento_nombre]" +
                        ",[evento_descripcion]" +
                        ",[evento_criticidad]" +
                        ",[evento_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@nombre," +
                        "@descripcion," +
                        "@criticidad," +
                        "@verificador_horizontal)";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);

            insertCommand.Parameters.AddWithValue("@id", id);
            insertCommand.Parameters.AddWithValue("@nombre", nombre);
            insertCommand.Parameters.AddWithValue("@descripcion", descripcion);
            insertCommand.Parameters.AddWithValue("@criticidad", criticidad);
            insertCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            ExecuteNonQuery(insertCommand);
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    METODOS USUARIO     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public UsuarioDTO GetUsuarioById(int usuarioId)
        {
            UsuarioDTO usuario = new UsuarioDTO();

            string selectCommandText = "" +
                "SELECT * " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_id = @Id";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@Id", usuarioId);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                usuario.nombre = (string)reader.GetValue(1);
                usuario.apellido = (string)reader.GetValue(2);
                usuario.dni = (string)reader.GetValue(3);
                usuario.domicilio = (string)reader.GetValue(4);
                usuario.email = (string)reader.GetValue(5);
                usuario.usuarioOculto = (bool)reader.GetValue(7) == false;
                usuario.fallosAutenticacionConsecutivos = (int)reader.GetValue(8);
                usuario.bloqueado = (bool)reader.GetValue(9);
            }

            CloseReader(reader);
            return usuario;
        }
        
        public bool BuscarUsuario(string dni, string email) { 
            string query =  "" +
                "SELECT * " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_dni = '" + dni + "' AND persona_email = '" + email + "'";

            return ExecuteScalar(query) != -1;

        }

        public int BuscarUsuarioPorMail(string emailEncrypted) {
            string query = "" +
                "SELECT persona_id " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_email = '" + emailEncrypted + "'";

            return ExecuteScalar(query);
        }

        public bool ValidarUsuarioBloqueado(int idUsuario) {
            string query = "" +
                "SELECT persona_id " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_id = '" + idUsuario + "' AND persona_bloqueada = 0";

            return ExecuteScalar(query) != 1;
        }

        public bool ValidarPasswordUsuario(int idUsuario, string passwordEncrypted) {
            string query = "" +
                "SELECT persona_id " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_password = '" + passwordEncrypted + "' AND persona_id = '" + idUsuario + "'";

            return ExecuteScalar(query) != -1;
        }

        public int IniciosSesionFallidos(int idUsuario) {
            string query = "" +
                "SELECT persona_fallos_autenticacion_consecutivos " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_id = '" + idUsuario + "'";

            return ExecuteScalar(query);
        }

        public bool BloquearUsuario(int idUsuario) {
            string updateCommandText = "" +
                "UPDATE [dbo].[Persona]" +
                "SET " +
                    "[persona_bloqueada] = @persona_bloqueada " +
                "WHERE " +
                    "persona_id = @id";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@persona_bloqueada", true);
            updateCommand.Parameters.AddWithValue("@id", idUsuario);
            ExecuteNonQuery(updateCommand);
            return true;
        }

        public bool UpdateCantidadIniciosSesion(int idUsuario, int iniciosSesionFallidos) {
            string updateCommandText = "" +
                "UPDATE [dbo].[Persona]" +
                "SET " +
                    "[persona_fallos_autenticacion_consecutivos] = @persona_fallos_autenticacion_consecutivos " +
                "WHERE " +
                    "persona_id = @id";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@persona_fallos_autenticacion_consecutivos", iniciosSesionFallidos);
            updateCommand.Parameters.AddWithValue("@id", idUsuario);
            ExecuteNonQuery(updateCommand);
            return true;
        }

        public int InsertarUsuario(string nombre, string apellido, string dni, string domicilio, string email, string password) {
            try
            {

                string insertCommandText = "" +
                    "INSERT INTO [dbo].[Persona] " +
                        "([persona_id]" +
                        ",[persona_nombre]" +
                        ",[persona_apellido]" +
                        ",[persona_dni]" +
                        ",[persona_domicilio_descripcion]" +
                        ",[persona_email]" +
                        ",[persona_password]" +
                        ",[persona_activa]" +
                        ",[persona_fallos_autenticacion_consecutivos]" +
                        ",[persona_bloqueada]" +
                        ",[persona_verificador_horizontal]) " +
                    "VALUES " +
                        "(@PersonaId," +
                        "@PersonaNombre," +
                        "@PersonaApellido," +
                        "@PersonaDni," +
                        "@PersonaDomicilioDescripcion," +
                        "@PersonaEmail," +
                        "@PersonaPassword," +
                        "@PersonaActiva," +
                        "@PersonaFallosAutenticacionConsecutivos," +
                        "@PersonaBloqueada," +
                        "@PersonaVerificadorHorizontal)";

                SqlCommand insertCommand = new SqlCommand(insertCommandText);

                int id = ObtenerUltimoId("Persona") + 1;
                insertCommand.Parameters.AddWithValue("@PersonaId", id);
                insertCommand.Parameters.AddWithValue("@PersonaNombre", nombre);
                insertCommand.Parameters.AddWithValue("@PersonaApellido", apellido);
                insertCommand.Parameters.AddWithValue("@PersonaDni", dni);
                insertCommand.Parameters.AddWithValue("@PersonaDomicilioDescripcion", domicilio);
                insertCommand.Parameters.AddWithValue("@PersonaEmail", email);
                insertCommand.Parameters.AddWithValue("@PersonaPassword", password);
                insertCommand.Parameters.AddWithValue("@PersonaActiva", true);
                insertCommand.Parameters.AddWithValue("@PersonaFallosAutenticacionConsecutivos", 0);
                insertCommand.Parameters.AddWithValue("@PersonaBloqueada", false);
                insertCommand.Parameters.AddWithValue("@PersonaVerificadorHorizontal", 0);

                ExecuteNonQuery(insertCommand);
                return id;
            }
            catch (Exception e) {
                return -1;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    METODOS BITACORA     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public int InsertarEventoBitacora(int evento, int idUsuario, DateTime fechaEvento)
        {
            try
            {
                string insertCommandText = "" +
                    "INSERT INTO [dbo].[Bitacora] " +
                        "([bitacora_id]" +
                        ",[bitacora_usuario]" +
                        ",[bitacora_hora]" +
                        ",[bitacora_evento]" +
                        ",[bitacora_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@usuario," +
                        "@hora," +
                        "@evento," +
                        "@verificador_horizontal)";

                SqlCommand insertCommand = new SqlCommand(insertCommandText);

                int id = ObtenerUltimoId("Bitacora") + 1;
                insertCommand.Parameters.AddWithValue("@id", id);
                insertCommand.Parameters.AddWithValue("@usuario", idUsuario);
                insertCommand.Parameters.AddWithValue("@hora", fechaEvento);
                insertCommand.Parameters.AddWithValue("@evento", evento);
                insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);
                ExecuteNonQuery(insertCommand);
                return id;
            }
            catch (Exception e) {
                return -1;
            }
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    METODOS PRIVADOS DE BD     /////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        //Prueba la conexion y devuelve resultado acorde
        private bool conectar()
        {
            try {
                this.miConnection.Open();
                this.miConnection.Close();
                return true;
            }
            catch (Exception e){
                return false;
            }   
        }

        private SqlDataReader ExecuteReader(SqlCommand command)
        {
            command.Connection = this.miConnection;
            this.miConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        private void CloseReader(SqlDataReader reader)
        {
            reader.Close();
            if (this.miConnection.State.ToString() == "Open")
            {
                this.miConnection.Close();
            }

        }

        private void ExecuteNonQuery(SqlCommand command)
        {
            command.Connection = this.miConnection;
            this.miConnection.Open();
            command.ExecuteNonQuery();
            this.miConnection.Close();
        }

        private int ExecuteScalar(string selectCommandText)
        {
            SqlCommand selectCommand = new SqlCommand(selectCommandText, this.miConnection);
            this.miConnection.Open();
            Object qryResult = selectCommand.ExecuteScalar();
            this.miConnection.Close();
            if (qryResult == null)
            {
                return -1;
            }
            else
            {
                return (int)qryResult;
            }
        }

        private int ObtenerUltimoId(string nombreTabla)
        {
            string selectCommandText = "" +
                "SELECT COALESCE(MAX(" + nombreTabla.ToLower() + "_id" + "),0) " +
                "FROM [dbo]." + nombreTabla;

            return this.ExecuteScalar(selectCommandText);
        }

        private int ObtenerCantidadFilas(string nombreTabla)
        {
            string selectCommandText = "" +
                "SELECT COUNT(*) " +
                "FROM [dbo]." + nombreTabla;

            return this.ExecuteScalar(selectCommandText);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////    METODOS PRIVADOS DE CRIPTOGRAFIA    //////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                    // to encrypt
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
            return encrypted;
        }

        private string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

    }
}
