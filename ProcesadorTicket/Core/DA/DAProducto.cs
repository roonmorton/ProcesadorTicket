using ProcesadorTicket.Core.DataBase;
using System;
using System.Data;
using System.Data.OleDb;

namespace ProcesadorTicket.Core.DA
{
    class DAProducto : DB
    {

        public Boolean AgregarActualizar(string idProducto, string codigo, string descripcion, string precio)
        {

            Boolean res = false;
            try
            {
                if (idProducto.Equals("0"))
                {
                    string query = "INSERT INTO TBL_Producto(codigo,descripcion, precio,usuarioCreacion) values('" + codigo + "','" + descripcion + "'," + precio + ",'" + Globals.usuario + "')";
                    ejecutarConsulta(query);
                }
                else
                {
                    string query = "UPDATE TBL_Producto SET TBL_Producto.codigo = '" + codigo + "', TBL_Producto.Descripcion = '" + descripcion + "', TBL_Producto.precio = '" + precio + "', TBL_Producto.usuarioModificacion = '" + Globals.usuario + "', TBL_Producto.fechaModificacion = '" + DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idProducto = " + idProducto;
                    ejecutarConsulta(query);
                }
                res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;

        }

        public DataTable buscar(string producto)
        {
            try
            {
                string query = "SELECT TOP 20 TBL_Producto.idProducto AS ID, TBL_Producto.codigo, TBL_Producto.descripcion, TBL_Producto.precio,TBL_Producto.cantidad AS stock FROM TBL_Producto WHERE TBL_Producto.descripcion LIKE '%" + producto + "%' ORDER BY TBL_Producto.idProducto DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable buscarCodigo(string codigo)
        {
            try
            {
                string query = "SELECT TBL_Producto.idProducto, TBL_Producto.codigo, TBL_Producto.descripcion, TBL_Producto.precio,   TBL_Producto.cantidad as stock FROM TBL_Producto  WHERE TBL_Producto.codigo = '" + codigo + "'";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean guardarDetalleEntrada(string referencia, string fecha, DataTable data = null)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction transaccion = null;
            Boolean estado = false;
            string query = "";
            OleDbConnection con = null;
            try
            {
                if (data != null)
                {
                    if (data.Rows.Count > 0)
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

                        //Insertar Encabezado
                        query = "INSERT INTO TBL_EntradaProducto(fecha, referencia,usuarioCreacion) values('" + fecha.ToString() + "','" + referencia + "','" + Globals.usuario + "')";
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        //Obtiene ID Ingresado en entrada
                        query = "Select @@Identity AS ID";
                        cmd.CommandText = query;
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        string idEntradaProducto = (dt.Rows.Count > 0) ? dt.Rows[0]["ID"].ToString() : "X";


                        if (!idEntradaProducto.Equals("X"))
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                query = "UPDATE TBL_Producto SET cantidad = (cantidad + " + row["cantidad"].ToString() + "), usuarioModificacion='" + Globals.usuario + "' WHERE idProducto = " + row["idProducto"].ToString();
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                query = "INSERT INTO TBL_DetalleEntradaProducto(idProducto,idEntradaProducto,cantidad,usuarioCreacion,precioEntrada) values(" + row["idProducto"].ToString() + "," + idEntradaProducto + "," + row["cantidad"].ToString() + ",'" + Globals.usuario + "'," + row["precio"].ToString() + ");";
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                            }
                            transaccion.Commit();
                            con.Close();
                            estado = true;
                        }
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

        public Boolean guardarDetalleVenta(string fecha, string idCliente = "", DataTable data = null)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction transaccion = null;
            Boolean estado = false;
            string query = "";
            OleDbConnection con = null;
            try
            {
                if (data != null)
                {
                    if (data.Rows.Count > 0)
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

                        //Insertar Encabezado
                        if (idCliente.Equals("0"))
                            query = "INSERT INTO TBL_VENTA(fecha,total,usuarioCreacion) values('" + fecha.ToString() + "'," + data.Compute("sum(subtotal)", "") + ",'" + Globals.usuario + "')";
                        else
                            query = "INSERT INTO TBL_VENTA(fecha,idCliente,total,usuarioCreacion) values('" + fecha.ToString() + "'," + idCliente + "," + data.Compute("sum(subtotal)", "") + ",'" + Globals.usuario + "')";

                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        //Obtiene ID Ingresado en entrada
                        query = "Select @@Identity AS ID";
                        cmd.CommandText = query;
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        string idVenta = (dt.Rows.Count > 0) ? dt.Rows[0]["ID"].ToString() : "X";


                        if (!idVenta.Equals("X"))
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                query = "UPDATE TBL_Producto SET cantidad = (cantidad - " + row["cantidad"].ToString() + "), usuarioModificacion = '" + Globals.usuario + "' WHERE idProducto = " + row["idProducto"].ToString();
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                query = "INSERT INTO TBL_DetalleVenta(idProducto, idVenta,cantidad,precioVenta,usuarioCreacion) VALUES(" + row["idProducto"].ToString() + "," + idVenta + "," + row["cantidad"].ToString() + "," + row["precio"].ToString() + ",'" + Globals.usuario + "');";
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                            }
                            transaccion.Commit();
                            con.Close();
                            estado = true;
                        }
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
