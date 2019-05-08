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
    public partial class FrmLogin : Form
    {
        private int count = 5;
        private Boolean success = false;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToScreen();
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            login();
        }

        protected void login(){
            DataTable tblInformacionLogin = new DataTable();
            try
            {
                DASeguridad seg = new DASeguridad();
                tblInformacionLogin = seg.login(txtUsuario.Text.Trim(), txtPass.Text.Trim());
                if (tblInformacionLogin.Rows.Count > 0)
                {
                    Globals.idUSuario = Convert.ToInt32(tblInformacionLogin.Rows[0]["idUsuario"].ToString());
                    Globals.usuario = tblInformacionLogin.Rows[0]["usuario"].ToString();
                    success = true;
                    this.Close();
                }
                else
                {
                    count -= 1;
                    Helper.MensajeSistema("Usuario incorrecto, " + count.ToString() + " intentos restantes");
                    limpiar();
                    txtUsuario.Focus();
                    if (count == 0) { Application.Exit(); }
                }
            }
            catch (Exception ex)
            {
                /*
                if (ex.Message.ToString().Contains("[CC]"))
                {
                    ClsHelper.MensajeSistema(ex.Message);
                    FrmCambiarContrasena frm = new FrmCambiarContrasena();
                    frm.usuario = txtUsuario.Text;
                    frm.ShowDialog();
                }
                else
                {
                    count -= 1;
                    ClsHelper.MensajeSistema(ex.Message + " " + count.ToString() + " intentos restantes");
                    if (count == 0) { Application.Exit(); }
                }*/
                count -= 1;
                Helper.MensajeSistema(ex.Message + " " + count.ToString() + " intentos restantes");
                if (count == 0) { Application.Exit(); }


            }
            }
        protected void salir()
        {
            try
            {
                if (!success)
                {
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            salir();
        }

        public Boolean autenticar()
        {
            Boolean res = false;
            try
            {
                this.ShowDialog();
                if (Globals.idUSuario > 0 && !String.IsNullOrEmpty(Globals.usuario))
                {
                    res = true;
                }
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void key_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void limpiar()
        {
            try
            {
                txtPass.Clear();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
