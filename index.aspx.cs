using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//bibliotecas/librerias a trabajar
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace encrip_rijndael
{
    public partial class index : System.Web.UI.Page
    {
        #region "metodos"
         static string EncriptacionSha(string passwordSinEncriptar)
        {
            //creamos el objeto sha1 con base a la clase SHA1CryptoServiceProvider
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            //comienzo la encriptación de mi valor de la variable passwordSinEncriptar
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(passwordSinEncriptar);
            //creamos un arreglo tipo byte, llamado hash
            Byte[] hash = sha1.ComputeHash(textOriginal);
            StringBuilder cadena = new StringBuilder();
            foreach (byte i in hash)
            {
                cadena.AppendFormat("{0:x2}", i);
            }
            return cadena.ToString();

        }
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        public String getString(byte[] text)
        {
            System.Text.ASCIIEncoding codificador = new System.Text.ASCIIEncoding();
            return codificador.GetString(text);
        }
        #endregion
        #region "variables"
        string cadenaEncriptar, passwordAVerificar, valorPasswordEncriptadoSha, passwordDB, hashNuevo;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnencriptar_Click(object sender, EventArgs e)
        {
            cadenaEncriptar = this.txtContrasena.Text;
            using (Rijndael myRijndael = Rijndael.Create())
            {
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes(cadenaEncriptar, myRijndael.Key, myRijndael.IV);

                // Decrypt the bytes to a string.
                string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                //Display the original data and the decrypted data.
                Console.WriteLine("Original:   {0}", cadenaEncriptar);  
                Console.WriteLine("Round Trip: {0}", roundtrip);

                //string[] hola = Convert.ToString( encrypted);

                this.txtEncriptada.Text = Convert.ToString(encrypted);
                //txtEncriptada.Text = getString(encrypted);
                txtDesencriptada.Text = roundtrip;
            }
        }

        protected void txtEncriptaciónSha_Click(object sender, EventArgs e)
        {
            lblValorPasswordSha.Text = "";
            //suponemos que el valor de la contraseña en la base de datos es S3perIr0nm@n
            //para esto realizo lo siguiente
            passwordDB = "S3perIr0nm@n";
            //invoco mi metodo de encriptación y el resultado lo guardo en  la variable
            valorPasswordEncriptadoSha = EncriptacionSha(passwordDB);

            //ahora muestro el valor de mi cadena
            //Response.Write("El valor encriptado de mi password ''S3perIr0nm@n'' " + " es: " + cadena);
            hashNuevo = EncriptacionSha(txtContrasena.Text);
            lblValorPasswordSha.Text = "El valor encriptado de mi password ''S3perIr0nm@n'' " + " es: " + valorPasswordEncriptadoSha
                + "<br/><br/> ahora comenzamos a verificar que la clave escrita conincida con la de" +
                " mi base de datos, que en este caso es simulada con la variable valorPasswordEncriptadoSha " + hashNuevo;
            if (valorPasswordEncriptadoSha == (passwordAVerificar=EncriptacionSha(txtContrasena.Text)))
            {
                lblVerificacionPassword.Text = "Password Correcto, ERES IRONMAN";

            }
            else
            {
                lblVerificacionPassword.Text = "INCORRECTO, TU NO ERES IRONMAN, ERES INEVITABLE";
            }



        }
    }
}