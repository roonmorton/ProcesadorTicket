using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DACliente :DB
    {
        public Boolean insertaActualizar(string id, string nombres, string codigoEmpleado)
        {
            Boolean res = false;
            try
            {
                if (id.Equals("0"))
                {
                    //Inserta
                    ejecutarConsulta("INSERT INTO TBL_Ticket ( noTicket, monto, idTipo, fecha,usuarioCreacion ) VALUES('" + ticket + "','" + monto + "'," + tipo + ",'" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "','" + Globals.usuario + "');");
                }
                else
                {
                    // Helper.MensajeSistema("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
                    //Modifica
                    ejecutarConsulta("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
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

        public DataTable buscar(string cliente)
        {

            try
            {
                return ejecutarConsultaDT("SELECT top 50 * FROM TBL_Cliente ORDER BY t.idCliente DESC");
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
