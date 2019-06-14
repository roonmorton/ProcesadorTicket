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
    public partial class FrmProducto : Form
    {
        private string idProducto = "0";
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
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
                txtCodigo.Clear();
                txtDescripcion.Clear();
                txtPrecio.Clear();
                btnPrecio.Enabled = false;
                txtPrecio.Enabled = true;
                buscar();
                idProducto = "0";
                txtDescripcion.Focus();
                
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void buscar()
        {
            try {
                DAProducto producto = new DAProducto();
                DataTable dt = producto.buscar(txtDescripcion.Text.ToString());
                txtContador.Text = "Registros(" + dt.Rows.Count + ")";
                grdData.DataSource = dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void BtnPrecio_Click(object sender, EventArgs e)
        {
            try
            {
                string permiso = "Precios";
                DASeguridad seg = new DASeguridad();
                DataTable dtSeg = seg.validarPermisoUsuario(Globals.usuario, permiso);

                if (dtSeg.Rows.Count == 0)
                {
                    
                    FrmLogin login = new FrmLogin();
                    login.setPermiso(permiso);
                    login.ShowDialog(this);
                    if (login.getvalidacion())
                    {
                        Helper.MensajeSistema("Usuario Validado correctamente...");
                        txtPrecio.Enabled = true;
                        btnPrecio.Enabled = false;
                        txtPrecio.Focus();

                    }
                    else
                        Helper.MensajeSistema("No se ha podido validar el usuario...");
                    login.Dispose();
                }
                else
                {
                    txtPrecio.Enabled = true;
                    btnPrecio.Enabled = false;
                    txtPrecio.Focus();
                }


            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
            }catch(Exception ex)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcion.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe ingresar una descripción...");
                    return;
                }

                if (txtCodigo.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe ingresar un código...");
                    return;
                }
                if (txtPrecio.Text.Equals(""))
                {
                    Helper.MensajeSistema("Debe ingresar un precio...");
                    return;
                }

                if (!Helper.IsNumeric(txtPrecio.Text.ToString().Trim()))
                {
                    Helper.MensajeSistema("El precio debe de ser un numero...");
                    return;
                }
                DAProducto producto = new DAProducto();
                producto.AgregarActualizar(idProducto, txtCodigo.Text.Trim(), txtDescripcion.Text.Trim(), txtPrecio.Text.Trim());
                Helper.MensajeSistema("Producto agregado");
                limpiar();
                btnPrecio.Enabled = false;
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        idProducto = grdData.SelectedRows[0].Cells["ID"].Value.ToString();
                        txtDescripcion.Text = grdData.SelectedRows[0].Cells["descripcion"].Value.ToString();
                        // cmbTipo.SelectedValue = grdHistorico.SelectedRows[0].Cells["idTipo"].Value;
                        txtPrecio.Text = grdData.SelectedRows[0].Cells["precio"].Value.ToString();
                        txtCodigo.Text = grdData.SelectedRows[0].Cells["codigo"].Value.ToString();
                        txtPrecio.Enabled = false;
                        txtDescripcion.Focus();
                        btnPrecio.Enabled = true;
                        break;
                   /* case 1:
                        DialogResult r = MessageBox.Show("¿Confirma que desea eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            DACliente cliente = new DACliente();
                            cliente.eliminar(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                        
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
                        break;*/
                }
            }
            catch (Exception ex)
            {
                idProducto = "0";
                Helper.erroLog(ex);
            }
        }
    }
}
