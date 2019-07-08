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
                    ejecutarConsulta("INSERT INTO TBL_Cliente ( nombres, codigoEmpleado,usuarioCreacion ) VALUES('" + nombres + "','" + codigoEmpleado + "','" + Globals.usuario + "');");
                }
                else
                {
                    // Helper.MensajeSistema("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
                    //Modifica
                    ejecutarConsulta("UPDATE TBL_Cliente set nombres = '" + nombres + "', codigoEmpleado = '" + codigoEmpleado + "', usuarioModificacion = '" + Globals.usuario + "', fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idCliente = " + id + ";");
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
                return ejecutarConsultaDT("SELECT top 50 nombres,codigoEmpleado,idCliente FROM TBL_Cliente WHERE nombres LIKE '%"+cliente +"%' AND estado= 1 ORDER BY idCliente DESC");
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
                ejecutarConsulta("UPDATE TBL_Cliente set estado = 0 WHERE idCliente = " + id + "");

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
