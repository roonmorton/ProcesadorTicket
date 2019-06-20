﻿using ProcesadorTicket.Core.DataBase;
using System;
using System.Data;
using System.Data.OleDb;

namespace ProcesadorTicket.Core.DA
{
    class DAProducto:DB 
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
                    string query = "UPDATE TBL_Producto SET TBL_Producto.codigo = '"+codigo+"', TBL_Producto.Descripcion = '"+descripcion+"', TBL_Producto.precio = '"+precio+"', TBL_Producto.usuarioModificacion = '"+Globals.usuario+"', TBL_Producto.fechaModificacion = '"+ DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss") + "' WHERE idProducto = " + idProducto;
                    ejecutarConsulta(query);
                }
                res = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return res;

        }

        public DataTable buscar(string producto)
        {
            try
            {
                string query = "SELECT TOP 20 TBL_Producto.idProducto AS ID, TBL_Producto.codigo, TBL_Producto.descripcion, TBL_Producto.precio,( SELECT SUM(TBL_StockProducto.cantidad) FROM TBL_StockProducto WHERE TBL_StockProducto.idProducto = TBL_Producto.idProducto) AS stock FROM TBL_Producto WHERE TBL_Producto.descripcion LIKE '%"+producto+"%' ORDER BY idProducto DESC";
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
                string query = "SELECT TBL_Producto.idProducto, TBL_Producto.codigo, TBL_Producto.descripcion, TBL_Producto.precio,   TBL_StockProducto.cantidad as stock FROM TBL_Producto  LEFT JOIN TBL_StockProducto ON  TBL_StockProducto.idProducto = TBL_Producto.idProducto WHERE TBL_Producto.codigo = '"+codigo+"'";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean guardarDetalle(DataTable data = null)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction transaccion = null;
            Boolean estado = false;
            string query = "";
            OleDbConnection con = null;
            try
            {
                if(data != null)
                {
                    if(data.Rows.Count > 0)
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

                        //Falta Agregar insercion de encabezado
                        foreach (DataRow row in data.Rows)
                        {
                            query = "SELECT idStockProducto FROM TBL_StockProducto WHERE idProducto = " + row["idProducto"].ToString();
                            cmd.CommandText = query;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            if(dt.Rows.Count > 0)//Actualiza
                                query = "UPDATE TBL_StockProducto SET cantidad = (cantidad+"+ row["cantidad"].ToString() +") WHERE idProducto = "+ row["idProducto"].ToString();
                            else //Inserta
                                query = "INSERT INTO TBL_StockProducto(idProducto, cantidad) VALUES("+ row["idProducto"].ToString() + ","+ row["cantidad"].ToString() + "     )";
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            dt.Dispose();
                            da.Dispose();
                            dt = new DataTable();
                            da = new OleDbDataAdapter();
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
            finally{
                if (con.State == ConnectionState.Open) con.Close();
            }
            return estado;

        }
    }
}
