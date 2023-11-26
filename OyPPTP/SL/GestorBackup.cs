using System;
using System.Collections.Generic;
using System.Text;
using SL;
using DAL;

namespace SL
{
    public class GestorBackup
    {
        public GestorBackup() { }

        public (bool, string) GenerarArchivoBackup(string filename){
            try
            {
                DAL.DAL miDAL = DAL.DAL.GetDAL();
                return miDAL.GenerarArchivoBackup(filename);
            }
            catch (Exception e)
            {
                return (false, e.Message.ToString());
            }
        }

        public bool RestaurarSistema(string filename)
        {
            
            try
            {
                DAL.DAL miDAL = DAL.DAL.GetDAL();
                return miDAL.RestaurarSistema(filename);
            }
            catch (Exception e) {
                return false;
            }

        
        }


    }
}
