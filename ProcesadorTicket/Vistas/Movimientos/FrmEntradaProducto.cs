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
        private DataTable detalle;
        private string pidProducto = "0";
        private string pCodigo = "0";
        private string pPrecio = "0";
        private string pStock = "0";
        private string pDescripcion = "0";
        public FrmEntradaProducto()
        {
            InitializeComponent();
           
        }

        private void crearData()
        {
            try
            {
                detalle = new DataTable();
                detalle.Columns.Add("idProducto");

                detalle.Columns.Add("codigo");
                detalle.Columns.Add("descripcion");
                detalle.Columns.Add("precio");
                detalle.Columns.Add("cantidad");
                detalle.Columns.Add("unidadMedida");
                detalle.Columns.Add("idUnidadMedida");

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private void limpiar()
        {
            try
            {
                pidProducto = "0";
                pCodigo = "0";
                pPrecio = "0";
                pStock = "0";
                pDescripcion = "0";
                txtCodigo.Clear();
                txtDescripcion.Clear();
                txtReferencia.Clear();
                //txtPrecio.Clear();
                //txtStock.Clear();
                txtCantidad.Clear();
                txtCodigo.Focus();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void cargarData()
        {
            try
            {

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void buscarProducto()
        {
            try
            {
                DAProducto producto = new DAProducto();
                DataTable dt = producto.buscarCodigo(txtCodigo.Text.ToString().Trim().Replace("H","")) ;
                if (dt.Rows.Count == 1)
                {
                    pCodigo = dt.Rows[0]["codigo"].ToString();
                    pPrecio = dt.Rows[0]["precio"].ToString();
                    pStock = dt.Rows[0]["stock"].ToString();
                    pDescripcion = dt.Rows[0]["descripcion"].ToString();
                    pidProducto = dt.Rows[0]["idProducto"].ToString();
                    txtDescripcion.Text = pDescripcion;
                    //txtPrecio.Text = pPrecio;
                    //txtStock.Text = pStock;
                    /*detalle.Rows.Add(
                        dt.Rows[0]["codigo"].ToString(),
                        dt.Rows[0]["descripcion"].ToString(),
                        dt.Rows[0]["precio"].ToString(), 
                        txtCantidad.Text.ToString(),
                        cmbUnidadMedida.SelectedText.ToString(),
                        cmbUnidadMedida.SelectedValue.ToString()
                        );*/
                }
                else
                    Helper.MensajeSistema("Producto no encontrado, debe de registrarlo");
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
                
                if(Int32.Parse(pStock) < 0)
                {
                    Helper.MensajeSistema("No hay stock para procesar el producto...");
                    return;
                }

                detalle.Rows.Add(
                        pidProducto,
                        pCodigo,
                        pDescripcion,
                        pPrecio,
                        txtCantidad.Text.ToString(),
                        cmbUnidadMedida.SelectedText.ToString(),
                        cmbUnidadMedida.SelectedValue.ToString()
                        );
                grdData.DataSource = detalle;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                agregarProducto();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    string texto = ((TextBox)sender).Text.ToString();
                    if (texto.Length > 0)
                    {
                        //Helper.MensajeSistema("ultimo: " + texto[texto.Length]);
                        if (texto[texto.Length - 1] == 'H')
                        {
                        buscarProducto();
                        }
                    }
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
