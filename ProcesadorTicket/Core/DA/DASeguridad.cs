using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DASeguridad : DB
    {
        public DataTable login(string usuario, string pass)
        {

            try
            {
                return ejecutarConsultaDT("SELECT TBL_Usuario.idUsuario, TBL_Usuario.usuario FROM TBL_Usuario WHERE TBL_Usuario.pass = '" + Helper.GetMD5(pass) + "'and TBL_Usuario.usuario = '" + usuario + "' and estado = 1; ");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Boolean cambioPass(string nuevaPass, string usuario)
        {
            Boolean res = false;
            try
            {
                ejecutarConsulta("UPDATE TBL_USUARIO SET TBL_USUARIO.pass = '" + Helper.GetMD5(nuevaPass) + "' , fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "', usuarioModificacion = '" + Globals.usuario + "' WHERE TBL_USUARIO.usuario = '" + usuario + "'");
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                throw ex;
            }
            return res;
        }

        public Boolean validarPermisoUsuario(string usuario, string permiso)
        {
            Boolean result = false;
            try
            {
                string query = "SELECT SEC_TBL_UsuarioPermiso.estado, SEC_TBL_Permiso.descripcion, SEC_TBL_Permiso.informacion FROM (SEC_TBL_Permiso LEFT JOIN SEC_TBL_UsuarioPermiso ON SEC_TBL_Permiso.IdPermiso = SEC_TBL_UsuarioPermiso.idPermiso) LEFT JOIN TBL_Usuario ON TBL_Usuario.idUsuario = SEC_TBL_UsuarioPermiso.idUsuario where TBL_Usuario.usuario = '" + usuario + "' AND SEC_TBL_Permiso.descripcion = '" + permiso + "'";
                System.Console.WriteLine(query);
                DataTable dtSeg = ejecutarConsultaDT(query);

                if (dtSeg.Rows.Count > 0)
                    if (dtSeg.Rows[0]["estado"].ToString().Equals("1"))
                        result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
    }
}
