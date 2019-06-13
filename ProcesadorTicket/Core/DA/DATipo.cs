using ProcesadorTicket.Core.DataBase;
using System;
using System.Data;

namespace ProcesadorTicket.Core.DA
{
    class DATipo : DB
    {
        public DataTable seleccionar()
        {
            try
            {
                return ejecutarConsultaDT("SELECT idTipo, descripcion FROM TBL_Tipo ");
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
