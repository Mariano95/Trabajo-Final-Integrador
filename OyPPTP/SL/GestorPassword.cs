using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SL
{
    public class GestorPassword
    {

        private static Random _random = new Random();

        public GestorPassword() { }

        public string GenerarPasswordRandom() {
            const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
            const string letrasMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";
            const string caracteresEspeciales = "$%!*";

            string caracteres = letrasMinusculas + letrasMayusculas + numeros + caracteresEspeciales;
            int longitud = _random.Next(8, 33); // Entre 8 y 32 caracteres

            StringBuilder sb = new StringBuilder();
            sb.Append(letrasMinusculas[_random.Next(letrasMinusculas.Length)]);
            sb.Append(letrasMayusculas[_random.Next(letrasMayusculas.Length)]);
            sb.Append(numeros[_random.Next(numeros.Length)]);
            sb.Append(caracteresEspeciales[_random.Next(caracteresEspeciales.Length)]);

            for (int i = 4; i < longitud; i++)
            {
                sb.Append(caracteres[_random.Next(caracteres.Length)]);
            }

            string resultado = new string(sb.ToString().OrderBy(c => _random.Next()).ToArray());
            return resultado;
        }

    }
}
