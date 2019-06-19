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
    public partial class UCUsuario : UserControl
    {

        private string idUsuario = "0";

        public UCUsuario()
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
                txtUsuario.Enabled = true;
                checkEstado.Checked = false;
            }
            catch (Exception ex)
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
                txtUsuario.Enabled = true;
                buscar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UCUsuario_Load(object sender, EventArgs e)
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

        private void BtnLimpiar_Click(object sender, EventArgs e)
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

        private void BtnBuscar_Click(object sender, EventArgs e)
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
                string vUsuaro = usuario.validarUsuario(txtUsuario.Text.ToString()).Rows[0]["usuarios"].ToString();

                //Si hay usuario y es nuevo, Se detiene 
                if (!vUsuaro.Equals("0") && idUsuario.Equals("0"))
                {
                    Helper.MensajeSistema("Usuario ya existe, ingresar uno diferente...");
                    txtUsuario.Focus();
                    return;
                }
                //Si hay usuario y no es el a modificar se detiene 
                /* if (!usuario.validarUsuarioID(txtUsuario.Text.ToString(),idUsuario).Rows[0]["usuarios"].ToString().Equals("1"))
                 {
                     Helper.MensajeSistema("Usuario no se puede modificar ya existe, ingresar uno diferente...");
                     txtUsuario.Focus();
                     return;
                 }*/
                usuario.insertaActualizar(idUsuario, txtNombres.Text.ToString().Trim(), txtApellidos.Text.ToString().Trim(), txtUsuario.Text.ToString().Trim(), (checkEstado.Checked == true ? "1" : "0").ToString());
                Helper.MensajeSistema("Guardado Correctamente...");
                limpiar();


            }
            catch (Exception ex)
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
                        txtUsuario.Enabled = false;
                        break;
                    case 1:
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
