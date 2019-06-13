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
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("descripcion");
                
                dt.Columns.Add("precio");
                dt.Columns.Add("stock");
                dt.Columns.Add("codigo");

                dt.Rows.Add("1", "Galleta bimbo", "10.00", "145","12381930123");
                dt.Rows.Add("1", "Producto x", "10.00", "145", "12381930123");

                dt.Rows.Add("1", "Producto 2", "10.00", "10", "12381930123");
                dt.Rows.Add("1", "Producto", "10.00", "76", "12381930123");


                grdData.DataSource = dt;


            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
