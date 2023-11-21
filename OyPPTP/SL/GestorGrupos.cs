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
            gestorBitacora.RegistrarEvento(13, usuarioId, id_grupo:grupo_id);

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

        public void QuitarUsuarioGrupo(int usuarioId, int grupoId)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.QuitarUsuarioGrupo(usuarioId, grupoId);
        }

        public void EliminarGrupo(int grupoId) {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.EliminarGrupo(grupoId);
        }

        public (bool, string) QuitarPatente(int usuarioId, int grupoId, int patenteId)
        {
            GestorPatentes gestorPatentes = new GestorPatentes();
            (bool, string) resultado = gestorPatentes.PuedeQuitarDelGrupo(grupoId, patenteId);
            if (!resultado.Item1) {
                return resultado;
            }

            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.QuitarPatenteDelGrupo(grupoId, patenteId);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(20, usuarioId, id_grupo: grupoId, id_patente: patenteId);

            int sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Persona_Grupo");
            bool verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Persona_Grupo", sumaVerificadoresHorizontales);

            return (true, "Exito al quitar la patente del grupo");
        }

        public (bool, string) AgregarPatente(int usuarioId, int grupoId, int patenteId)
        {
            DAL.DAL miDAL = DAL.DAL.GetDAL();
            miDAL.AgregarPatenteAGrupo(grupoId, patenteId);

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.RegistrarEvento(19, usuarioId, id_grupo: grupoId, id_patente: patenteId);

            string concatenado = "";
            concatenado += grupoId.ToString();
            concatenado += patenteId.ToString();
            int verificador_horizontal = 0;
            int contador = 1;
            foreach (char caracter in concatenado)
            {
                verificador_horizontal += (int)caracter * contador;
                contador++;
            }

            miDAL.ActualizarVerificadorHorizontalGrupoPatente(grupoId, patenteId, verificador_horizontal);

            int sumaVerificadoresHorizontales = 0;
            bool verificador_vertical_ok;
            sumaVerificadoresHorizontales = miDAL.ObtenerSumaVerificadoresHorizontales("Grupo_Patente");
            verificador_vertical_ok = miDAL.ActualizarVerificadorVertical("Grupo_Patente", sumaVerificadoresHorizontales);

            return (true, "Exito al agregar la patente al grupo");
        }

    }
}
