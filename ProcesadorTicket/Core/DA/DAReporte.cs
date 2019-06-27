using ProcesadorTicket.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesadorTicket.Core.DA
{
    class DAReporte : DB
    {

        public DataTable TicketFecha(string fechaInicio, string fechaFin)
        {
            try
            {

                return ejecutarConsultaDT("SELECT '' as id, (SELECT cli.nombres from TBL_Cliente as cli WHERE cli.idCliente = TBL_Ticket.idCliente) as nombres, TBL_Ticket.monto as MONTO, TBL_Tipo.descripcion as TIPO, format(Tbl_Ticket.fechaCreacion,'dd/MM/yyyy HH:mm:ss') as FECHA  FROM TBL_Tipo INNER JOIN TBL_Ticket ON TBL_Tipo.idTipo = TBL_Ticket.idTipo where"
+ "((format(TBL_TIcket.fecha, 'dd/MM/yyyy')  Between format('" + fechaInicio + "', 'dd/MM/yyyy') And format('" + fechaFin + "', 'dd/MM/yyyy'))) ORDER BY TBL_TIcket.fechaCreacion ASC");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable VentaFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                string query = "SELECT \n" +
                    "	venta.idVenta AS ID_VENTA,\n" +
                    "	venta.fechaCreacion AS FECHA_VENTA,\n" +
                    "	(SELECT COUNT(1) FROM TBL_DetalleVenta WHERE TBL_DetalleVenta.idVenta = venta.idVenta ) AS CANTIDAD_VENTA,\n" +
                    "	venta.total AS TOTAL_VENTA,\n" +
                    "	IIf(cliente.idCliente IS NULL, \"S/N\", cliente.nombres) AS CLIENTE,\n" +
                    "	venta.usuarioCreacion AS USUARIO_VENTA\n" +
                    "	FROM TBL_VENTA AS venta\n" +
                    "	LEFT JOIN TBL_Cliente AS cliente\n" +
                    "	ON venta.idCliente = cliente.idCliente\n" +
                    "	WHERE \n" +
                    "	format(venta.fechaCreacion, 'dd/MM/yyyy')  \n" +
                    "	BETWEEN format('"+fechaInicio+"', 'dd/MM/yyyy') AND format('"+fechaFin+ "', 'dd/MM/yyyy') ORDER BY venta.fechaCreacion DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VentaDetalleFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                string query = "SELECT \n" +
                "	venta.idVenta AS ID_VENTA,\n" +
                "	venta.fechaCreacion AS FECHA_VENTA,\n" +
                "	IIF(\n" +
                "		(SELECT cliente.nombres from TBL_Cliente AS cliente WHERE cliente.idCliente = venta.idCliente ) is null, \n" +
                "		\"S/N\", \n" +
                "		(SELECT cliente.nombres from TBL_Cliente AS cliente WHERE cliente.idCliente = venta.idCliente )\n" +
                "		)\n" +
                "	AS CLIENTE,\n" +

                "	detalle.descripcion AS DESCRIPCION_PRODUCTO,\n" +
                "	detalle.cantidad AS CANTIDAD_PRODUCTO,\n" +
                "	detalle.precioVenta AS PRECIO_VENTA,\n" +
                "	venta.usuarioCreacion AS USUARIO_VENTA\n" +
                "	FROM TBL_Venta AS venta\n" +
                "	LEFT JOIN \n" +
                "	(\n" +
                "	SELECT \n" +
                "	producto.idProducto,\n" +
                "	producto.codigo,\n" +
                "	producto.descripcion,\n" +
                "	detalle.idVenta,\n" +
                "	detalle.cantidad,\n" +
                "	detalle.precioVenta\n" +
                "	FROM TBL_Producto AS producto \n" +
                "	INNER JOIN TBL_DetalleVenta AS detalle \n" +
                "	ON producto.idProducto = detalle.idProducto\n" +
                "	) AS detalle \n" +
                "	ON detalle.idVenta = venta.idVenta\n" +
                "	WHERE \n" +
                "	format(venta.fechaCreacion, 'dd/MM/yyyy')  \n" +
                "	BETWEEN format('"+fechaInicio+"', 'dd/MM/yyyy') AND format('"+fechaFin+ "', 'dd/MM/yyyy') ORDER BY venta.fechaCreacion DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EntradaFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                string query = "SELECT \n" +
"	entrada.idEntradaProducto AS ID_ENTRADA,\n" +
"	entrada.fechaCreacion AS FECHA_ENTRADA,\n" +
"	entrada.referencia AS REFERENCIA_ENTRADA,\n" +
"	(SELECT COUNT(1) FROM TBL_DetalleEntradaProducto WHERE TBL_DetalleEntradaProducto.idEntradaProducto = entrada.idEntradaProducto ) AS CANTIDAD_PRODUCTOS,\n" +
"	entrada.usuarioCreacion AS USARIO_ENTRADA\n" +
"	FROM TBL_EntradaProducto AS entrada\n" +
"	WHERE \n" +
"	format(entrada.fechaCreacion, 'dd/MM/yyyy')  \n" +
"	BETWEEN format('"+fechaInicio+"', 'dd/MM/yyyy') AND format('"+fechaFin+ "', 'dd/MM/yyyy') ORDER BY entrada.fechaCreacion DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable EntradaDetalleFecha(string fechaInicio, string fechaFin)
        {
            try
            {
                string query = "SELECT \n" +
"	entrada.idEntradaProducto AS ID_ENTRADA,\n" +
"	entrada.fechaCreacion AS FECHA_ENTRADA,\n" +
"	entrada.referencia AS REFERENCIA_ENTRADA,\n" +
"	detalle.descripcion AS DESCRIPCION_PRODUCTO,\n" +
"	detalle.cantidad AS CANTIDAD_PRODUCTO,\n" +
"	detalle.precioEntrada AS PRECIO_ENTRADA,\n" +
"	entrada.usuarioCreacion AS USUARIO\n" +
"	FROM TBL_EntradaProducto AS entrada\n" +
"	RIGHT JOIN \n" +
"	(\n" +
"	SELECT \n" +
"	producto.idProducto,\n" +
"	producto.codigo,\n" +
"	producto.descripcion,\n" +
"	detalle.idEntradaProducto,\n" +
"	detalle.cantidad,\n" +
"	detalle.precioEntrada\n" +
"	FROM TBL_Producto AS producto \n" +
"	INNER JOIN TBL_DetalleEntradaProducto AS detalle \n" +
"	ON producto.idProducto = detalle.idProducto\n" +
"	) AS detalle \n" +
"	ON detalle.idEntradaProducto = entrada.idEntradaProducto\n" +
"	WHERE \n" +
"	format(entrada.fechaCreacion, 'dd/MM/yyyy')  \n" +
"	BETWEEN format('"+fechaInicio+"', 'dd/MM/yyyy') AND format('"+fechaFin+ "', 'dd/MM/yyyy') ORDER BY entrada.fechaCreacion DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable productosMasVendidos()
        {
            try
            {
                string query = "SELECT \n" +
"	producto.idProducto AS ID_PRODUCTO,\n" +
"	producto.codigo AS CODIGO, \n" +
"	producto.descripcion AS DESCRIPCION, \n" +
"	count(venta.cantidad) AS CANTIDAD_VENTAS \n" +
"	FROM TBL_Producto AS producto\n" +
"	INNER JOIN TBL_DetalleVenta AS venta\n" +
"	ON producto.idProducto = venta.idVenta \n" +
"	GROUP BY \n" +
"	producto.idProducto, \n" +
"	producto.descripcion,\n" +
"	producto.codigo \n" +
"	ORDER BY count(venta.cantidad) DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable productoMasVendido()
        {
            try
            {
                string query = "SELECT TOP 1\n" +
"	producto.idProducto AS ID_PRODUCTO,\n" +
"	producto.codigo AS CODIGO_PRODUCTO, \n" +
"	producto.descripcion AS DESCRIPCION, \n" +
"	count(venta.cantidad) AS CANTIDAD_VENTAS \n" +
"	FROM TBL_Producto AS producto\n" +
"	INNER JOIN TBL_DetalleVenta AS venta\n" +
"	ON producto.idProducto = venta.idVenta \n" +
"	GROUP BY \n" +
"	producto.idProducto, \n" +
"	producto.descripcion,\n" +
"	producto.codigo \n" +
"	ORDER BY count(venta.cantidad) DESC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable productos()
        {
            try
            {
                string query = "SELECT \n" +
"TBL_Producto.idProducto AS ID_PRODUCTO, \n" +
"TBL_Producto.codigo AS CODIGO_PRODUCTO, \n" +
"TBL_Producto.Descripcion AS DESCRIPCION, \n" +
"TBL_Producto.precio AS PRECIO, \n" +
"TBL_Producto.cantidad AS STOCK\n" +
"FROM TBL_Producto ORDER BY TBL_Producto.Descripcion ASC";
                return ejecutarConsultaDT(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
