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
    public partial class UCVenta : UserControl
    {

        private DataTable detalle;
        private string pidProducto = "0";
        private string pCodigo = "0";
        private string pPrecio = "0";
        private string pStock = "0";
        //private string pDescripcion = "0";
        private string idCliente = "0";

        public UCVenta()
        {
            InitializeComponent();
        }

        
        private void buscarProducto()
        {
            try
            {
                DAProducto producto = new DAProducto();
                DataTable dt = producto.buscarCodigo(txtCodigoProducto.Text.ToString().Trim().Replace("H", ""));
                if (dt.Rows.Count == 1)
                {
                    pCodigo = dt.Rows[0]["codigo"].ToString();
                    pPrecio = dt.Rows[0]["precio"].ToString();
                    txtPrecio.Text = "Q."+Convert.ToDecimal( dt.Rows[0]["precio"].ToString()).ToString("N2");
                    pStock = dt.Rows[0]["stock"].ToString();
                    txtStock.Text = dt.Rows[0]["stock"].ToString();
                    //pDescripcion = dt.Rows[0]["descripcion"].ToString();
                    txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString();

                    pidProducto = dt.Rows[0]["idProducto"].ToString();
                    // txtFecha.Text = pDescripcion;
                    txtCantidad.Text = "1";
                    txtCantidad.Enabled = true;
                    txtCantidad.Focus();
                }
                else
                {
                    txtCantidad.Text = "1";
                    txtStock.Text = "0";
                    txtCantidad.Enabled = false;
                    Helper.MensajeSistema("Producto no encontrado, debe de registrarlo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void agregar()
        {
            try
            {
                if (txtCodigoProducto.Text.Equals(""))
                {
                    Helper.MensajeSistema("Codigo no debe de estar vacio...");
                    txtCodigoProducto.Focus();
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
                    Helper.MensajeSistema("Debe de ingresar una cantidad..");
                    txtCodigoProducto.Focus();
                    return;
                }
                /* if(Convert.ToDecimal(pStock) <= 0)
                 {
                     Helper.MensajeSistema("No hay Stock de este producto, verificar...");
                     txtCodigoProducto.Focus();
                    //return;
                 }*/
                if (Convert.ToInt32(txtCantidad.Text) < 0)
                {
                    Helper.MensajeSistema("Cantidad invalida, verificar...");
                    txtCantidad.Focus();
                    return;
                }

                if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Text))
                {
                    Helper.MensajeSistema("No hay Stock de este producto, verificar...");
                    txtCodigoProducto.Focus();
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
                //Decimal suma = 0;
                if (detalle.Rows.Count > 0)
                {
                    foreach (DataRow row in detalle.Rows)
                    {
                        if (row["codigo"].ToString().Equals(pCodigo))// Si existe en el grid actualiza
                        {
                            row["cantidad"] = (Int32.Parse(row["cantidad"].ToString()) + Int32.Parse(txtCantidad.Text.ToString())).ToString();
                            row["subtotal"] = Convert.ToDecimal(row["cantidad"].ToString()) * Convert.ToDecimal(pPrecio);
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    dr["idProducto"] = pidProducto;
                    dr["codigo"] = pCodigo;
                    dr["descripcion"] = txtDescripcion.Text;
                    dr["precio"] = Convert.ToDecimal(pPrecio);
                    dr["cantidad"] = Convert.ToInt32(txtCantidad.Text.ToString());
                    dr["subtotal"] = Convert.ToDecimal(Convert.ToDecimal(pPrecio) * Convert.ToDecimal(txtCantidad.Text.ToString()));
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
                agregar();
               // if (txtCodigoProducto.Text.Equals(""))
               // {
               //     Helper.MensajeSistema("Codigo no debe de estar vacio...");
               //     txtCodigoProducto.Focus();
               //     return;
               // }
               // if (txtCantidad.Text.Equals(""))
               // {
               //     Helper.MensajeSistema("Debe de ingresar una cantidad..");
               //     txtCantidad.Focus();
               //     return;
               // }
               // if (pidProducto.Equals("0"))
               // {
               //     Helper.MensajeSistema("Debe de ingresar una cantidad..");
               //     txtCodigoProducto.Focus();
               //     return;
               // }
               ///* if(Convert.ToDecimal(pStock) <= 0)
               // {
               //     Helper.MensajeSistema("No hay Stock de este producto, verificar...");
               //     txtCodigoProducto.Focus();
               //    //return;
               // }*/
               // if(Convert.ToInt32(txtCantidad.Text) < 0)
               // {
               //     Helper.MensajeSistema("Cantidad invalida, verificar...");
               //     txtCantidad.Focus();
               //     return;
               // }

               // if(Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Text))
               // {
               //     Helper.MensajeSistema("No hay Stock de este producto, verificar...");
               //     txtCodigoProducto.Focus();
               // }

               // //if (cmbUnidadMedida.SelectedValue.ToString().Equals("0"))
               // //{
               // //    Helper.MensajeSistema("Selecionar un tipo para guardar...");
               // //    cmbUnidadMedida.Focus();
               // //    return;
               // //}

               // /* if (Int32.Parse(pStock) < 0)
               //  {
               //      Helper.MensajeSistema("No hay stock para procesar el producto...");

               //      return;
               //  }*/
               // Boolean flag = false;
               // DataRow dr = detalle.NewRow();
               // //Decimal suma = 0;
               // if (detalle.Rows.Count > 0)
               // {
               //     foreach (DataRow row in detalle.Rows)
               //     {
               //         if (row["codigo"].ToString().Equals(pCodigo))// Si existe en el grid actualiza
               //         {
               //             row["cantidad"] = (Int32.Parse(row["cantidad"].ToString()) + Int32.Parse(txtCantidad.Text.ToString())).ToString();
               //             row["subtotal"] = Convert.ToDecimal(row["cantidad"].ToString()) * Convert.ToDecimal(pPrecio);
               //             flag = true;
               //             break;
               //         }
               //     }
               // }
               // if (!flag)
               // {
               //     dr["idProducto"] = pidProducto;
               //     dr["codigo"] = pCodigo;
               //     dr["descripcion"] = txtDescripcion.Text;
               //     dr["precio"] = Convert.ToDecimal(pPrecio);
               //     dr["cantidad"] = Convert.ToInt32(txtCantidad.Text.ToString());
               //     dr["subtotal"] = Convert.ToDecimal(Convert.ToDecimal(pPrecio) * Convert.ToDecimal(txtCantidad.Text.ToString()));
               //     detalle.Rows.Add(dr);   
               // }
               // //btnGuardar.Enabled = detalle.Rows.Count > 0 ? true : false;
               // limpiarFormulario();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UCVenta_Load(object sender, EventArgs e)
        {
            try
            {
                limpiar();

            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }


        private void limpiarFormulario()
        {
            try
            {
                pidProducto = "0";
                pCodigo = "0";
                pPrecio = "0";
                pStock = "0";
                // pDescripcion = "0";
                //idCliente = "0";

                //txtTicket.Clear();
                //txtCliente.Clear();

                txtCodigoProducto.Clear();
                txtDescripcion.Clear();
                txtPrecio.Text = "Q.";
                // txtStock.Clear();
                // txtCantidad.Clear();
                txtStock.Text = "0";
                txtCantidad.Text = "1";
                txtCantidad.Enabled = false;

                btnGuardar.Enabled = detalle.Rows.Count > 0 ? true : false;

                grdData.DataSource = detalle;
                txtContador.Text = "Registros(" + detalle.Rows.Count + ")";
                txtTotal.Text = "Total: "+Convert.ToDecimal(detalle.Compute("Sum(subtotal)","").ToString()).ToString("C2");
                txtCliente.BackColor = Color.White;
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
               // pDescripcion = "0";
                idCliente = "0";

                txtTicket.Clear();
                txtCliente.Clear();

                txtCodigoProducto.Clear();
                txtDescripcion.Clear();
                txtPrecio.Text = "Q.";
                //txtStock.Clear();
                //txtCantidad.Clear();

                txtStock.Text = "0";
                txtCantidad.Text = "1";
                txtCantidad.Enabled = false;

                btnGuardar.Enabled = false;
                if (detalle != null) detalle.Dispose();
                crearData();
                grdData.DataSource = detalle;
                txtContador.Text = "Productos(" + detalle.Rows.Count + ")";
                txtTotal.Text = "Total: Q.00.00";
                txtCliente.BackColor = Color.White;

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void crearData()
        {
            try
            {
                detalle = new DataTable();
                detalle.Columns.Add("idProducto");
                detalle.Columns.Add("codigo");
                detalle.Columns.Add("descripcion");
                detalle.Columns.Add("precio",typeof(Decimal));
                detalle.Columns.Add("cantidad",typeof(Int32));
                detalle.Columns.Add("subTotal",typeof(Decimal));

                //detalle.Columns.Add("unidadMedida");
                //detalle.Columns.Add("idUnidadMedida");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                /*string texto = ((TextBox)sender).Text.ToString();
                if (texto.Length > 0)
                {
                    //Helper.MensajeSistema("ultimo: " + texto[texto.Length]);
                    if (texto[texto.Length - 1] == 'H')
                    {
                        buscarProducto();
                    }
                }*/
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }

        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void TxtCodigoEmpleado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string texto = ((TextBox)sender).Text.ToString();
                if (texto.Length > 0)
                {
                    //Helper.MensajeSistema("ultimo: " + texto[texto.Length]);
                    if (texto[texto.Length - 1] == 'H')
                    {
                        buscarEmpleado();
                        //buscarProducto();
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }


        private void buscarEmpleado()
        {
            try
            {
                string texto = txtTicket.Text.ToString();
                if (texto.Length > 0)
                {
                    //Helper.MensajeSistema("ultimo: " + texto[texto.Length]);
                    if (texto[texto.Length - 1] == 'H')
                    {
                        //Helper.MensajeSistema("");
                        //texto = txtTicket.Text.Replace(" ", "");
                        string[] data = texto.Split(Globals.Separator);
                        if (data.Length < 2)
                        {

                            txtCliente.BackColor = Color.White;
                            txtTicket.Focus();
                            //txtCliente.Clear();
                            return;
                        }
                        //else
                        //{
                        if (!data[1].Equals(""))
                        {
                            /*if (cmbTipo.SelectedValue.ToString().Equals("0"))
                            {
                                Helper.MensajeSistema("Seleccione un tiempo de comida para continuar...");
                                txtTicket.Clear();
                                cmbTipo.Focus();
                                return;
                            }*/

                            if (!Helper.IsNumeric(data[0].ToString().Trim()))
                            {
                                Helper.MensajeSistema("El código es erroneo...");
                                txtCliente.Focus();
                                return;
                            }
                            //else
                            //{

                            DATicket ticket = new DATicket();
                            DataTable dt = ticket.cliente(
                                data[0].ToString().Trim(),
                                data[1].ToString().Trim().Replace("H", "")
                                );
                            if (dt.Rows.Count > 0)
                            {

                                idCliente = dt.Rows[0]["idCliente"].ToString();

                                //Helper.MensajeSistema(ticket.tiemposCliente(cmbTipo.SelectedValue.ToString(), idCliente, DateTime.Today.ToString("dd/MM/yyyy")).Rows[0]["tiempos"].ToString());
                                // ComprobarTiempoComida(ticket);

                                /*if (Convert.ToInt32(ticket.tiemposCliente(cmbTipo.SelectedValue.ToString(), idCliente, DateTime.Today.ToString("dd/MM/yyyy")).Rows[0]["tiempos"].ToString()) >= 1)
                                {

                                    Helper.MensajeSistema("Ya Cambio el ticket en este tiempo de comida...");
                                    txtTicket.Clear();
                                    idCliente = "0";
                                    return;
                                }*/
                                //else
                                //{

                                /*cmbTipo.Enabled = false;*/
                                txtCliente.Text = dt.Rows[0]["nombres"].ToString();
                                SystemSounds.Hand.Play();
                                txtCliente.BackColor = Color.DarkSeaGreen;
                                txtCodigoProducto.Focus();
                                //Helper.MensajeSistema("Texto: " + dt.Rows.Count);
                                //}
                            }
                            else
                            {
                               // cmbTipo.Enabled = true;
                                Helper.MensajeSistema("No encontro cliente...");
                                txtCliente.BackColor = Color.White;
                                idCliente = "0";
                                txtCliente.Clear();

                            }

                            //}
                            /*else
                            {
                                txtCliente.Clear();
                                txtCliente.BackColor = Color.White;
                            }*/





                            //   }

                        }

                    }
                    //else
                    //{
                    //    txtCliente.BackColor = Color.White;
                    //    txtCliente.Clear();
                    //}
                    // txtTicket.Text = texto;
                    //txtTicket.Focus();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count < 0)
                {
                    Helper.MensajeSistema("No hay datos a guardar...");
                    return;
                }
                DAProducto producto = new DAProducto();

                if (producto.guardarDetalleVenta(DateTime.Today.ToString("dd/MM/yyyy hh:mm:ss"),idCliente, detalle))
                {
                    Helper.MensajeSistema("Guardado Correctamente...");
                    limpiar();
                }
                else
                    Helper.MensajeSistema("No se ha guardado, contactar con soporte...");
            }
            catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
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
                        foreach (DataRow dr in detalle.Rows)
                            if (dr["idProducto"].Equals(id))
                            {
                                detalle.Rows.Remove(dr);
                                break;
                            }
                        detalle.AcceptChanges();
                        grdData.DataSource = detalle;
                        break;
                }
            }
            catch (Exception ex)
            {
                //idUsuario = "0";
                Helper.erroLog(ex);
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                agregar();
                //buscarProducto();
                //Helper.MensajeSistema("Código: " + txtCodigoProducto.Text);
            }
        }
    }
}
