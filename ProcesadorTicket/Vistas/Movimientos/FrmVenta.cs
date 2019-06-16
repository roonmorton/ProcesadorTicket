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
    public partial class FrmVenta: Form
    {
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("codigo");
                dt.Columns.Add("descripcion");

                dt.Columns.Add("cantidad");
                dt.Columns.Add("precio");
                dt.Columns.Add("subtotal");

                dt.Rows.Add("12381930123", "Galleta bimbo", "1", "10", "10.00");
                dt.Rows.Add("12381930123", "Producto x", "2", "10", "20.00");

                dt.Rows.Add("12381930123", "Producto 2", "5", "10", "50.00");


                grdData.DataSource = dt;


            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
