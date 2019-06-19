﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProcesadorTicket.Core.DA;
using System.Media;
using System.IO;

namespace ProcesadorTicket
{
    public partial class UCClientes : UserControl
    {

        private String idCliente = "0";

        public UCClientes()
        {
            InitializeComponent();
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

        private void limpiar()
        {
            try
            {
                txtNombre.Clear();
                txtCodigoEmpleado.Clear();
                txtCodigoEmpleado.Enabled = true;
                idCliente = "0";
                buscar();
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buscar()
        {
            try
            {
                DACliente cliente = new DACliente();
                DataTable dt = cliente.buscar(txtNombre.Text.ToString().Trim());
                grdHistorico.DataSource = dt;
                txtContador.Text = "Registros (" + dt.Rows.Count + ")";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void guardar()
        {
            try
            {
                if (txtNombre.Text.ToString().Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar un nombre para el nuevo Cliente");
                    txtNombre.Focus();
                    return;
                }
                if (txtCodigoEmpleado.Text.ToString().Equals(""))
                {
                    Helper.MensajeSistema("Debe de ingresar un codigo de Empleado");
                    txtCodigoEmpleado.Focus();
                    return;
                }
                DACliente cliente = new DACliente();
                cliente.insertaActualizar(idCliente, txtNombre.Text.ToString().Trim(), txtCodigoEmpleado.Text.ToString().Trim());
                limpiar();
                Helper.MensajeSistema("Se guardo correctamente...");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
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

        private void grdHistorico_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        idCliente = grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString();
                        txtNombre.Text = grdHistorico.SelectedRows[0].Cells["nombres"].Value.ToString();
                        // cmbTipo.SelectedValue = grdHistorico.SelectedRows[0].Cells["idTipo"].Value;
                        txtCodigoEmpleado.Text = grdHistorico.SelectedRows[0].Cells["codigoEmpleado"].Value.ToString();
                        txtCodigoEmpleado.Enabled = false;
                        //txtMonto.Text = grdHistorico.SelectedRows[0].Cells["monto"].Value.ToString();
                        //txtNombre.Enabled = true;
                        //txtTicket.Focus();
                        break;
                    case 1:
                        DialogResult r = MessageBox.Show("¿Confirma que desea eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            DACliente cliente = new DACliente();
                            cliente.eliminar(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            /*clsLinea.eliminar(grdExistentes.SelectedRows[0].Cells["idLineaCol"].Value.ToString());
                            ClsHelper.MensajeSistema("Proceso ejecutado exitosamente");
                            limpiarControles();*/
                            Helper.MensajeSistema("Registro Eliminado");
                            limpiar();

                        }
                        break;
                    case 2:

                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.Filter = "Archivos jpg (*.jpg)| *.jpg";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string cadena = grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString() + Globals.Separator + grdHistorico.SelectedRows[0].Cells["codigoEmpleado"].Value.ToString() + "H";
                            Bitmap btm = Helper.GenerarCodigoBarras(cadena, 325, 150);
                            Image img = btm;
                            img.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            FileInfo img__ = new FileInfo(dialog.FileName);
                            //img__.MoveTo(dialog.FileName );
                            Helper.MensajeSistema("Guardado...");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                idCliente = "0";
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

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void UCClientes_Load(object sender, EventArgs e)
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

    }
}
