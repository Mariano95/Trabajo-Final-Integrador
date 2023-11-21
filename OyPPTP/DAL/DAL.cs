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

        public List<(string, int)> SumaVerificadoresHorizontalesPorTabla(List<string> excepciones) {

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

                if (!excepciones.Contains(tabla)) {

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
                        for (int i = 0; i < reader2.FieldCount - 1; i++)
                        {
                            concatenado += reader2.GetValue(i).ToString();
                            if (tabla == "Bitacora_Detalle")
                            {
                                int a = 1;
                            }
                        }

                        contador = 1;
                        foreach (char c in concatenado)
                        {
                            sumaVerificadoresHorizontales += (int)c * contador;
                            contador++;
                        }

                        if (tabla == "Bitacora_Detalle")
                        {
                            int a = 1;
                        }
                    }
                    CloseReader(reader);

                    //Ignoro a las tablas que no tienen ni un registro
                    if (sumaVerificadoresHorizontales > 0)
                    {
                        resultado.Add((tabla, sumaVerificadoresHorizontales));
                    }

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

            id = 53;
            nombre = "Pantalla inicial backup";
            idPantalla = "PantallaInicial";
            idControl = "backup";
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

        public void InicializarIdiomas() {
            int id = 1;
            string idioma_nombre = "English";
            string concatenado = id.ToString() + idioma_nombre;
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertIdioma(id, idioma_nombre, verificador_horizontal);

            id = 2;
            idioma_nombre = "Português";
            concatenado = id.ToString() + idioma_nombre;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertIdioma(id, idioma_nombre, verificador_horizontal);

            id = 3;
            idioma_nombre = "Español";
            concatenado = id.ToString() + idioma_nombre;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertIdioma(id, idioma_nombre, verificador_horizontal);


            int sumaVerificadoresHorizontales = ObtenerSumaVerificadoresHorizontales("Idioma");
            bool verificador_vertical_ok = ActualizarVerificadorVertical("Idioma", sumaVerificadoresHorizontales);

        }

        private void InsertIdioma(int id, string nombre, int verificador_horizontal)
        {
            string insertCommandText = "" +
                    "INSERT INTO [dbo].[Idioma] " +
                        "([idioma_id]" +
                        ",[idioma_nombre]" +
                        ",[idioma_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@nombre," +
                        "@verificador_horizontal)";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);

            insertCommand.Parameters.AddWithValue("@id", id);
            insertCommand.Parameters.AddWithValue("@nombre", nombre);
            insertCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            ExecuteNonQuery(insertCommand);
        }

        public void InicializarTextosIdiomas()
        {
            int id = 1;
            int id_idioma = 1;
            string nombre_control = "cancelar";
            string texto_control = "Cancel";
            string concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 2;
            id_idioma = 1;
            nombre_control = "iniciar";
            texto_control = "Start";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 3;
            id_idioma = 1;
            nombre_control = "nueva_contrasena";
            texto_control = "New password";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 4;
            id_idioma = 1;
            nombre_control = "actualizar";
            texto_control = "Update";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 5;
            id_idioma = 1;
            nombre_control = "desbloquear_usuario_label";
            texto_control = "Unlock user";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 6;
            id_idioma = 1;
            nombre_control = "desbloquear_usuario";
            texto_control = "Unlock user";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 7;
            id_idioma = 1;
            nombre_control = "grupo";
            texto_control = "Group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 8;
            id_idioma = 1;
            nombre_control = "eliminar_grupo";
            texto_control = "Delete group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 9;
            id_idioma = 1;
            nombre_control = "crear_nuevo_grupo";
            texto_control = "Create group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 10;
            id_idioma = 1;
            nombre_control = "crear_nuevo_grupo_text";
            texto_control = "Create group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 11;
            id_idioma = 1;
            nombre_control = "crear_grupo";
            texto_control = "Create group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 12;
            id_idioma = 1;
            nombre_control = "miembros_del_grupo";
            texto_control = "Group members";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 13;
            id_idioma = 1;
            nombre_control = "quitar_del_grupo";
            texto_control = "Remove from group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 14;
            id_idioma = 1;
            nombre_control = "otros_usuarios";
            texto_control = "Other members";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 15;
            id_idioma = 1;
            nombre_control = "agregar_al_grupo";
            texto_control = "Add to group";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 16;
            id_idioma = 1;
            nombre_control = "volver";
            texto_control = "Return";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 17;
            id_idioma = 1;
            nombre_control = "datosPersonalesOption ";
            texto_control = "Personal details";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 18;
            id_idioma = 1;
            nombre_control = "modificarServicios";
            texto_control = "Edit services";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 19;
            id_idioma = 1;
            nombre_control = "modificarDatosPersonales";
            texto_control = "Edit personal details";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 20;
            id_idioma = 1;
            nombre_control = "modificarPassword";
            texto_control = "Change password";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 21;
            id_idioma = 1;
            nombre_control = "ocultarUsuario";
            texto_control = "Hide user";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 22;
            id_idioma = 1;
            nombre_control = "busquedaTrabajadoresOption ";
            texto_control = "Search workers";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 23;
            id_idioma = 1;
            nombre_control = "buscarTrabajadores";
            texto_control = "Search workers";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 24;
            id_idioma = 1;
            nombre_control = "citacionesOption";
            texto_control = "Citations";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 25;
            id_idioma = 1;
            nombre_control = "citacionesRecibidas";
            texto_control = "Received citations";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 26;
            id_idioma = 1;
            nombre_control = "citacionesEnviadas";
            texto_control = "Sent citations";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 27;
            id_idioma = 1;
            nombre_control = "administracionOption ";
            texto_control = "Administration";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 28;
            id_idioma = 1;
            nombre_control = "bitacora";
            texto_control = "Log";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 29;
            id_idioma = 1;
            nombre_control = "backup";
            texto_control = "Create system backup";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 30;
            id_idioma = 1;
            nombre_control = "restaurarSistema";
            texto_control = "Restore backup";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 31;
            id_idioma = 1;
            nombre_control = "desbloquearUsuario";
            texto_control = "Unlock user";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 32;
            id_idioma = 1;
            nombre_control = "patentesPorGrupo";
            texto_control = "Group patents";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 33;
            id_idioma = 1;
            nombre_control = "patentesPorUsuario";
            texto_control = "User patents";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 34;
            id_idioma = 1;
            nombre_control = "gruposUsuarios";
            texto_control = "User groups";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 35;
            id_idioma = 1;
            nombre_control = "crearUsuarioAdmin";
            texto_control = "Create admin user";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 36;
            id_idioma = 1;
            nombre_control = "opciones";
            texto_control = "Options";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 37;
            id_idioma = 1;
            nombre_control = "cambiarIdioma";
            texto_control = "Change language";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 38;
            id_idioma = 1;
            nombre_control = "sesion";
            texto_control = "Session";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 39;
            id_idioma = 1;
            nombre_control = "cerrarSesion";
            texto_control = "End session";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 40;
            id_idioma = 1;
            nombre_control = "pantalla_inicial_label_1";
            texto_control = "Welcome, ";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 41;
            id_idioma = 1;
            nombre_control = "pantalla_inicial_label_2";
            texto_control = "Welcome to the service portal. Please select one of the options above to continue.";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 42;
            id_idioma = 3;
            nombre_control = "cancelar";
            texto_control = "Cancelar";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 43;
            id_idioma = 3;
            nombre_control = "iniciar";
            texto_control = "Iniciar";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 44;
            id_idioma = 3;
            nombre_control = "nueva_contrasena";
            texto_control = "Nueva contraseña";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 45;
            id_idioma = 3;
            nombre_control = "actualizar";
            texto_control = "Actualizar";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 46;
            id_idioma = 3;
            nombre_control = "desbloquear_usuario_label";
            texto_control = "Desbloquear usuario";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 47;
            id_idioma = 3;
            nombre_control = "desbloquear_usuario";
            texto_control = "Desbloquear usuario";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 48;
            id_idioma = 3;
            nombre_control = "grupo";
            texto_control = "Grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 49;
            id_idioma = 3;
            nombre_control = "eliminar_grupo";
            texto_control = "Eliminar grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 50;
            id_idioma = 3;
            nombre_control = "crear_nuevo_grupo";
            texto_control = "Crear grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 51;
            id_idioma = 3;
            nombre_control = "crear_nuevo_grupo_text";
            texto_control = "Crear grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 52;
            id_idioma = 3;
            nombre_control = "crear_grupo";
            texto_control = "Crear grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 53;
            id_idioma = 3;
            nombre_control = "miembros_del_grupo";
            texto_control = "Miembros del grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 54;
            id_idioma = 3;
            nombre_control = "quitar_del_grupo";
            texto_control = "Quitar del grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 55;
            id_idioma = 3;
            nombre_control = "otros_usuarios";
            texto_control = "Otros usuarios";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 56;
            id_idioma = 3;
            nombre_control = "agregar_al_grupo";
            texto_control = "Agregar al grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 57;
            id_idioma = 3;
            nombre_control = "volver";
            texto_control = "Volver";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 58;
            id_idioma = 3;
            nombre_control = "datosPersonalesOption";
            texto_control = "Datos personales";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 59;
            id_idioma = 3;
            nombre_control = "modificarServicios";
            texto_control = "Modificar servicios";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 60;
            id_idioma = 3;
            nombre_control = "modificarDatosPersonales";
            texto_control = "Modificar datos personales";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 61;
            id_idioma = 3;
            nombre_control = "modificarPassword ";
            texto_control = "Modificar contraseña";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 62;
            id_idioma = 3;
            nombre_control = "ocultarUsuario";
            texto_control = "Ocultar usuario";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 63;
            id_idioma = 3;
            nombre_control = "busquedaTrabajadoresOption ";
            texto_control = "Búsqueda de trabajadores";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 64;
            id_idioma = 3;
            nombre_control = "buscarTrabajadores";
            texto_control = "Buscar trabajadores";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 65;
            id_idioma = 3;
            nombre_control = "citacionesOption ";
            texto_control = "Citaciones";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 66;
            id_idioma = 3;
            nombre_control = "citacionesRecibidas";
            texto_control = "Citaciones recibidas";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 67;
            id_idioma = 3;
            nombre_control = "citacionesEnviadas";
            texto_control = "Citaciones enviadas";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 68;
            id_idioma = 3;
            nombre_control = "administracionOption ";
            texto_control = "Administación";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);


            id = 69;
            id_idioma = 3;
            nombre_control = "bitacora";
            texto_control = "Bitácora";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 70;
            id_idioma = 3;
            nombre_control = "backup";
            texto_control = "Backup";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 71;
            id_idioma = 3;
            nombre_control = "restaurarSistema";
            texto_control = "Restaurar sistema";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 72;
            id_idioma = 3;
            nombre_control = "desbloquearUsuario";
            texto_control = "Desbloquear usuario";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 73;
            id_idioma = 3;
            nombre_control = "patentesPorGrupo";
            texto_control = "Patentes por grupo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 74;
            id_idioma = 3;
            nombre_control = "patentesPorUsuario";
            texto_control = "Patentes por usuario";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 75;
            id_idioma = 3;
            nombre_control = "gruposUsuarios";
            texto_control = "Grupos de usuarios";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 76;
            id_idioma = 3;
            nombre_control = "crearUsuarioAdmin ";
            texto_control = "Crear usuario administrador";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 77;
            id_idioma = 3;
            nombre_control = "opciones";
            texto_control = "Opciones";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 78;
            id_idioma = 3;
            nombre_control = "cambiarIdioma";
            texto_control = "Cambiar idioma";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 79;
            id_idioma = 3;
            nombre_control = "sesion";
            texto_control = "Sesión";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 80;
            id_idioma = 3;
            nombre_control = "cerrarSesion";
            texto_control = "Cerrar sesión";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 81;
            id_idioma = 3;
            nombre_control = "pantalla_inicial_label_1";
            texto_control = "Bienvenido, ";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 82;
            id_idioma = 3;
            nombre_control = "pantalla_inicial_label_2";
            texto_control = "Bienvenido al portal de servicios. Por favor, seleccioná una de las opciones de arriba para continuar.";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 83;
            id_idioma = 3;
            nombre_control = "leyenda_error_sin_ayuda";
            texto_control = "No se encontró un documento de ayuda para esta pantalla";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 84;
            id_idioma = 1;
            nombre_control = "leyenda_error_sin_ayuda";
            texto_control = "A help documnt could not be found for this screen";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 85;
            id_idioma = 3;
            nombre_control = "leyenda_error_sin_navegador";
            texto_control = "No se encontró un navegador web configurado para este equipo";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 86;
            id_idioma = 1;
            nombre_control = "leyenda_error_sin_navegador";
            texto_control = "A web browser could not be found on this computer";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 87;
            id_idioma = 3;
            nombre_control = "leyenda_error_carga_ayuda";
            texto_control = "Hubo un error al abrir el documento de ayuda de esta pantalla";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            id = 88;
            id_idioma = 1;
            nombre_control = "leyenda_error_carga_ayuda";
            texto_control = "An error occured while trying to open the help document for this screen";
            concatenado = id.ToString() + id_idioma + nombre_control + texto_control;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertTextoIdioma(id, id_idioma, nombre_control, texto_control, verificador_horizontal);

            int sumaVerificadoresHorizontales = ObtenerSumaVerificadoresHorizontales("Idioma_Texto");
            bool verificador_vertical_ok = ActualizarVerificadorVertical("Idioma_Texto", sumaVerificadoresHorizontales);

        }

        private void InsertTextoIdioma(int id, int id_idioma, string nombre_control, string texto_control, int verificador_horizontal)
        {
            string insertCommandText = "" +
                    "INSERT INTO [dbo].[Idioma_Texto] " +
                        "([idioma_texto_id]" +
                        ",[idioma_texto_id_idioma]" +
                        ",[idioma_texto_nombre_control]" +
                        ",[idioma_texto_control]" +
                        ",[idioma_texto_verificador_horizontal]) " +
                    "VALUES " +
                        "(@id," +
                        "@id_idioma," +
                        "@nombre_control," +
                        "@texto_control," +
                        "@verificador_horizontal)";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);

            insertCommand.Parameters.AddWithValue("@id", id);
            insertCommand.Parameters.AddWithValue("@id_idioma", id_idioma);
            insertCommand.Parameters.AddWithValue("@nombre_control", nombre_control);
            insertCommand.Parameters.AddWithValue("@texto_control", texto_control);
            insertCommand.Parameters.AddWithValue("@verificador_horizontal", verificador_horizontal);
            ExecuteNonQuery(insertCommand);
        }

        public void InicializarPantallas()
        {
            int id = 1;
            int idioma_id = 1;
            string pantallaCodigo = "PantallaInicial";
            string link_ayuda = "https://docs.google.com/document/d/12fxnXBTnWyOYWkHUJR0YXaAIhkhpeOBpBLbUk9RvNNU/edit?usp=sharing";            
            string concatenado = id.ToString() + idioma_id.ToString() + pantallaCodigo + link_ayuda;
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPantalla(id, idioma_id, pantallaCodigo, link_ayuda, verificador_horizontal);

            id = 2;
            idioma_id = 3;
            pantallaCodigo = "PantallaInicial";
            link_ayuda = "https://docs.google.com/document/d/1Ex1vlbH9gBKQ9QbcVdizh_7xUKtbCf5O_PlnF2qhuyo/edit?usp=sharing";
            concatenado = id.ToString() + idioma_id.ToString() + pantallaCodigo + link_ayuda;
            verificador_horizontal = 0;
            contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }
            InsertPantalla(id, idioma_id, pantallaCodigo, link_ayuda, verificador_horizontal);



            int sumaVerificadoresHorizontales = ObtenerSumaVerificadoresHorizontales("Pantalla");
            bool verificador_vertical_ok = ActualizarVerificadorVertical("Pantalla", sumaVerificadoresHorizontales);

        }

        private void InsertPantalla(int id, int idioma_id, string pantalla_codigo, string link_ayuda, int verificador_horizontal)
        {
            string insertCommandText = "" +
                    "INSERT INTO [dbo].[Pantalla] " +
                        "([pantalla_id]" +
                        ",[pantalla_idioma_id]" +
                        ",[pantalla_codigo]" +
                        ",[pantalla_link_ayuda]" +
                        ",[pantalla_verificador_horizontal]) " +
                    "VALUES " +
                        "(@idPantalla," +
                        "@idIdioma," +
                        "@pantallaCodigo," +
                        "@linkAyuda," +
                        "@verificador_horizontal)";

            SqlCommand insertCommand = new SqlCommand(insertCommandText);

            insertCommand.Parameters.AddWithValue("@idPantalla", id);
            insertCommand.Parameters.AddWithValue("@idIdioma", idioma_id);
            insertCommand.Parameters.AddWithValue("@pantallaCodigo", pantalla_codigo);
            insertCommand.Parameters.AddWithValue("@linkAyuda", link_ayuda);
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

        public int BuscarUsuario(string dni)
        {
            string query = "" +
                "SELECT * " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_dni = '" + dni + "'";

            return ExecuteScalar(query);

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

        public bool DesbloquearUsuario(int idUsuario) {
            string updateCommandText = "" +
                "UPDATE Persona " +
                "SET " +
                    "persona_fallos_autenticacion_consecutivos = 0, " +
                    "persona_bloqueada = 0 " +
                "WHERE " +
                    "persona_id = @id";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@id", idUsuario);
            ExecuteNonQuery(updateCommand);
            return true;
        }

        public bool ActualizarUsuario(int usuarioId, string nuevoPassword) {
            string updateCommandText = "" +
                "UPDATE Persona " +
                "SET " +
                    "persona_password = @password " +
                "WHERE " +
                    "persona_id = @id";

            SqlCommand updateCommand = new SqlCommand(updateCommandText);
            updateCommand.Parameters.AddWithValue("@password", nuevoPassword);
            updateCommand.Parameters.AddWithValue("@id", usuarioId);
            ExecuteNonQuery(updateCommand);
            return true;
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

        public int InsertarBitacoraDetalle(int id_evento, int id_registro, string tabla_afectada, int id_grupo_afectado, int id_usuario_afectado, int id_patente_afectada)
        {
            try
            {

                string insertCommandText;
                SqlCommand insertCommand;
                int id;


                switch (id_evento)
                {
                    //Fallo de integridad en tabla
                    case 8:
                        insertCommandText = "" +
                            "INSERT INTO [dbo].[Bitacora_Detalle] " +
                                "([bitacora_detalle_id]" +
                                ",[bitacora_detalle_registro_bitacora_id]" +
                                ",[bitacora_detalle_tabla_involucrada]" +
                                ",[bitacora_detalle_verificador_horizontal]) " +
                            "VALUES " +
                                "(@id," +
                                "@registro_id," +
                                "@tabla_id," +
                                "@verificador_horizontal)";

                        insertCommand = new SqlCommand(insertCommandText);

                        id = ObtenerUltimoId("Bitacora_Detalle") + 1;
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@registro_id", id_registro);
                        insertCommand.Parameters.AddWithValue("@tabla_id", tabla_afectada);
                        insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);

                        break;


                    //Alta o baja de un grupo
                    case 13: case 14:
                        insertCommandText = "" +
                            "INSERT INTO [dbo].[Bitacora_Detalle] " +
                                "([bitacora_detalle_id]" +
                                ",[bitacora_detalle_registro_bitacora_id]" +
                                ",[bitacora_detalle_grupo_involucrado_id]" +
                                ",[bitacora_detalle_verificador_horizontal]) " +
                            "VALUES " +
                                "(@id," +
                                "@registro_id," +
                                "@grupo_id," +
                                "@verificador_horizontal)";

                        insertCommand = new SqlCommand(insertCommandText);

                        id = ObtenerUltimoId("Bitacora_Detalle") + 1;
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@registro_id", id_registro);
                        insertCommand.Parameters.AddWithValue("@grupo_id", id_grupo_afectado);
                        insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);

                        break;


                    //Usuario agregado o eliminado de un grupo
                    case 15: case 16:
                        insertCommandText = "" +
                            "INSERT INTO [dbo].[Bitacora_Detalle] " +
                                "([bitacora_detalle_id]" +
                                ",[bitacora_detalle_registro_bitacora_id]" +
                                ",[bitacora_detalle_grupo_involucrado_id]" +
                                ",[bitacora_detalle_usuario_involucrado_id]" +
                                ",[bitacora_detalle_verificador_horizontal]) " +
                            "VALUES " +
                                "(@id," +
                                "@registro_id," +
                                "@grupo_id," +
                                "@usuario_id," +
                                "@verificador_horizontal)";

                        insertCommand = new SqlCommand(insertCommandText);

                        id = ObtenerUltimoId("Bitacora_Detalle") + 1;
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@registro_id", id_registro);
                        insertCommand.Parameters.AddWithValue("@grupo_id", id_grupo_afectado);
                        insertCommand.Parameters.AddWithValue("@usuario_id", id_usuario_afectado);
                        insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);

                        break;


                    //Patente otorgada o quitada de un usuario
                    case 17: case 18:
                        insertCommandText = "" +
                            "INSERT INTO [dbo].[Bitacora_Detalle] " +
                                "([bitacora_detalle_id]" +
                                ",[bitacora_detalle_registro_bitacora_id]" +
                                ",[bitacora_detalle_usuario_involucrado_id]" +
                                ",[bitacora_detalle_patente_involucrada_id]" +
                                ",[bitacora_detalle_verificador_horizontal]) " +
                            "VALUES " +
                                "(@id," +
                                "@registro_id," +
                                "@usuario_id," +
                                "@patente_id," +
                                "@verificador_horizontal)";

                        insertCommand = new SqlCommand(insertCommandText);

                        id = ObtenerUltimoId("Bitacora_Detalle") + 1;
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@registro_id", id_registro);
                        insertCommand.Parameters.AddWithValue("@usuario_id", id_usuario_afectado);
                        insertCommand.Parameters.AddWithValue("@patente_id", id_patente_afectada);
                        insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);

                        break;


                    //Patente otorgada o quitada de un grupo
                    case 19: case 20:
                        insertCommandText = "" +
                            "INSERT INTO [dbo].[Bitacora_Detalle] " +
                                "([bitacora_detalle_id]" +
                                ",[bitacora_detalle_registro_bitacora_id]" +
                                ",[bitacora_detalle_grupo_involucrado_id]" +
                                ",[bitacora_detalle_patente_involucrada_id]" +
                                ",[bitacora_detalle_verificador_horizontal]) " +
                            "VALUES " +
                                "(@id," +
                                "@registro_id," +
                                "@grupo_id," +
                                "@patente_id," +
                                "@verificador_horizontal)";

                        insertCommand = new SqlCommand(insertCommandText);

                        id = ObtenerUltimoId("Bitacora_Detalle") + 1;
                        insertCommand.Parameters.AddWithValue("@id", id);
                        insertCommand.Parameters.AddWithValue("@registro_id", id_registro);
                        insertCommand.Parameters.AddWithValue("@grupo_id", id_grupo_afectado);
                        insertCommand.Parameters.AddWithValue("@patente_id", id_patente_afectada);
                        insertCommand.Parameters.AddWithValue("@verificador_horizontal", 0);

                        break;


                    default:
                        return -1;
                }

                ExecuteNonQuery(insertCommand);
                return id;
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public List<(int, string)> ListaEventos()
        {

            List<(int, string)> result = new List<(int, string)>();

            string selectCommandText = "" +
                "SELECT evento_id, evento_nombre " +
                "FROM [dbo].[Evento] ";

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

        public List<(string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string)> ObtenerBitacora(DateTime fechaDesde, DateTime fechaHasta, int eventoId, int usuarioId)
        {
            //usuario, evento, criticidad, hora
            List<(string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string)> result = new List<(string, string, string, string, string, int, DateTime, string, string, string, string, string, string, string)>();

            string selectCommandText = "" +
                "SELECT p1.persona_nombre, " +
                    "p1.persona_apellido, " +
                    "p1.persona_dni, " +
                    "p1.persona_email, " +
                    "Evento.evento_nombre, " +
                    "Evento.evento_criticidad, " +
                    "Bitacora.bitacora_hora, " +
                    "Bitacora_Detalle.bitacora_detalle_tabla_involucrada, " +
                    "Grupo.grupo_nombre, " +
                    "p2.persona_nombre, " +
                    "p2.persona_apellido, " +
                    "p2.persona_dni, " +
                    "p2.persona_email, " +
                    "Patente.patente_nombre " +
                "FROM Bitacora " +
                "INNER JOIN Evento ON Bitacora.bitacora_evento = Evento.evento_id " +
                "INNER JOIN Persona p1 ON Bitacora.bitacora_usuario = p1.persona_id " +
                "LEFT JOIN Bitacora_Detalle ON Bitacora.bitacora_id = Bitacora_Detalle.bitacora_detalle_registro_bitacora_id " +
                "LEFT JOIN Persona p2 on Bitacora_Detalle.bitacora_detalle_usuario_involucrado_id = p2.persona_id " +
                "LEFT JOIN Patente on Patente.patente_id = Bitacora_Detalle.bitacora_detalle_patente_involucrada_id " +
                "LEFT JOIN Grupo on Grupo.grupo_id = Bitacora_Detalle.bitacora_detalle_grupo_involucrado_id " +
                "WHERE p1.persona_id >= 1 ";

            if (fechaDesde != null) {
                selectCommandText += "AND Bitacora.bitacora_hora >= @fechaDesde ";
            }

            if (fechaHasta != null)
            {
                selectCommandText += "AND Bitacora.bitacora_hora <= @fechaHasta ";
            }

            if (eventoId != -1)
            {
                selectCommandText += "AND Bitacora.bitacora_evento = @eventoId ";
            }

            if (usuarioId != -1)
            {
                selectCommandText += "AND Bitacora.bitacora_usuario = @usuarioId ";
            }

            SqlCommand selectCommand = new SqlCommand(selectCommandText);

            if (fechaDesde != null)
            {
                selectCommand.Parameters.AddWithValue("@fechaDesde", fechaDesde);
            }

            if (fechaHasta != null)
            {
                selectCommand.Parameters.AddWithValue("@fechaHasta", fechaHasta);
            }

            if (eventoId != -1)
            {
                selectCommand.Parameters.AddWithValue("@eventoId", eventoId);
            }

            if (usuarioId != -1)
            {
                selectCommand.Parameters.AddWithValue("@usuarioId", usuarioId);
            }
            
            SqlDataReader reader = ExecuteReader(selectCommand);

            while (reader.Read())
            {
                result.Add(
                    (
                        (string)reader.GetValue(0), //nombre
                        (string)reader.GetValue(1), //apellido
                        (string)reader.GetValue(2), //dni
                        (string)reader.GetValue(3), //email
                        (string)reader.GetValue(4), //evento
                        (int)reader.GetValue(5), //criticidad
                        (DateTime)reader.GetValue(6),  //hora
                        reader.GetValue(7).ToString(),  //tabla
                        reader.GetValue(8).ToString(), //grupo
                        reader.GetValue(9).ToString(),  //nombre
                        reader.GetValue(10).ToString(),  //apellido,
                        reader.GetValue(11).ToString(),  //dni
                        reader.GetValue(12).ToString(),  //email
                        reader.GetValue(13).ToString()  //patente
                    )
                );
            }
            CloseReader(reader);

            return result;
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
        ////////////////////////////////////    METODOS BACKUP    ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public bool GenerarArchivoBackup(string savePath) {

            try
            {
                string backupCommandText = "" +
                "BACKUP DATABASE [TFI_DB] " +
                "TO DISK = @savepath " +
                "WITH FORMAT";

                SqlCommand backupCommand = new SqlCommand(backupCommandText);
                backupCommand.Parameters.AddWithValue("@savepath", savePath);
                ExecuteNonQuery(backupCommand);

                return true;
            }
            catch (Exception e) {
                return false;
            }

        }

        public bool RestaurarSistema(string savePath)
        {

            try
            {
                string restoreCommandText = "" +
                "USE MASTER " +
                "ALTER DATABASE [TFI_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                "RESTORE DATABASE [TFI_DB] " +
                "FROM DISK = @savepath " +
                "WITH REPLACE, RECOVERY, STATS = 10 ";

                SqlCommand restoreCommand = new SqlCommand(restoreCommandText);
                restoreCommand.Parameters.AddWithValue("@savepath", savePath);
                ExecuteNonQuery(restoreCommand);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    METODOS IDIOMAS     /////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public List<(int, string)> ObtenerListadoIdiomas(int idioma_actual) {
            List<(int, string)> idiomas = new List<(int, string)>();
            SqlCommand selectCommand;
            SqlDataReader reader;

            string selectCommandIdioma = "" +
                "SELECT idioma_id, idioma_nombre " +
                "FROM Idioma " + 
                "WHERE idioma_id != @idioma_id";

            selectCommand = new SqlCommand(selectCommandIdioma);
            selectCommand.Parameters.AddWithValue("@idioma_id", idioma_actual);

            reader = ExecuteReader(selectCommand);
            while (reader.Read())
            {
                idiomas.Add(((int)reader.GetValue(0), (string)reader.GetValue(1)));
            }
            CloseReader(reader);

            return idiomas;
        }

        public string ObtenerTextos(string control, int idIdioma)
        {
            SqlCommand selectCommand;
            SqlDataReader reader;

            string selectCommandText = "" +
                "SELECT idioma_texto_control " +
                "FROM Idioma_Texto " +
                "WHERE idioma_texto_nombre_control = @nombre_control AND idioma_texto_id_idioma = @idioma_id";

            selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@nombre_control", control);
            selectCommand.Parameters.AddWithValue("@idioma_id", idIdioma);

            
            reader = ExecuteReader(selectCommand);
            string texto;
            
            if (reader.Read())
            {
                texto = (string)reader.GetValue(0);
            }
            else {
                texto = "";
            }
            
            CloseReader(reader);
            return texto;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////  METODOS PANTALLA     /////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public string ObtenerLinkAyuda(string codPantalla, int idiomaId) {
            SqlCommand selectCommand;
            SqlDataReader reader;

            string selectCommandText = "" +
                "SELECT pantalla_link_ayuda " +
                "FROM Pantalla " +
                "WHERE pantalla_codigo = @pantallaCodigo AND pantalla_idioma_id = @idiomaId";

            selectCommand = new SqlCommand(selectCommandText);
            selectCommand.Parameters.AddWithValue("@pantallaCodigo", codPantalla);
            selectCommand.Parameters.AddWithValue("@idiomaId", idiomaId);


            reader = ExecuteReader(selectCommand);
            string texto;

            if (reader.Read())
            {
                texto = (string)reader.GetValue(0);
            }
            else
            {
                texto = "";
            }

            CloseReader(reader);
            return texto;

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
            if (this.miConnection.State.ToString() != "Open")
            {
                this.miConnection.Open();
            }
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
            if (this.miConnection.State.ToString() == "Open") {
                this.miConnection.Close();
            }
            this.miConnection.Open();
            command.ExecuteNonQuery();
            this.miConnection.Close();
        }

        private int ExecuteScalar(string selectCommandText)
        {
            SqlCommand selectCommand = new SqlCommand(selectCommandText, this.miConnection);
            if (this.miConnection.State.ToString() == "Open")
            {
                this.miConnection.Close();
            }
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
