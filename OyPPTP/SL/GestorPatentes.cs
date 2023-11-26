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


        public List<(int, string)> GetPatentesUsuarioGrupo(int usuarioId = -1, int grupoId = -1) {
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

        public (bool, string) PuedeQuitarDelGrupo(int grupoId, int patenteId)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<int> usuariosConPatente = miDAL.UsuariosConPatente(grupoId, patenteId);

            if (usuariosConPatente.Count > 0)
            {
                return (true, "La patente puede ser quitada del grupo");
            }
            
            return (false, "No se puede quitar esta patente del grupo, quedaría vacía.");
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

        public (bool, string) PuedeElminarGrupo(int grupoId) {

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<(int, string)> patentesGrupo = miDAL.ListaPatentes(grupoId);

            int patenteId;
            List<int> usuarios = new List<int>();
            foreach ((int, string) patente in patentesGrupo) {
                patenteId = patente.Item1;
                usuarios = miDAL.UsuariosConPatente(grupoId, patenteId);
                if (usuarios.Count < 1) {
                    return (false, "No se puede eliminar el grupo, quedarían patentes vacías.");
                }
            }
            return (true, "Ok");

        }

        public (bool, string) PuedeEliminarUsuario(int usuarioID)
        {

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<(int, string)> patentesUsuario = miDAL.ListaPatentesUsuario(usuarioID);

            int patenteId;
            List<int> usuarios = new List<int>();
            foreach ((int, string) patente in patentesUsuario)
            {
                patenteId = patente.Item1;
                usuarios = miDAL.UsuariosConPatente(patenteId);
                if (usuarios.Contains(usuarioID) && usuarios.Count == 1)
                {
                    return (false, "No se puede eliminar el usuario, quedarían patentes vacías.");
                }
            }
            return (true, "Ok");

        }

        public (bool, string) PuedeQuitarUsuarioGrupo(int usuarioId, int grupoId) {
            
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            List<int> usuariosGrupo = miDAL.ListaUsuariosPorGrupo(grupoId);
            if (usuariosGrupo.Count > 1) {
                return (true, "Ok");
            }

            List<(int, string)> patentesGrupo = miDAL.ListaPatentes(grupoId);
            List<int> usuariosConPatente;
            foreach ((int,string) patente in patentesGrupo)
            {
                usuariosConPatente = miDAL.UsuariosConPatente(grupoId, patente.Item1);
                if (!(usuariosConPatente.Count > 0)) {
                    return (false, "No se puede eliminar al usuario del grupo, quedarían patentes vacías.");
                }
            }

            return (true, "Ok");
        }

    }
}
