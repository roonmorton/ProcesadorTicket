using ProcesadorTicket.Core.DA;
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
        private DataTable detalle = new DataTable();
        private string pidProducto = "0";
        private string pPrecio = "0";
        private string pStock = "0";
        private string pDescripcion = "0";
        public FrmEntradaProducto()
        {
            InitializeComponent();

            detalle.Columns.Add("codigo");
            detalle.Columns.Add("descripcion");
            detalle.Columns.Add("precio");
            detalle.Columns.Add("cantidad");
            detalle.Columns.Add("unidadMedida");
            detalle.Columns.Add("idUnidadMedida");

        }

        private void FrmEntradaProducto_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void buscarProducto()
        {
            try
            {
                DAProducto producto = new DAProducto();
                DataTable dt = producto.buscarCodigo(txtCodigo.Text.ToString().Trim());
                if(dt.Rows.Count == 1)
                {
                    detalle.Rows.Add(
                        dt.Rows[0]["codigo"].ToString(),
                        dt.Rows[0]["descripcion"].ToString(),
                        dt.Rows[0]["precio"].ToString(), 
                        txtCantidad.Text.ToString(),
                        cmbUnidadMedida.SelectedText.ToString(),
                        cmbUnidadMedida.SelectedValue.ToString()
                        );
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        private void agregarProducto()
        {
            try
            {
                if (txtCodigo.Text.Equals(""))
                {
                    Helper.MensajeSistema("Codigo no debe de estar vacio...");
                    return;
                }
                if (txtCantidad.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar una cantidad..");
                    return;
                }
                

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
