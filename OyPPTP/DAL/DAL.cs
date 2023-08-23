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
            this.key = aes.Key;
            this.IV = aes.IV;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    FUNCIONES PUBLICAS     //////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public byte[] EncriptarAES(string raw) {
            byte[] encrypted = Encrypt(raw, this.key, this.IV);
            return encrypted;
        }

        public string DesencriptarAES(byte[] encrypted) {
            string decrypted = Decrypt(encrypted, this.key, this.IV);
            return decrypted;
        }

        public bool ConectarBD(string stringConexion) {
            this.miConnection = new SqlConnection(stringConexion);
            return conectar();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////    FUNCIONES DE BD     ////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private bool conectar()
        {
            try {
                this.miConnection.Open();
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
                "SELECT COALESCE(MAX(persona_id),0) " +
                "FROM [dbo]." + nombreTabla;

            return this.ExecuteScalar(selectCommandText);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    FUNCIONES DE ENCRIPTADO    /////////////////////////////
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
