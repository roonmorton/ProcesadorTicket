using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmEntradaProducto : Form
    {
        public FrmEntradaProducto()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void FrmEntradaProducto_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");

                dt.Columns.Add("precio");
                dt.Columns.Add("cantidad");
                dt.Columns.Add("unidadMedida");

                dt.Rows.Add("12381930123", "Galleta bimbo", "10.00", "145", "CAJA");
                dt.Rows.Add("12381930123", "Producto x", "10.00", "145", "CAJA");

                dt.Rows.Add("12381930123", "Producto 2", "10.00", "10", "PAQUETE");
                dt.Rows.Add("12381930123", "Producto", "10.00", "76", "PAQUETE");


                grdData.DataSource = dt;


            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
