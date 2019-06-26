using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket
{
    class DAPermiso : DB
    {
        public DataTable obtenerPermisos(string idUsuario = "0")
        {

            try
            {
                string sql = "SELECT \n" +
                "permisosTemp.idUsuarioPermiso,\n" +
                "permiso.idPermiso,\n" +
                "permisosTemp.estado,\n" +
                // "permisosTemp.idUsuario,\n" +
                // "permisosTemp.usuario,\n" +
                "permiso.descripcion,\n" +
                "permiso.informacion\n" +
                "FROM SEC_TBL_Permiso AS permiso \n" +
                "LEFT JOIN \n" +
                "(\n" +
                "SELECT \n" +
                "usuarioPermiso.idUsuarioPermiso,\n" +
                "usuarioPermiso.idPermiso,\n" +
                "usuarioPermiso.estado,\n" +
                "usuarioPermiso.idUsuario,\n" +
                "usuario.usuario\n" +
                " FROM SEC_TBL_UsuarioPermiso AS UsuarioPermiso \n" +
                "INNER JOIN TBL_Usuario AS Usuario ON\n" +
                "usuario.idUsuario = UsuarioPermiso.idUsuario\n" +
                "WHERE usuario.idUsuario = " + idUsuario + "\n" +
                ") AS permisosTemp\n" +
                "ON permisosTemp.idPermiso = permiso.idPermiso";
                return ejecutarConsultaDT(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean guardar(DataTable dtPermisos, string idUsuario = "0")
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction transaccion = null;
            Boolean estado = false;

            OleDbConnection con = null;
            try
            {
                if (dtPermisos != null)
                {
                    if (dtPermisos.Rows.Count > 0)
                    {
                        con = getCon();
                        if (con.State == ConnectionState.Open) con.Close();
                        con.Open();
                        cmd.Connection = con;
                        transaccion = con.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Connection = con;
                        cmd.Transaction = transaccion;
                        cmd.CommandType = CommandType.Text;
                        DataTable dt = new DataTable();
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        string query = "";
                        string pEstado = "";
                        foreach (DataRow row in dtPermisos.Rows)
                        {
                            pEstado = row["estado"].ToString().Equals("") ? "0" : row["estado"].ToString();
                            if (!row["idUsuarioPermiso"].ToString().Equals(""))
                                query = "UPDATE SEC_TBL_UsuarioPermiso SET estado = " + pEstado + " WHERE idUsuarioPermiso = " + row["idUsuarioPermiso"].ToString();
                            else
                                query = "INSERT INTO SEC_TBL_UsuarioPermiso(idPermiso,idUsuario, estado) values(" + row["idPermiso"].ToString() + "," + idUsuario + "," + pEstado + ")";
                            System.Console.WriteLine(query);
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                        con.Close();
                        estado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaccion != null) transaccion.Rollback();
                if (con.State == ConnectionState.Open) con.Close();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return estado;
        }
    }
}
