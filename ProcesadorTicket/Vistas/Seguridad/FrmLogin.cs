using ProcesadorTicket.Core.DA;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmLogin : Form
    {
        private int count = 5;
        private Boolean success = false;
        private Boolean validacion = false;
        private string strPermiso = "";
        public Boolean getvalidacion()
        {
            return validacion;
        }

        public void setPermiso( string permiso)
        {
            this.strPermiso = permiso;
        }

       
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
            try
            {

                login();
                if (!this.strPermiso.Equals(""))
                {
                    validarUsuarioPermiso();

                }

            }
            catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }


        protected void validarUsuarioPermiso()
        {
            try
            {
                if (success)
                {

                    DASeguridad seg = new DASeguridad();
                    DataTable dtSeg = seg.validarPermisoUsuario(this.txtUsuario.Text.ToString(), this.strPermiso);

                    if (dtSeg.Rows.Count > 0)
                    {
                        this.validacion = true;
                        Helper.MensajeSistema("Usuario Valido");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                    if(this.strPermiso.Equals(""))
                        this.Close();
                }
                else
                {
                    count -= 1;
                    Helper.MensajeSistema("Usuario incorrecto, " + count.ToString() + " intentos restantes");
                    limpiar();
                    txtUsuario.Focus();
                    if (count == 0) {
                        if (this.strPermiso.Equals(""))
                            Application.Exit();
                        else
                            this.Close();        
                    }
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
                //count -= 1;
                Helper.erroLog(ex);
                //Helper.MensajeSistema(ex.Message + " " + count.ToString() + " intentos restantes");
                //if (count == 0) { Application.Exit(); }


            }
            }
        protected void salir()
        {
            try
            {
                //if (!this.strPermiso.Equals(""))
                //    this.Close();
                if (!success && this.strPermiso.Equals(""))
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
            try
            {
                salir();
                if (!this.strPermiso.Equals(""))
                    this.Close();

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
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
                if (!this.strPermiso.Equals(""))
                {
                    validarUsuarioPermiso();

                }
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
