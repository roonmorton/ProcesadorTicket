using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcesadorTicket.Core.DA;
using System.Media;

namespace ProcesadorTicket
{
    public partial class UCEntradaProductot : UserControl
    {
        private DataTable detalle;
        private string pidProducto = "0";
        private string pCodigo = "0";
        private string pPrecio = "0";
        private string pStock = "0";
        private string pDescripcion = "0";

        public UCEntradaProductot()
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

        private void UCEntradaProducto_Load(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                
                //cargarComboUnidadMedia();
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
                btnGuardar.Enabled = false;
                if (detalle != null) detalle.Dispose();
                crearData();
                grdData.DataSource = detalle;
                txtContador.Text = "Productos("+ detalle.Rows.Count+")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void limpiarFormulario()
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
                //txtReferencia.Clear();
                txtCantidad.Clear();
                txtCodigo.Focus();
                btnGuardar.Enabled = detalle.Rows.Count > 0 ? true : false;
                grdData.DataSource = detalle;
                txtContador.Text = "Registros(" + detalle.Rows.Count + ")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void cargarComboUnidadMedia()
        //{
        //    try
        //    {
        //        DAUnidadMedida tipo = new DAUnidadMedida();
        //        cargarCombos(tipo.seleccionar(), this.cmbUnidadMedida);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

       /* private void cargarCombos(DataTable dataTable, ComboBox combo)
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
        */
        private void guardar()
        {
            try
            {
                if(grdData.Rows.Count < 0)
                {
                    Helper.MensajeSistema("No hay datos a guardar...");
                    return;
                }
                DAProducto producto = new DAProducto();

                if (producto.guardarDetalleEntrada(txtReferencia.Text.ToString(), DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss"), detalle))
                {
                    Helper.MensajeSistema("Guardado Correctamente...");
                    limpiar();
                }
                else
                    Helper.MensajeSistema("No se ha guardado, contactar con soporte...");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buscarProducto()
        {
            try
            {
                DAProducto producto = new DAProducto();
                DataTable dt = producto.buscarCodigo(txtCodigo.Text.ToString().Trim().Replace("H", ""));
                if (dt.Rows.Count == 1)
                {
                    pCodigo = dt.Rows[0]["codigo"].ToString();
                    pPrecio = dt.Rows[0]["precio"].ToString();
                    pStock = dt.Rows[0]["stock"].ToString();
                    pDescripcion = dt.Rows[0]["descripcion"].ToString();
                    pidProducto = dt.Rows[0]["idProducto"].ToString();
                    txtDescripcion.Text = pDescripcion;
                    txtCantidad.Focus();
                    SystemSounds.Hand.Play();
                    txtDescripcion.BackColor = Color.DarkSeaGreen;
                }
                else
                {
                    txtDescripcion.BackColor = Color.White;
                    Helper.MensajeSistema("Producto no encontrado, debe de registrarlo");

                }
            }
            catch (Exception ex)
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

                if (pidProducto.Equals("0"))
                {
                    Helper.MensajeSistema("Ingrese un codigo producto valido..");
                    txtCodigo.Focus();
                    return;
                }
                //if (cmbUnidadMedida.SelectedValue.ToString().Equals("0"))
                //{
                //    Helper.MensajeSistema("Selecionar un tipo para guardar...");
                //    cmbUnidadMedida.Focus();
                //    return;
                //}

                /* if (Int32.Parse(pStock) < 0)
                 {
                     Helper.MensajeSistema("No hay stock para procesar el producto...");

                     return;
                 }*/
                Boolean flag = false;
                DataRow dr = detalle.NewRow();
                if (detalle.Rows.Count > 0)
                {
                    foreach (DataRow row in detalle.Rows)
                    {
                        if (row["codigo"].ToString().Equals(pCodigo))// Si existe en el grid actualiza
                        {
                            row["cantidad"] = (Int32.Parse(row["cantidad"].ToString()) + Int32.Parse(txtCantidad.Text.ToString())).ToString();
                            flag = true;
                            break;
                        }
                    }
                }

                if (!flag)
                {
                    dr["idProducto"] = pidProducto;
                    dr["codigo"] = pCodigo;
                    dr["descripcion"] = pDescripcion;
                    dr["precio"] = pPrecio;
                    dr["cantidad"] = txtCantidad.Text.ToString();
                    dr["stock"] = pStock;
                    detalle.Rows.Add(dr);
                }
                
                
                //btnGuardar.Enabled = detalle.Rows.Count > 0 ? true : false;
                limpiarFormulario();
            }
            catch (Exception ex)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                guardar();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
            } catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscarProducto();
                //Helper.MensajeSistema("Código: " + txtCodigoProducto.Text);
            }
        }

        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        string id = grdData.SelectedRows[0].Cells["idProducto"].Value.ToString();
                        //DataTable dtAux = detalle;
                        foreach (DataRow dr in detalle.Rows)
                            if (dr["idProducto"].Equals(id))
                            {

                                detalle.Rows.Remove(dr);
                                break;
                            }
                        detalle.AcceptChanges();
                        //detalle = dtAux;
                        grdData.DataSource = detalle;
                        
                        /*txtNombres.Text = grdHistorico.SelectedRows[0].Cells["nombres"].Value.ToString();
                        txtApellidos.Text = grdHistorico.SelectedRows[0].Cells["apellidos"].Value.ToString();
                        txtUsuario.Text = grdHistorico.SelectedRows[0].Cells["usuario"].Value.ToString();
                        checkEstado.Checked = (grdHistorico.SelectedRows[0].Cells["estadoUsuario"].Value.ToString() == "Activo" ? true : false);
                        txtUsuario.Enabled = false;*/
                        break;
                   /* case 1:
                        DialogResult dr = MessageBox.Show("¿Reestablecer contraseña de usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            DAUsuario usuario = new DAUsuario();
                            usuario.resetPass(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            Helper.MensajeSistema("Se ha reestablecido la contraseña...");
                        }

                        break;
                    case 2:
                        string id = grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString();
                        string strUsuario = grdHistorico.SelectedRows[0].Cells["usuario"].Value.ToString();
                        FrmPermisos permisos = new FrmPermisos();
                        permisos.setIdUsuario(id);
                        permisos.setUsuario(strUsuario);
                        permisos.ShowDialog(this);
                        break;*/
                }
            }
            catch (Exception ex)
            {
                //idUsuario = "0";
                Helper.erroLog(ex);
            }
        }
    }
}
