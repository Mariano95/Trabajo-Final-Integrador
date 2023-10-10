using System;
using System.Collections.Generic;
using System.Text;

namespace SL
{
    public class GestorPatentes
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GestorPatentes() {}

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    METODOS PUBLICOS     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public List<(int, string)> GetPatentes() {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaPatentes();
        }
        
        public List<string> ObtenerPatentes(int usuarioId, string pantalla) {
            GestorGrupos gestorGrupos = new GestorGrupos();
            List<int> grupos = gestorGrupos.ObtenerGrupos(usuarioId);
            
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.FiltrarPatentes(usuarioId, pantalla, grupos);
        }

        public List<(int, string)> GetPatentesUsuarioGrupo(int usuarioId, int grupoId = -1) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaPatentesUsuarioGrupo(usuarioId, grupoId);
        }

        public (bool, string) PuedeQuitar(int usuarioId, int patenteId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<int> usuariosConPatente = miDAL.UsuariosConPatente(patenteId);
            int repeticionesUser = 0;
            foreach (int otroUsuarioid in usuariosConPatente) {
                //Reviso que al menos haya un usuario distinto al que se esta editando y que tenga asignada la patente
                if (otroUsuarioid != usuarioId)
                {
                    return (true, "La patente puede ser quitada");
                }
                //Basicamente estoy contando cuantas veces aparece el usuario en la lista para poder darme cuenta de si tiene la patente asignada mas de una vez
                repeticionesUser++;
            }
            if (repeticionesUser > 1) {
                //Puedo sacar la patente porque la tiene asignada ademas en un grupo
                return (true, "La patente puede ser quitada");
            }
            return (false, "No se puede quitar esta patente, quedaría vacía.");
        }

        public void QuitarPatente(int usuarioId, int patenteId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.QuitarPatente(usuarioId, patenteId);
        }

        public void AgregarPatente(int usuarioId, int patenteId)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.AgregarPatente(usuarioId, patenteId);
        }

    }
}
