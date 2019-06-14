using ProcesadorTicket.Core.DataBase;
using System;
using System.Data;

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
        
    }
}
