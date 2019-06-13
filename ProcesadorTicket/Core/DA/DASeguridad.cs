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
                return ejecutarConsultaDT("SELECT TBL_Usuario.idUsuario, TBL_Usuario.usuario FROM TBL_Usuario WHERE TBL_Usuario.pass = '" + Helper.GetMD5( pass )+ "'and TBL_Usuario.usuario = '" + usuario + "'; ");

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
                ejecutarConsulta("UPDATE TBL_USUARIO SET TBL_USUARIO.pass = '"+ Helper.GetMD5( nuevaPass)+ "' , fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "', usuarioModificacion = '" + Globals.usuario + "' WHERE TBL_USUARIO.usuario = '" + usuario+"'");
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                throw ex;
            }
            return res;
        }
    }
}
