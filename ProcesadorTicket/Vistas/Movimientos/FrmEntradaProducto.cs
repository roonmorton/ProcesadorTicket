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
                detalle.Columns.Add("stock");

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
                limpiar();
                cargarComboUnidadMedia();
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
                txtCantidad.Clear();
                txtCodigo.Focus();
                crearData();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void cargarComboUnidadMedia()
        {
            try
            {
                DAUnidadMedida tipo = new DAUnidadMedida();
                cargarCombos(tipo.seleccionar(), this.cmbUnidadMedida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarCombos(DataTable dataTable, ComboBox combo)
        {
            Dictionary<int, String> dicTipo = new Dictionary<int, string>();
            dicTipo.Add(0, "Seleccione un Tipo...");
            foreach (DataRow row in dataTable.Rows)
            {
                dicTipo.Add(Int32.Parse(row[0].ToString()), row[1].ToString());
            }
            combo.DisplayMember = "Value";
            combo.ValueMember = "Key";
            combo.DataSource = dicTipo.ToArray();
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
                    txtCodigo.Focus();
                    return;
                }
                if (txtCantidad.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar una cantidad..");
                    txtCantidad.Focus();
                    return;
                }

                if (cmbUnidadMedida.SelectedValue.ToString().Equals("0"))
                {
                    Helper.MensajeSistema("Selecionar un tipo para guardar...");
                    cmbUnidadMedida.Focus();
                    return;
                }

               /* if (Int32.Parse(pStock) < 0)
                {
                    Helper.MensajeSistema("No hay stock para procesar el producto...");
                    
                    return;
                }*/
                DataRow dr = detalle.NewRow();

                //detalle.Columns.Add("codigo");
                //detalle.Columns.Add("descripcion");
                //detalle.Columns.Add("precio");
                //detalle.Columns.Add("cantidad");
                //detalle.Columns.Add("stock");

                //detalle.Columns.Add("unidadMedida");
                //detalle.Columns.Add("idUnidadMedida");

                dr["idProducto"] = pidProducto;
                dr["codigo"] = pCodigo;
                dr["descripcion"] = pDescripcion;
                dr["precio"] = pPrecio;
                dr["cantidad"] = txtCantidad.Text.ToString();
                dr["stock"] = pStock;
                dr["idUnidadMedida"] = cmbUnidadMedida.SelectedValue.ToString();
                dr["unidadMedida"] = cmbUnidadMedida.SelectedText.ToString(); ;



                detalle.Rows.Add(dr);
                
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
            }
            catch (Exception ex)
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
