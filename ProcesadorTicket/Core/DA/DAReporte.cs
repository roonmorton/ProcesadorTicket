﻿using ProcesadorTicket.Core.DataBase;
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

                return ejecutarConsultaDT("SELECT '' as id, (SELECT cli.nombres from TBL_Cliente as cli WHERE cli.idCliente = TBL_Ticket.idCliente) as nombres, TBL_Ticket.monto as MONTO, TBL_Tipo.descripcion as TIPO, format(Tbl_Ticket.fechaCreacion,'dd/MM/yyyy HH:mm:ss') as FECHA  FROM TBL_Tipo INNER JOIN TBL_Ticket ON TBL_Tipo.idTipo = TBL_Ticket.idTipo where"
+ "((format(TBL_TIcket.fecha, 'dd/MM/yyyy')  Between format('"+fechaInicio+"', 'dd/MM/yyyy') And format('"+fechaFin+"', 'dd/MM/yyyy'))) ORDER BY TBL_TIcket.fechaCreacion ASC");
            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
