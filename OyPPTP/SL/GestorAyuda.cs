using System;
using System.Collections.Generic;
using System.Text;

namespace SL
{
    public class GestorAyuda
    {

        public GestorAyuda() { }

        public string ObtenerLinkAyuda(string codPantalla, int idIdioma) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ObtenerLinkAyuda(codPantalla, idIdioma);
        }

    }
}
