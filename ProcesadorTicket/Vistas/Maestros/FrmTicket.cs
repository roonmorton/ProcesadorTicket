using ProcesadorTicket.Core.DA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmTicket : Form
    {
        private string idTicket = "0";
        private string idCliente = "0";
        public FrmTicket()
        {
            InitializeComponent();
        }

        private void FrmTicket_Load(object sender, EventArgs e)
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
            dicTipo.Add(0, "Seleccione un Tipo...");
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
                    txtTicket.Focus();
                    return;
                }

                DATicket ticket = new DATicket();
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

        private void btnSerach_Click(object sender, EventArgs e)
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
                if (!texto.Equals(""))
                {
                    texto = texto.Replace(" ", "");
                    string[] data = texto.Split(Globals.Separator);
                    if (data.Length < 2)
                    {

                        txtCliente.BackColor = Color.White;
                        txtCliente.Clear();
                        return;
                    }

                    if (!Helper.IsNumeric(data[0].ToString().Trim()))
                    {
                        Helper.MensajeSistema("El código es erroneo...");
                        txtMonto.Focus();
                        return;
                    }
                    else
                    {
                        txtCliente.Clear();

                        txtCliente.BackColor = Color.White;
                    }
                    DATicket ticket = new DATicket();
                    DataTable dt = ticket.ticket(
                        data[0].ToString().Trim(),
                        data[1].ToString().Trim()
                        );
                    if (dt.Rows.Count > 0)
                    {
                        txtCliente.Text = dt.Rows[0]["nombres"].ToString();
                        idCliente = dt.Rows[0]["idCliente"].ToString();
                        SystemSounds.Hand.Play();
                        txtCliente.BackColor = Color.DarkSeaGreen;
                        //Helper.MensajeSistema("Texto: " + dt.Rows.Count);
                    }
                    else
                    {
                        txtCliente.BackColor = Color.White;
                        txtCliente.Clear();

                    }

                }
                else
                {
                    txtCliente.BackColor = Color.White;
                    txtCliente.Clear();
                }
                txtTicket.Text = texto;
                txtTicket.Focus();

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
