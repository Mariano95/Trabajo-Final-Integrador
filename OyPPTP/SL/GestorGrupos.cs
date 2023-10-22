using System;
using System.Collections.Generic;
using System.Text;
using DAL;

namespace SL
{
    public class GestorGrupos
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public GestorGrupos() { }

        /////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////    METODOS PUBLICOS     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public List<int> ObtenerGrupos(int usuarioId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ObtenerGrupos(usuarioId);
        }

        public List<int> GetGruposPorUsuario(int usuarioId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaGruposPorUsuario(usuarioId);
        }

        public List<(int, string)> GetGrupos()
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            return miDAL.ListaGrupos();
        }

        public List<(int, string, string, string)> GetUsuariosPorGrupo(int grupoId) {
            List<(int, string, string, string)> usuarios = new List<(int, string, string, string)>();

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            usuarios.AddRange(miDAL.ListaIntegrantes(grupoId));

            return usuarios;

        }

        public void CrearGrupo(string nombre_nuevo_grupo, int usuarioId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            nombre_nuevo_grupo = miDAL.EncriptarAES(nombre_nuevo_grupo);
            int grupo_id = miDAL.CrearGrupo(nombre_nuevo_grupo);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(13, usuarioId);

            string concatenado = "";
            concatenado += grupo_id.ToString();
            concatenado += nombre_nuevo_grupo;
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }

            bool verificador_horizontal_ok = miDAL.ActualizarVerificadorHorizontal("Grupo", grupo_id, verificador_horizontal);

            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Grupo");
            bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Grupo", sumaVerificadoresHorizontales);

        }

        public void AgregarUsuarioGrupo(int usuarioId, int grupoId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.AgregarUsuarioGrupo(usuarioId, grupoId);
        }

        public void EliminarGrupo(int grupoId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.EliminarGrupo(grupoId);
        }

    }
}
