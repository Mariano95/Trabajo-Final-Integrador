using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace SL
{
    public class GestorIdioma
    {

        public List<(int, string)> ObtenerListadoIdiomas(int idioma_actual)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ObtenerListadoIdiomas(idioma_actual);
        }

        public string ObtenerTextos(string control, int idIdioma) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ObtenerTextos(control, idIdioma);
        }
    }
}
