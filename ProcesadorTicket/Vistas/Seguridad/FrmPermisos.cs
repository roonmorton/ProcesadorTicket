using System;
using System.Data;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmPermisos : Form
    {
        private string idUsuario = "0";
        private string usuario = "";
        public FrmPermisos()
        {
            InitializeComponent();
        }

        private void buscarPermisos()
        {
            try
            {
                DAPermiso permisos = new DAPermiso();
                DataTable dtPermisos = permisos.obtenerPermisos(idUsuario);
                grdData.DataSource = dtPermisos;
                txtContador.Text = "Permisos(" +dtPermisos.Rows.Count+ ")";
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void FrmPermisos_Load(object sender, EventArgs e)
        {
            if (idUsuario.Equals("0"))
            {
                Helper.MensajeSistema("No hay usuario para asignar permisos...");
                this.Close();
            }
            else
            {
                buscarPermisos();
                lblUsuario.Text = "Usuario: " + usuario.ToUpper();
            }
        }

        public string getIdUsuario()
        {
            return idUsuario;
        }

        public  void setIdUsuario(string idUsuario)
        {
            this.idUsuario = idUsuario;
        }

        public void setUsuario(string usuario)
        {
            this.usuario = usuario;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(grdData.Rows.Count > 0)
                {
                    DAPermiso permiso = new DAPermiso();
                    if (permiso.guardar((DataTable)grdData.DataSource, idUsuario))
                    {
                        Helper.MensajeSistema("Guardado Correctamente...");
                    }else
                        Helper.MensajeSistema("Ocurrio un error, no se ha guardado...");
                }
                else
                {
                    Helper.MensajeSistema("No hay datos a cargar, contactar soporte...");
                }
                
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
