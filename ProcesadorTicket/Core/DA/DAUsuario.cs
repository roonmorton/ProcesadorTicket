using ProcesadorTicket.Core.DataBase;
using System;
using System.Data;

namespace ProcesadorTicket.Core.DA
{
    class DAUsuario:DB 
    {
        public Boolean insertaActualizar(string id, string nombres, string apellidos, string usuario, string estado)
        {
            Boolean res = false;
            try
            {
                if (id.Equals("0"))
                {
                    //Inserta
                    ejecutarConsulta("INSERT INTO TBL_Usuario ( nombres, apellidos,usuario,usuarioCreacion, estado, pass ) VALUES('" + nombres + "','" + apellidos + "','" + usuario +"','" + Globals.usuario + "',"+ estado+",'"+ Helper.GetMD5("x") +"');");
                }
                else
                {
                    // Helper.MensajeSistema("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
                    //Modific
                    ejecutarConsulta("UPDATE TBL_Usuario set estado = "+ estado +", nombres = '" + nombres + "', apellidos = '" + apellidos + "', usuarioModificacion = '" + Globals.usuario + "', usuario = '" + usuario + "', fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idUsuario = " + id );
                }
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                throw ex;
            }
            return res;
        }

        public DataTable buscar(string nombres="")
        {

            try
            {
                string query = "SELECT  top 20 (Switch(estado=0,'Inactivo',estado=1,'Activo'))  AS estadoUsuario,nombres, apellidos, idUsuario AS ID,usuario FROM TBL_Usuario WHERE configurable = 1 and nombres like '%"+nombres+"%'";
                //Helper.MensajeSistema(query);
                DataTable dt=  ejecutarConsultaDT(query);
                //Helper.MensajeSistema("Datos: " + dt.Rows.Count);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable validarUsuario(string usuario = "")
        {

            try
            {
                string query = "SELECT count(1) AS usuarios FROM TBL_Usuario WHERE usuario = '"+usuario+"'";
                //Helper.MensajeSistema(query);
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable validarUsuarioID(string usuario = "",string id="")
        {

            try
            {
                string query = "SELECT count(1) AS usuarios FROM TBL_Usuario WHERE usuario = '"+usuario+"' and idUsuario = "+id+"";
                //Helper.MensajeSistema(query);
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable obtenerConfigurable()
        {

            try
            {
                string query = "SELECT configurable FROM TBL_Usuario WHERE ucase(usuario) ='"+Globals.usuario.ToUpper()+"'";
                //Helper.MensajeSistema(query);
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Boolean eliminar(string id)
        {
            Boolean res = false;
            try
            {
                //Elimina
                //ejecutarConsulta("DELETE FROM SEC_TBL_UsuarioPermisos WHERE idUsuario = " + id + "");
                ejecutarConsulta("UPDATE TBL_Usuario set estado = 0 , fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' , usuarioModificacion = '" + Globals.usuario + "' WHERE idUsuario = " + id + "");
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                throw ex;
            }
            return res;
        }

        public Boolean resetPass(string id)
        {
            Boolean res = false;
            try
            {
                //Elimina
                ejecutarConsulta("UPDATE TBL_Usuario set reset =  1, pass = '" + Helper.GetMD5("x") + "' , fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "', usuarioModificacion = '" + Globals.usuario + "' WHERE IdUsuario = " + id + "");
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
