using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace SL
{
    public class GestorUsuarios
    {

        public GestorUsuarios() { }

        public bool EliminarUsuario(int usuarioId)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.EliminarUsuario(usuarioId);        
        }

    }
}
