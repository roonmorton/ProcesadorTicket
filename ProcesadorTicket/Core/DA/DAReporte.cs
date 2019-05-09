using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DAReporte:DB
    {

        public DataTable TicketFecha(string fechaInicio, string fechaFin)
        {
            try
            {

                return ejecutarConsultaDT("SELECT '' as id, TBL_Ticket.noTicket as TICKET, TBL_Ticket.monto as MONTO, TBL_Tipo.descripcion as TIPO, format(Tbl_Ticket.fecha,'dd/MM/yyyy') as FECHA  FROM TBL_Tipo INNER JOIN TBL_Ticket ON TBL_Tipo.idTipo = TBL_Ticket.idTipo where" 
+"((format(TBL_TIcket.fecha, 'dd/MM/yyyy')  Between format('"+fechaInicio+"', 'dd/MM/yyyy') And format('"+fechaFin+"', 'dd/MM/yyyy'))) ORDER BY TBL_TIcket.fecha ASC");
            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
