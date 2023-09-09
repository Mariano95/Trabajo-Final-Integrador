using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;

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
        ///////////////////////////////////////    METODOS USUARIO     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public bool BuscarUsuario(string dni, string email) { 
            string query =  "" +
                "SELECT * " +
                "FROM [dbo].[Persona] " +
                "WHERE " +
                    "persona_dni = '" + dni + "' AND persona_email = '" + email + "'";

            return ExecuteScalar(query) != -1;

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
