using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DATicket:DB
    {
        public Boolean insertaActualizar(string id, string cliente, string monto, string tipo)
        {
            Boolean res = false;
            try
            {
                if (id.Equals("0")){
                    //Inserta
                    ejecutarConsulta("INSERT INTO TBL_Ticket ( idCliente, monto, idTipo, fecha,usuarioCreacion ) VALUES('" + cliente+"','"+monto+"',"+tipo+",'"+ DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") +"','"+Globals.usuario+"');");
                }
                else
                {
                   // Helper.MensajeSistema("UPDATE TBL_Ticket set noTicket = '" + ticket + "', monto = " + monto + ", idTipo = " + tipo + ", usuarioModificacion = '" + Globals.usuario + "' WHERE idTicket = " + id + ";");
                    //Modifica
                    ejecutarConsulta("UPDATE TBL_Ticket set monto = "+monto+", idTipo = "+tipo+ ", usuarioModificacion = '" + Globals.usuario+"', fechaModificacion = '"+ DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idTicket = "+id+";");
                }
                res = true;
            }catch(Exception ex)
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
                return ejecutarConsultaDT("SELECT top 50 t.idTicket, t.idCliente as cliente, (SELECT cli.nombres from TBL_Cliente as cli WHERE cli.idCliente = t.idCliente) as nombres, t.monto, tipo.descripcion, format(t.fechaCreacion,'dd/MM/yyyy HH:mm:ss') as fecha,tipo.idTipo   FROM TBL_Ticket as t LEFT OUTER JOIN TBL_Tipo tipo ON t.idTipo = tipo.idTipo WHERE t.idCliente LIKE '%" + cliente.Trim() + "%' ORDER BY t.idTicket DESC");
            } catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public DataTable tiemposCliente(string idTipo, string idCliente,string fecha)
        {

            try
            {
                string query = "SELECT count(*) as tiempos FROM TBL_Ticket as ticket, TBL_TIPO as tipo WHERE ticket.idTipo = tipo.idTipo  and tipo.idTipo = " + idTipo + " and ticket.idCliente = " + idCliente + " and format(ticket.fecha,'dd/MM/yyyy') = format('" + fecha + "','dd/MM/yyyy')";
                //Helper.MensajeSistema("Query " + query);
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable cliente(string id, string codigoEmpleado)
        {

            try
            {
                return ejecutarConsultaDT("SELECT * FROM TBL_Cliente WHERE idCliente = "+id+" and codigoEmpleado = '"+codigoEmpleado+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable seleccionDia(string fecha)
        {
            try
            {
                //Helper.MensajeSistema("SELECT top 50 t.idTicket, t.noTicket, t.monto, tipo.descripcion, t.fecha  FROM TBL_Ticket as t LEFT OUTER JOIN TBL_Tipo tipo ON t.idTipo = tipo.idTipo WHERE t.fecha between #"+fecha+"# and #"+fecha+"# ORDER BY t.idTicket DESC");
                return ejecutarConsultaDT("SELECT top 50 t.idTicket,t.idCliente as cliente, (SELECT cli.nombres from TBL_Cliente as cli WHERE cli.idCliente = t.idCliente) as nombres, t.monto, tipo.descripcion, format(t.fechaCreacion,'dd/MM/yyyy HH:mm:ss') as fecha, tipo.idTipo  FROM TBL_Ticket as t LEFT OUTER JOIN TBL_Tipo tipo ON t.idTipo = tipo.idTipo WHERE  (((format(t.fecha,'dd/MM/yyyy'))  Between format('" + fecha+"','dd/MM/yyyy') And format('"+fecha+ "','dd/MM/yyyy'))) ORDER BY t.idTicket DESC");
            }
            catch(Exception ex)
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
                    ejecutarConsulta("DELETE FROM TBL_Ticket WHERE idTicket = "+id+"");
                
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
