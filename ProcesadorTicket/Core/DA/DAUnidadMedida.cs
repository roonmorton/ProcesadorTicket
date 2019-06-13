using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DAUnidadMedida : DB
    {

        public Boolean insertaActualizar(string id, string descripcion)
        {
            Boolean res = false;
            try
            {
                if (id.Equals("0"))
                {
                    //Inserta
                    ejecutarConsulta("INSERT INTO TBL_UnidadMedida(descripcion,usuarioCreacion) values('"+descripcion+"','"+Globals.usuario+"')");
                }
                /*else
                {
                    // Helper.MensajeSistema("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
                    //Modifica
                    ejecutarConsulta("UPDATE TBL_Cliente set nombres = '" + nombres + "', codigoEmpleado = '" + codigoEmpleado + "', usuarioModificacion = '" + Globals.usuario + "', fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idCliente = " + id + ";");
                }*/
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                throw ex;
            }
            return res;
        }
        public DataTable buscar(string descripcion)
        {

            try
            {
                return ejecutarConsultaDT("SELECT top 50 descripcion, idUnidadMedida As ID FROM TBL_UnidadMedida WHERE descripcion LIKE '%"+descripcion+"%' ORDER BY idUnidadMedida DESC");
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
                ejecutarConsulta("DELETE FROM TBL_Cliente WHERE idCliente = " + id + "");

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
