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

        public static DAL GetDAL() {
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

        public bool ActualizarVerificadorHorizontalPersonaPatente(int idPersona, int idPatente, int verificador_horizontal)
        {

            string updateCommandText = "" +
                "UPDATE [dbo].[Persona_Patente]" +
                "SET " +
                    "[persona_patente_verificador_horizontal] = @verificador_horizontal " +
                "WHERE " +
                    "persona_patente_persona_id = @persona AND persona_patente_patente_id = @patente";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            updateCommand.Parameters.AddWithValue("@persona", idPersona);
            updateCommand.Parameters.AddWithValue("@patente", idPatente);
            ExecuteNonQuery(updateCommand);
            return true;


        }

        public bool ActualizarVerificadorHorizontalGrupoPatente(int idGrupo, int idPatente, int verificador_horizontal)
        {

            string updateCommandText = "" +
                "UPDATE [dbo].[Grupo_Patente]" +
                "SET " +
                    "[grupo_patente_verificador_horizontal] = @verificador_horizontal " +
                "WHERE " +
                    "grupo_patente_grupo_id = @grupo AND grupo_patente_patente_id = @patente";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            updateCommand.Parameters.AddWithValue("@grupo", idGrupo);
            updateCommand.Parameters.AddWithValue("@patente", idPatente);
            ExecuteNonQuery(updateCommand);
            return true;


        }

        public bool ActualizarVerificadorHorizontalPersonaGrupo(int idPersona, int idGrupo, int verificador_horizontal)
        {

            string updateCommandText = "" +
                "UPDATE [dbo].[Persona_Grupo]" +
                "SET " +
                    "[persona_grupo_verificador_horizontal] = @verificador_horizontal " +
                "WHERE " +
                    "persona_grupo_persona_id = @persona AND persona_grupo_grupo_id = @grupo";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            updateCommand.Parameters.AddWithValue("@persona", idPersona);
            updateCommand.Parameters.AddWithValue("@grupo", idGrupo);
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

        public List<(string, int)> SumaVerificadoresHorizontalesPorTabla() {

            List<string> tablas = new List<string>();
            List<(string, int)> resultado = new List<(string, int)>();

            string selectCommandText = "" +
                "SELECT verificadores_verticales_tabla " +
                "FROM [dbo].[Verificadores_Verticales]";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                tablas.Add((string)reader.GetValue(0));
            }
            CloseReader(reader);

            string selectCommandText2;
            SqlCommand selectCommand2;
            SqlDataReader reader2;
            string concatenado;
            int sumaVerificadoresHorizontales;
            int contador;
            foreach (string tabla in tablas) {
                selectCommandText2 = "" +
                    "SELECT * " +
                    "FROM [dbo].[" + tabla + "]";

                selectCommand2 = new SqlCommand(selectCommandText2);
                reader2 = ExecuteReader(selectCommand2);
                sumaVerificadoresHorizontales = 0;

                while (reader2.Read())
                {
                    concatenado = "";
                    //Voy hasta el anteultimo campo inclusive porque no quiero agarrar el ultimo que es el verificador horizontal
                    for (int i = 0; i < reader2.FieldCount - 1; i++) {
                        concatenado += reader2.GetValue(i).ToString();
                        if (tabla == "Bitacora")
                        {
                            continue;
                        }
                    }

                    contador = 1;
                    foreach (char c in concatenado) {
                        sumaVerificadoresHorizontales += (int)c * contador;
                        contador++;
                    }
                }
                CloseReader(reader);

                //Ignoro a las tablas que no tienen ni un registro
                if (sumaVerificadoresHorizontales > 0) {
                    resultado.Add((tabla, sumaVerificadoresHorizontales));
                }
            }

            return resultado;
        }

        public bool CoincideVerificadorVertical(int digito, string tabla) {
            string selectCommandText = "" +
                "SELECT verificadores_verticales_numero " +
                "FROM [dbo].[Verificadores_Verticales] " +
                "WHERE verificadores_verticales_tabla = @tabla";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@tabla", tabla);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                string verificador_vertical_unencrypted = DesencriptarAES((string)reader.GetValue(0));
                if (verificador_vertical_unencrypted != digito.ToString()) {
                    CloseReader(reader);
                    return false;
                }
            }
            CloseReader(reader);
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

        public void InicializarPatentes() {
            int id = 1;
            string nombre = "Backup cancelar";
            string idPantalla = "Backup";
            string idControl = "cancelar";
            string concatenado = id.ToString() + nombre + idPantalla + idControl;
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 2;
            nombre = "Backup iniciar";
            idPantalla = "Backup";
            idControl = "iniciar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 3;
            nombre = "Bitacora cerrar";
            idPantalla = "Bitacora";
            idControl = "cerrar_bitacora";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 4;
            nombre = "Bitacora cambiar filtro";
            idPantalla = "Bitacora";
            idControl = "cambiar_filtros";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 5;
            nombre = "Bitacora imprimir";
            idPantalla = "Bitacora";
            idControl = "imprimir";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 6;
            nombre = "Buscar trabajadores";
            idPantalla = "BuscarTrabajadores";
            idControl = "buscar_trabajadores";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 7;
            nombre = "Calificar usuario guardar";
            idPantalla = "CalificarUsuario";
            idControl = "guardar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 8;
            nombre = "Cambiar password";
            idPantalla = "CambioPassword";
            idControl = "guardar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 9;
            nombre = "Cambiar estado citacion";
            idPantalla = "CambioEstadoCitacion";
            idControl = "guardar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 10;
            nombre = "Cargar servicios continuar";
            idPantalla = "CargarServicios";
            idControl = "continuar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 11;
            nombre = "Citar trabajador";
            idPantalla = "CitarTrabajador";
            idControl = "generar_citacion";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 12;
            nombre = "Comentar citacion";
            idPantalla = "ComentarCitacion";
            idControl = "agregar_comentario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 13;
            nombre = "Credenciales bd conectar";
            idPantalla = "CredencialesBD";
            idControl = "conectar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 14;
            nombre = "Desbloquear usuario";
            idPantalla = "DesbloquearUsuario";
            idControl = "desbloquear_usuario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 15;
            nombre = "Grupo usuario eliminar";
            idPantalla = "GruposUsuarios";
            idControl = "eliminar_grupo";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 16;
            nombre = "Grupo usuario crear";
            idPantalla = "GruposUsuarios";
            idControl = "crear_grupo";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 17;
            nombre = "Grupo usuario quitar usuario";
            idPantalla = "GruposUsuarios";
            idControl = "quitar_del_grupo";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 18;
            nombre = "Grupo usuario agregar usuario";
            idPantalla = "GruposUsuarios";
            idControl = "agregar_al_grupo";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 19;
            nombre = "Indicar usuario particular";
            idPantalla = "IndicarTipoUsuario";
            idControl = "particular";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 20;
            nombre = "Indicar usuario trabajador";
            idPantalla = "IndicarTipoUsuario";
            idControl = "trabajador";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 21;
            nombre = "Iniciar sesion";
            idPantalla = "IniciarSesion";
            idControl = "iniciar_sesion";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 22;
            nombre = "Ocultar usuario si";
            idPantalla = "ModalOcultar";
            idControl = "si";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 23;
            nombre = "Ocultar usuario no";
            idPantalla = "ModalOcultar";
            idControl = "no";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 24;
            nombre = "Pantalla inicial modificar servicios";
            idPantalla = "PantallaInicial";
            idControl = "modificarServicios";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 25;
            nombre = "Pantalla inicial modificar datos personales";
            idPantalla = "PantallaInicial";
            idControl = "modificarDatosPersonales";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 26;
            nombre = "Pantalla inicial modificar password";
            idPantalla = "PantallaInicial";
            idControl = "modificarPassword";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 27;
            nombre = "Pantalla inicial ocultar usuario";
            idPantalla = "PantallaInicial";
            idControl = "ocultarUsuario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 28;
            nombre = "Pantalla inicial buscar trabajadores";
            idPantalla = "PantallaInicial";
            idControl = "buscarTrabajadores";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 29;
            nombre = "Pantalla inicial citaciones recibidas";
            idPantalla = "PantallaInicial";
            idControl = "citacionesRecibidas";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 30;
            nombre = "Pantalla inicial citaciones enviadas";
            idPantalla = "PantallaInicial";
            idControl = "citacionesEnviadas";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 31;
            nombre = "Pantalla inicial bitacora";
            idPantalla = "PantallaInicial";
            idControl = "bitacora";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 32;
            nombre = "Pantalla inicial restaurar sistema";
            idPantalla = "PantallaInicial";
            idControl = "restaurarSistema";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 33;
            nombre = "Pantalla inicial desbloquear usuario";
            idPantalla = "PantallaInicial";
            idControl = "desbloquearUsuario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 34;
            nombre = "Pantalla inicial patentes por grupo";
            idPantalla = "PantallaInicial";
            idControl = "patentesPorGrupo";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 35;
            nombre = "Pantalla inicial patentes por usuario";
            idPantalla = "PantallaInicial";
            idControl = "patentesPorUsuario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 36;
            nombre = "Pantalla inicial grupos de usuarios";
            idPantalla = "PantallaInicial";
            idControl = "gruposUsuarios";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 37;
            nombre = "Pantalla inicial crear usuario administrador";
            idPantalla = "PantallaInicial";
            idControl = "crearUsuarioAdmin";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 38;
            nombre = "Pantalla inicial cambiar idioma";
            idPantalla = "PantallaInicial";
            idControl = "cambiarIdioma";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 39;
            nombre = "Pantalla inicial cerrar sesion";
            idPantalla = "PantallaInicial";
            idControl = "cerrarSesion";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 40;
            nombre = "Patentes por grupo quitar patente";
            idPantalla = "PatentesPorGrupo";
            idControl = "quitarPatente";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 41;
            nombre = "Patentes por grupo otorgar patente";
            idPantalla = "PatentesPorGrupo";
            idControl = "otorgarPatente";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 42;
            nombre = "Patentes por grupo volver";
            idPantalla = "PatentesPorGrupo";
            idControl = "volver";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 43;
            nombre = "Patentes por usuario quitar patente";
            idPantalla = "PatentesPorUsuario";
            idControl = "quitarPatente";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 44;
            nombre = "Patentes por usuario otorgar patente";
            idPantalla = "PatentesPorUsuario";
            idControl = "otorgarPatente";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 45;
            nombre = "Patentes por usuario volver";
            idPantalla = "PatentesPorUsuario";
            idControl = "volver";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 46;
            nombre = "Pre bitacora buscar";
            idPantalla = "PreBitacora";
            idControl = "buscar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 47;
            nombre = "Pre login iniciar sesion";
            idPantalla = "PreLogin";
            idControl = "iniciar_sesion";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 48;
            nombre = "Pre login registrar usuario";
            idPantalla = "PreLogin";
            idControl = "registrar_usuario";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 49;
            nombre = "Pre login restablecer contrasena";
            idPantalla = "PreLogin";
            idControl = "olvide_mi_contrasena";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 50;
            nombre = "Registrar usuario continuar";
            idPantalla = "RegistrarUsuario";
            idControl = "continuar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 51;
            nombre = "Restaurar sistema cancelar";
            idPantalla = "RestaurarSistema";
            idControl = "cancelar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            id = 52;
            nombre = "Restaurar sistema iniciar";
            idPantalla = "RestaurarSistema";
            idControl = "iniciar";
            concatenado = id.ToString() + nombre + idPantalla + idControl;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPatente(id, nombre, idPantalla, idControl, verificador_horizontal);

            int sumaVerificadoresHorizontales = ObtenerSumaVerificadoresHorizontales("Patente");
            bool verificador_vertical_ok = ActualizarVerificadorVertical("Patente", sumaVerificadoresHorizontales);

        }

        private void InsertPatente(int id, string nombre, string idPantalla, string idControl, int verificador_horizontal) {
            string insertCommandText = "" +
                    "INSERT INTO [dbo].[Patente] " +
                        "([patente_id]" +
                        ",[patente_nombre]" +
                        ",[patente_idPantalla]" +
                        ",[patente_idControl]" +
                        ",[patente_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@nombre," +
                        "@pantalla," +
                        "@control," +
                        "@verificador_horizontal)";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);

            insertCommand.Parameters.AddWithValue("@id", id);
            insertCommand.Parameters.AddWithValue("@nombre", nombre);
            insertCommand.Parameters.AddWithValue("@pantalla", idPantalla);
            insertCommand.Parameters.AddWithValue("@control", idControl);
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
                usuario.id = (int)reader.GetValue(0);
                usuario.nombre = (string)reader.GetValue(1);
                usuario.apellido = (string)reader.GetValue(2);
                usuario.dni = (string)reader.GetValue(3);
                usuario.domicilio = (string)reader.GetValue(4);
                usuario.email = (string)reader.GetValue(5);
                usuario.password = (string)reader.GetValue(6);
                usuario.usuarioOculto = (bool)reader.GetValue(7) == false;
                usuario.fallosAutenticacionConsecutivos = (int)reader.GetValue(8);
                usuario.bloqueado = (bool)reader.GetValue(9);
            }

            CloseReader(reader);
            return usuario;
        }

        public List<(int, string, string, string)> ListaUsuarios()
        {

            List<(int, string, string, string)> result = new List<(int, string, string, string)>();

            string selectCommandText = "" +
                "SELECT persona_id, persona_nombre, persona_apellido, persona_dni " +
                "FROM [dbo].[Persona] ";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                result.Add(
                    (
                        (int)reader.GetValue(0), //id
                        (string)reader.GetValue(1), //nombre
                        (string)reader.GetValue(2), //apellido
                        (string)reader.GetValue(3) //dni
                    )
                );
            }
            CloseReader(reader);

            return result;

        }

        public List<(int, string, string, string)> ListaIntegrantes(int grupoId) {

            List<(int, string, string, string)> result = new List<(int, string, string, string)>();

            string selectCommandText = "" +
                "SELECT persona_id, persona_nombre, persona_apellido, persona_dni " +
                "FROM [dbo].[Persona] " +
                "INNER JOIN Persona_Grupo ON persona_grupo_persona_id = persona_id " +
                "WHERE persona_grupo_grupo_id = @grupoId";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@grupoId", grupoId);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                result.Add(
                    (
                        (int)reader.GetValue(0), //id
                        (string)reader.GetValue(1), //nombre
                        (string)reader.GetValue(2), //apellido
                        (string)reader.GetValue(3) //dni
                    )
                );
            }
            CloseReader(reader);

            return result;

        }

        public bool BuscarUsuario(string dni, string email) {
            string query = "" +
                "SELECT * " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_dni = '" + dni + "' OR persona_email = '" + email + "'";

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
                    "persona_id = '" + idUsuario + "' AND persona_bloqueada = 1";

            return ExecuteScalar(query) != -1;
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
        ///////////////////////////////////////    METODOS GRUPOS     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public List<int> ObtenerGrupos(int usuarioId) {
            List<int> result = new List<int>();

            string selectCommandText = "" +
                "SELECT Persona_Grupo.persona_grupo_grupo_id " +
                "FROM Persona_Grupo " +
                "INNER JOIN Persona " +
                    "ON Persona.persona_id = Persona_Grupo.persona_grupo_persona_id " +
                "WHERE Persona.persona_id = @persona_id";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@persona_id", usuarioId);

            SqlDataReader reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                result.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            return result;
        }

        public List<int> ListaGruposPorUsuario(int usuarioId) {
            return ObtenerGrupos(usuarioId);
        }

        public List<int> ListaUsuariosPorGrupo(int grupoId) {
            List<int> result = new List<int>();

            string selectCommandText = "" +
                "SELECT persona_grupo_persona_id " +
                "FROM Persona_Grupo " +
                "WHERE persona_grupo_grupo_id = @grupo_id";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@grupo_id", grupoId);

            SqlDataReader reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                result.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            return result;
        }

        public List<(int, string)> ListaGrupos()
        {

            List<(int, string)> result = new List<(int, string)>();

            string selectCommandText = "" +
                "SELECT grupo_id, grupo_nombre " +
                "FROM [dbo].[Grupo] ";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                result.Add(
                    (
                        (int)reader.GetValue(0), //id
                        (string)reader.GetValue(1) //nombre
                    )
                );
            }
            CloseReader(reader);

            return result;

        }

        public int CrearGrupo(string nombre_nuevo_grupo) {

            try
            {
                string insertCommandText = "" +
                    "INSERT INTO [dbo].[Grupo] " +
                        "([grupo_id]" +
                        ",[grupo_nombre]" +
                        ",[grupo_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@nombre," +
                        "@verificador_horizontal)";

                SqlCommand insertCommand = new SqlCommand(insertCommandText);

                int id = ObtenerUltimoId("Grupo") + 1;
                insertCommand.Parameters.AddWithValue("@id", id);
                insertCommand.Parameters.AddWithValue("@nombre", nombre_nuevo_grupo);
                insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);
                ExecuteNonQuery(insertCommand);

                return id;
            }
            catch (Exception e) {
                return -1;
            }
            
        }

        public void AgregarUsuarioGrupo(int usuarioId, int grupoId) {

            string insertCommandText = "" +
                "INSERT " +
                "INTO Persona_Grupo " +
                "VALUES (@grupo_id, @persona_id, @verificadorHorizontal) ";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);
            insertCommand.Parameters.AddWithValue("@grupo_id", grupoId);
            insertCommand.Parameters.AddWithValue("@persona_id", usuarioId);
            insertCommand.Parameters.AddWithValue("@verificadorHorizontal", 0);
            ExecuteNonQuery(insertCommand);

        }

        public void QuitarUsuarioGrupo(int usuarioId, int grupoId)
        {

            string deleteCommandText = "" +
                "DELETE " +
                "FROM Persona_Grupo " +
                "WHERE persona_grupo_persona_id = @persona_id AND persona_grupo_grupo_id = @grupo_id ";

            SqlCommand deleteCommand = new SqlCommand(deleteCommandText);
            deleteCommand.Parameters.AddWithValue("@persona_id", usuarioId);
            deleteCommand.Parameters.AddWithValue("@grupo_id", grupoId);            
            ExecuteNonQuery(deleteCommand);

        }

        public void EliminarGrupo(int grupoId) {
            string deleteCommandText1 = "" +
                "DELETE " +
                "FROM Grupo_Patente " +
                "WHERE grupo_patente_grupo_id = @grupoId";

            string deleteCommandText2 = "" +
                "DELETE " +
                "FROM Persona_Grupo " +
                "WHERE persona_grupo_grupo_id = @grupoId";

            string deleteCommandText3 = "" +
                "DELETE " +
                "FROM Grupo " +
                "WHERE grupo_id = @grupoId";

            SqlCommand deleteCommand1 = new SqlCommand(deleteCommandText1);
            SqlCommand deleteCommand2 = new SqlCommand(deleteCommandText2);
            SqlCommand deleteCommand3 = new SqlCommand(deleteCommandText3);
            deleteCommand1.Parameters.AddWithValue("@grupoId", grupoId);
            deleteCommand2.Parameters.AddWithValue("@grupoId", grupoId);
            deleteCommand3.Parameters.AddWithValue("@grupoId", grupoId);
            ExecuteNonQuery(deleteCommand1);
            ExecuteNonQuery(deleteCommand2);
            ExecuteNonQuery(deleteCommand3);
        }

        public void QuitarPatenteDelGrupo(int grupoId, int patenteId)
        {

            string deleteCommandText = "" +
                "DELETE " +
                "FROM Grupo_Patente " +
                "WHERE grupo_patente_patente_id = @patenteId and grupo_patente_grupo_id = @grupoId";

            SqlCommand deleteCommand = new SqlCommand(deleteCommandText);
            deleteCommand.Parameters.AddWithValue("@patenteId", patenteId);
            deleteCommand.Parameters.AddWithValue("@grupoId", grupoId);
            ExecuteNonQuery(deleteCommand);

        }

        public void AgregarPatenteAGrupo(int grupoId, int patenteId)
        {

            string insertCommandText = "" +
                "INSERT " +
                "INTO Grupo_Patente " +
                "VALUES (@grupo_id, @patente_id, @verificadorHorizontal) ";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);
            insertCommand.Parameters.AddWithValue("@grupo_id", grupoId);
            insertCommand.Parameters.AddWithValue("@patente_id", patenteId);
            insertCommand.Parameters.AddWithValue("@verificadorHorizontal", 0);
            ExecuteNonQuery(insertCommand);

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    METODOS PATENTES     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public List<(int, string)> ListaPatentes() {

            List<(int, string)> result = new List<(int, string)>();

            string selectCommandText = "" +
                "SELECT patente_id, patente_nombre " +
                "FROM [dbo].[Patente] ";

            SqlCommand selectCommand = new SqlCommand(selectCommandText);
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                result.Add(
                    (
                        (int)reader.GetValue(0), //id
                        (string)reader.GetValue(1) //nombre
                    )
                );
            }
            CloseReader(reader);

            return result;

        }

        public List<(int, string)> ListaPatentes(int grupoId)
        {
            return ListaPatentesUsuarioGrupo(-1, grupoId);
        }

        public List<string> FiltrarPatentes(int usuarioId, string pantalla, List<int> gruposUsuario) {
            List<string> result = new List<string>();

            string selectCommandTextGrupo = "" +
                "SELECT patente_idControl " +
                "FROM Patente " +
                "INNER JOIN Grupo_Patente " +
                    "ON Patente.patente_id = Grupo_Patente.grupo_patente_patente_id " +
                "WHERE grupo_patente_grupo_id = @grupo_id " +
                    "AND Patente.patente_idPantalla = @pantalla_id";
            SqlCommand selectCommand;
            SqlDataReader reader;
            string value = "";

            foreach (int grupo in gruposUsuario) {
                selectCommand = new SqlCommand(selectCommandTextGrupo);
                selectCommand.Parameters.AddWithValue("@grupo_id", grupo);
                selectCommand.Parameters.AddWithValue("@pantalla_id", pantalla);
                reader = ExecuteReader(selectCommand);
                while (reader.Read())
                {
                    value = reader.GetValue(0).ToString();
                    if (!result.Contains(value)) {
                        result.Add(reader.GetValue(0).ToString());
                    }
                }
                CloseReader(reader);
            }

            string selectCommandTextPersona = "" +
                "SELECT patente_idControl " +
                "FROM Patente " +
                "INNER JOIN Persona_Patente " +
                    "ON Patente.patente_id = Persona_Patente.persona_patente_patente_id " +
                "WHERE Persona_Patente.persona_patente_persona_id = @persona_id " +
                    "AND Patente.patente_idPantalla = @pantalla_id";

            selectCommand = new SqlCommand(selectCommandTextPersona);
            selectCommand.Parameters.AddWithValue("@persona_id", usuarioId);
            selectCommand.Parameters.AddWithValue("@pantalla_id", pantalla);
            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                value = reader.GetValue(0).ToString();
                if (!result.Contains(value))
                {
                    result.Add(reader.GetValue(0).ToString());
                }
            }
            CloseReader(reader);

            return result;
        }

        public List<(int, string)> ListaPatentesUsuarioGrupo(int usuarioId, int grupo) {
            List<string> patentes = new List<string>();
            List<(int, string)> result = new List<(int, string)>();

            string selectCommandTextGrupo = "" +
                "SELECT patente_id, patente_nombre " +
                "FROM Patente " +
                "INNER JOIN Grupo_Patente " +
                    "ON Patente.patente_id = Grupo_Patente.grupo_patente_patente_id " +
                "WHERE grupo_patente_grupo_id = @grupo_id ";

            SqlCommand selectCommand;
            SqlDataReader reader;
            string value = "";

            //En caso de que no se especifique grupo, paso derecho buscar las patentes asociadas a nivel de la persona
            if (grupo != -1)
            {
                selectCommand = new SqlCommand(selectCommandTextGrupo);
                selectCommand.Parameters.AddWithValue("@grupo_id", grupo);
                reader = ExecuteReader(selectCommand);
                while (reader.Read())
                {
                    value = reader.GetValue(0).ToString();
                    if (!patentes.Contains(value))
                    {
                        result.Add(((int)reader.GetValue(0), reader.GetValue(1).ToString()));
                    }
                }
                CloseReader(reader);
            }

            string selectCommandTextPersona = "" +
                "SELECT patente_id, patente_nombre " +
                "FROM Patente " +
                "INNER JOIN Persona_Patente " +
                    "ON Patente.patente_id = Persona_Patente.persona_patente_patente_id " +
                "WHERE Persona_Patente.persona_patente_persona_id = @persona_id ";

            if (usuarioId != -1)
            {
                selectCommand = new SqlCommand(selectCommandTextPersona);
                selectCommand.Parameters.AddWithValue("@persona_id", usuarioId);
                reader = ExecuteReader(selectCommand);
                while (reader.Read())
                {
                    value = reader.GetValue(0).ToString();
                    if (!patentes.Contains(value))
                    {
                        result.Add(((int)reader.GetValue(0), reader.GetValue(1).ToString()));
                    }
                }
                CloseReader(reader);
            }

            return result;
        }

        public List<int> UsuariosConPatente(int grupoId, int patenteId)
        {
            
            //Esta funcion busca todas las patentes otorgadas al usuario a excepcion de aquellas que le fueron otorgadas a nivel del grupo especificado
            
            List<int> usuarios = new List<int>();
            SqlCommand selectCommand;
            SqlDataReader reader;

            string selectCommandTextPersona = "" +
                "SELECT persona_patente_persona_id " +
                "FROM Persona_Patente " +
                "WHERE persona_patente_patente_id = @patente_id ";

            selectCommand = new SqlCommand(selectCommandTextPersona);
            selectCommand.Parameters.AddWithValue("@patente_id", patenteId);
            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                usuarios.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            string selectCommandTextGrupo = "" +
                "SELECT persona_grupo_persona_id " +
                "FROM Persona_Grupo " +
                "INNER JOIN Grupo_Patente ON persona_grupo_grupo_id = grupo_patente_grupo_id " +
                "WHERE grupo_patente_patente_id = @patente_id " +
                "AND grupo_patente_grupo_id not in (" + grupoId +")";

            selectCommand = new SqlCommand(selectCommandTextGrupo);
            selectCommand.Parameters.AddWithValue("@patente_id", patenteId);
            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                usuarios.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            return usuarios;

        }

        public List<int> UsuariosConPatente(int patenteId) {
            List<int> usuarios = new List<int>();
            SqlCommand selectCommand;
            SqlDataReader reader;

            string selectCommandTextPersona = "" +
                "SELECT persona_patente_persona_id " +
                "FROM Persona_Patente " +
                "WHERE persona_patente_patente_id = @patente_id ";

            selectCommand = new SqlCommand(selectCommandTextPersona);
            selectCommand.Parameters.AddWithValue("@patente_id", patenteId);
            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                usuarios.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            string selectCommandTextGrupo = "" +
                "SELECT persona_grupo_persona_id " +
                "FROM Persona_Grupo " +
                "INNER JOIN Grupo_Patente ON persona_grupo_grupo_id = grupo_patente_grupo_id " +
                "WHERE grupo_patente_patente_id = @patente_id ";

            selectCommand = new SqlCommand(selectCommandTextGrupo);
            selectCommand.Parameters.AddWithValue("@patente_id", patenteId);
            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                usuarios.Add((int)reader.GetValue(0));
            }
            CloseReader(reader);

            return usuarios;

        }

        public void QuitarPatente(int usuarioId, int patenteId) {

            string deleteCommandText = "" +
                "DELETE " +
                "FROM Persona_Patente " +
                "WHERE persona_patente_patente_id = @patenteId and persona_patente_persona_id = @usuarioId";                    

            SqlCommand deleteCommand = new SqlCommand(deleteCommandText);
            deleteCommand.Parameters.AddWithValue("@patenteId", patenteId);
            deleteCommand.Parameters.AddWithValue("@usuarioId", usuarioId);
            ExecuteNonQuery(deleteCommand);

        }

        public void AgregarPatente(int usuarioId, int patenteId)
        {

            string insertCommandText = "" +
                "INSERT " +
                "INTO Persona_Patente " +
                "VALUES (@persona_id, @patente_id, @verificadorHorizontal) ";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);
            insertCommand.Parameters.AddWithValue("@persona_id", usuarioId);
            insertCommand.Parameters.AddWithValue("@patente_id", patenteId);
            insertCommand.Parameters.AddWithValue("@verificadorHorizontal", 0);
            ExecuteNonQuery(insertCommand);

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
            if (qryResult == null || qryResult == System.DBNull.Value)
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
