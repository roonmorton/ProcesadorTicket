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
    public partial class FrmUsuario : Form
    {
        private string idUsuario = "0";

        public FrmUsuario()
        {
            InitializeComponent();
        }

    
        private void buscar()
        {
            try
            {
                DAUsuario usuario = new DAUsuario();
                DataTable dt = usuario.buscar(txtNombres.Text.ToString().Trim());
                txtContador.Text = "Registros(" + dt.Rows.Count + ")";
                grdHistorico.DataSource = dt;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    

  

        private void key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void limpiar()
        {
            try
            {
                txtNombres.Clear();
                txtApellidos.Clear();
                txtUsuario.Clear();
                checkEstado.Checked = false;
                idUsuario = "0";
                txtNombres.Focus();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                buscar();
            }catch(Exception ex)
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
                Helper.erroLog(ex);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                buscar();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombres.Text.ToString().Equals(""))
                {
                    Helper.MensajeSistema("Debe ingresar un nombre al Usuario");
                    txtNombres.Focus();
                    return;
                }
                if (txtUsuario.Text.ToString().Equals(""))
                {
                    Helper.MensajeSistema("Debe ingresar un nombre de usuario");
                    txtUsuario.Focus();
                    return;
                }
                DAUsuario usuario = new DAUsuario();
                usuario.insertaActualizar(idUsuario, txtNombres.Text.ToString().Trim(), txtApellidos.Text.ToString().Trim(), txtUsuario.Text.ToString().Trim(), (checkEstado.Checked == true ? "1" : "0").ToString());
                Helper.MensajeSistema("Guardado Correctamente...");
                limpiar();
            }
            catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void GrdHistorico_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        idUsuario = grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString();
                        txtNombres.Text = grdHistorico.SelectedRows[0].Cells["nombres"].Value.ToString();
                        txtApellidos.Text = grdHistorico.SelectedRows[0].Cells["apellidos"].Value.ToString();
                        txtUsuario.Text = grdHistorico.SelectedRows[0].Cells["usuario"].Value.ToString();
                        checkEstado.Checked = (grdHistorico.SelectedRows[0].Cells["estadoUsuario"].Value.ToString() == "Activo" ? true : false);

                        break;
                    case 1:
                        DialogResult r = MessageBox.Show("¿Confirma que desea deshabilitar usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (r == DialogResult.Yes)
                        {
                            // DACliente cliente = new DACliente();
                            //cliente.eliminar(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            /*clsLinea.eliminar(grdExistentes.SelectedRows[0].Cells["idLineaCol"].Value.ToString());
                            ClsHelper.MensajeSistema("Proceso ejecutado exitosamente");
                            limpiarControles();*/
                            //DAUsuario usuario = new DAUsuario();
                            //usuario.eliminar(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            //Helper.MensajeSistema("Usuario Deshabilitado...");
                            //limpiar();
                        }
                        break;
                    case 2:

                        DialogResult dr = MessageBox.Show("¿Reestablecer contraseña de usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            DAUsuario usuario = new DAUsuario();
                            usuario.resetPass(grdHistorico.SelectedRows[0].Cells["ID"].Value.ToString());
                            Helper.MensajeSistema("Se ha reestablecido la contraseña...");
                        }

                            break;
                }
            }
            catch (Exception ex)
            {
                idUsuario = "0";
                Helper.erroLog(ex);
            }
        }
    }
}
