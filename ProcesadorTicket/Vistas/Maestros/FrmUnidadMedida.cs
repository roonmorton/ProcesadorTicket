using System;
using System.Data;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmUnidadMedida : Form
    {
        public FrmUnidadMedida()
        {
            InitializeComponent();
        }

        private void FrmUnidadMedida_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("descripcion");

                dt.Rows.Add("1", "Caja");
                dt.Rows.Add("2", "Paquete");

                grdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
