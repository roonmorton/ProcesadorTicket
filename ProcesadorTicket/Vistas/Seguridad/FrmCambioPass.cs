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
    public partial class FrmCambioPass : Form
    {
        public FrmCambioPass()
        {
            InitializeComponent();
        }

        private void FrmCambioPass_Load(object sender, EventArgs e)
        {
            try
            {
                DAUsuario usuario = new DAUsuario();
                if (usuario.obtenerConfigurable().Rows[0]["configurable"].ToString().Equals("0"))
                {
                    Helper.MensajeSistema("No se puede configurar este usuario...");
                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarPass();
        }

        private void txtPss1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                actualizarPass();
            }
        }

        private void actualizarPass()
        {
            try
            {
                if (txtPss1.Text.Equals(txtPass2.Text))
                {
                    DASeguridad seg = new DASeguridad();
                    if (seg.cambioPass(txtPss1.Text, Globals.usuario))
                    {
                        Helper.MensajeSistema("Contrseña Cambiada correctamente...", "Exito");

                        this.Dispose();
                        this.Close();
                    }

                }
                else
                {
                    Helper.MensajeSistema("Contraseñas no coinciden, intentar de nuevo", "error");
                    txtPss1.Focus();
                }
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);

            }
        }
    }
}
