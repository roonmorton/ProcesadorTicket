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
    public partial class UCTicket : UserControl
    {
        private string idTicket = "0";
        private string idCliente = "0";
        public UCTicket()
        {
            InitializeComponent();
        }


        private void limpiar()
        {
            try
            {
                txtTicket.Clear();
                txtCliente.Clear();
                //cmbTipo.Focus();
                cargar();
                idTicket = "0";
                idCliente = "0";
                txtMonto.Text = "0";
                txtTicket.Focus();
                txtCliente.BackColor = Color.White;
                cmbTipo.Enabled = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarComboTipo()
        {
            try
            {
                DATipo tipo = new DATipo();
                cargarCombos(tipo.seleccionar(), this.cmbTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarCombos(DataTable dataTable, ComboBox combo)
        {
            Dictionary<int, String> dicTipo = new Dictionary<int, string>();
            dicTipo.Add(0, "Seleccione unida de Medida...");
            foreach (DataRow row in dataTable.Rows)
            {
                dicTipo.Add(Int32.Parse(row[0].ToString()), row[1].ToString());
            }
            combo.DisplayMember = "Value";
            combo.ValueMember = "Key";
            combo.DataSource = dicTipo.ToArray();
        }

        private void buscar()
        {
            try
            {
                string texto = txtTicket.Text;
                if (!texto.Equals(""))
                {
                    texto = texto.Replace(" ", "");
                    string[] data = texto.Split(Globals.Separator);
                    if (data.Length < 2)
                    {
                        Helper.MensajeSistema("Ingrese numero Ticket...");
                        txtTicket.Focus();
                        //txtCliente.BackColor = Color.White;
                        //txtCliente.Clear();
                        return;
                    }

                    if (!Helper.IsNumeric(data[0].ToString().Trim()))
                    {
                        Helper.MensajeSistema("EL numero Ticket no es valido...");
                        txtMonto.Focus();
                        return;
                    }

                    DATicket ticket = new DATicket();
                    DataTable dt = ticket.buscar(data[0]);
                    grdHistorico.DataSource = dt;
                    txtContador.Text = "Registros (" + dt.Rows.Count + ")";

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargar()
        {
            try
            {
                DATicket ticket = new DATicket();
                DataTable dt = ticket.seleccionDia(DateTime.Today.ToString("dd/MM/yyyy"));
                grdHistorico.DataSource = dt;
                txtContador.Text = "Registros (" + dt.Rows.Count + ")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
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
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void guardar()
        {
            try
            {
                if (txtTicket.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar un numero Ticket");
                    txtTicket.Focus();
                    return;
                }
                if (txtMonto.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar un monto");
                    txtMonto.Focus();
                    return;
                }
                if (!Helper.IsNumeric(txtMonto.Text))
                {
                    Helper.MensajeSistema("Monto debe de se un numero");
                    txtMonto.Focus();
                    return;
                }
                //Helper.MensajeSistema(cmbTipo.SelectedValue.ToString());
                if (cmbTipo.SelectedValue.ToString().Equals("0"))
                {
                    Helper.MensajeSistema("Selecionar un tipo para guardar...");
                    cmbTipo.Focus();
                    return;
                }
                if (idCliente.Equals("0"))
                {
                    Helper.MensajeSistema("No se ha registrado ticket...");
                    txtTicket.Clear();
                    txtTicket.Focus();
                    return;
                }

                DATicket ticket = new DATicket();
                // ComprobarTiempoComida(ticket);
                ticket.insertaActualizar(idTicket, idCliente, txtMonto.Text.Trim(), cmbTipo.SelectedValue.ToString());
                limpiar();
                Helper.MensajeSistema("Guardado Correctamente...");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtTicket_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    guardar();
                }
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                buscar();
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void grdHistorico_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        idTicket = grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString();
                        txtCliente.Text = grdHistorico.SelectedRows[0].Cells["nombres"].Value.ToString();
                        cmbTipo.SelectedValue = grdHistorico.SelectedRows[0].Cells["idTipo"].Value;
                        idCliente = grdHistorico.SelectedRows[0].Cells["Cliente"].Value.ToString();
                        txtMonto.Text = grdHistorico.SelectedRows[0].Cells["monto"].Value.ToString();
                        //txtNombre.Enabled = true;
                        txtTicket.Focus();
                        break;
                    case 1:
                        DialogResult r = MessageBox.Show("¿Confirma que desea eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            DATicket ticket = new DATicket();
                            ticket.eliminar(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            /*clsLinea.eliminar(grdExistentes.SelectedRows[0].Cells["idLineaCol"].Value.ToString());
                            ClsHelper.MensajeSistema("Proceso ejecutado exitosamente");
                            limpiarControles();*/
                            Helper.MensajeSistema("Registro Eliminado");
                            limpiar();

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                idTicket = "0";
                Helper.erroLog(ex);
            }
        }

        private void txtTicket_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string texto = ((TextBox)sender).Text.ToString();
                if (texto.Length > 0)
                {
                    //Helper.MensajeSistema("ultimo: " + texto[texto.Length]);
                    if (texto[texto.Length - 1] == 'H')
                    {
                        Helper.MensajeSistema("");
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
                            if (cmbTipo.SelectedValue.ToString().Equals("0"))
                            {
                                Helper.MensajeSistema("Seleccione un tiempo de comida para continuar...");
                                txtTicket.Clear();
                                cmbTipo.Focus();
                                return;
                            }

                            if (!Helper.IsNumeric(data[0].ToString().Trim()))
                            {
                                Helper.MensajeSistema("El código es erroneo...");
                                txtCliente.Focus();
                                return;
                            }
                            //else
                            //{

                            DATicket ticket = new DATicket();
                            DataTable dt = ticket.ticket(
                                data[0].ToString().Trim(),
                                data[1].ToString().Trim().Replace("H", "")
                                );
                            if (dt.Rows.Count > 0)
                            {

                                idCliente = dt.Rows[0]["idCliente"].ToString();

                                //Helper.MensajeSistema(ticket.tiemposCliente(cmbTipo.SelectedValue.ToString(), idCliente, DateTime.Today.ToString("dd/MM/yyyy")).Rows[0]["tiempos"].ToString());
                                // ComprobarTiempoComida(ticket);

                                if (Convert.ToInt32(ticket.tiemposCliente(cmbTipo.SelectedValue.ToString(), idCliente, DateTime.Today.ToString("dd/MM/yyyy")).Rows[0]["tiempos"].ToString()) >= 1)
                                {

                                    Helper.MensajeSistema("Ya Cambio el ticket en este tiempo de comida...");
                                    txtTicket.Clear();
                                    idCliente = "0";
                                    return;
                                }
                                //else
                                //{

                                cmbTipo.Enabled = false;
                                txtCliente.Text = dt.Rows[0]["nombres"].ToString();
                                SystemSounds.Hand.Play();
                                txtCliente.BackColor = Color.DarkSeaGreen;
                                //Helper.MensajeSistema("Texto: " + dt.Rows.Count);
                                //}
                            }
                            else
                            {
                                cmbTipo.Enabled = true;
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
                Helper.erroLog(ex);
            }
        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Helper.MensajeSistema("keyPRess");
        }

        private void txtTicket_KeyUp(object sender, KeyEventArgs e)
        {
            //Helper.MensajeSistema("keyup");

        }

        private void txtTicket_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Helper.MensajeSistema("keydown");

        }

        private void txtTicket_TabStopChanged(object sender, EventArgs e)
        {
            Helper.MensajeSistema("keystop");


        }

        private void UCTickets_Load(object sender, EventArgs e)
        {
            try
            {
                cargarComboTipo();
                limpiar();
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
