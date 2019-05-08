using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DASeguridad
    {
        public DataTable login(string usuario, string pass)
        {

            try
            {
                DB db = new DB();
                return db.ejecutarConsulta("SELECT TBL_Usuario.idUsuario, TBL_Usuario.usuario FROM TBL_Usuario WHERE TBL_Usuario.pass = '" + pass + "'and TBL_Usuario.usuario = '"+ usuario +"'; ");

            }catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
