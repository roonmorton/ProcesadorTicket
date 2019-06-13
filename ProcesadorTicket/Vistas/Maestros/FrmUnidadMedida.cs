using ProcesadorTicket.Core.DA;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmUnidadMedida : Form
    {
        string idUnidadMedida = "0";

        public FrmUnidadMedida()
        {
            InitializeComponent();
        }

        private void FrmUnidadMedida_Load(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                //DataTable dt = new DataTable();
                //dt.Columns.Add("ID");
                //dt.Columns.Add("descripcion");

                //dt.Rows.Add("1", "Caja");
                //dt.Rows.Add("2", "Paquete");

                //grdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void buscar()
        {
            try
            {
                DAUnidadMedida tipoMedida = new DAUnidadMedida();
                DataTable dt = tipoMedida.buscar(txtDescripcion.Text.ToString());
                txtContador.Text = "Registros: ("+dt.Rows.Count+")";
                grdData.DataSource = dt;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        private void limpiar()
        {
            try
            {
                txtDescripcion.Clear();
                buscar();
                idUnidadMedida = "0";
            }catch(Exception ex)
            {
                throw ex;
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
            }catch(Exception ex)
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
                    Helper.MensajeSistema("Ingresar una descripción...");
                    txtDescripcion.Focus();
                    return;
                }
                DAUnidadMedida UnidadMedida = new DAUnidadMedida();
                UnidadMedida.insertaActualizar(idUnidadMedida,txtDescripcion.Text.ToString());
                limpiar();
                Helper.MensajeSistema("Guardado Correctamente...");

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
