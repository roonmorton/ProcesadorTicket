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
    public partial class UCReports : UserControl
    {
      
        public UCReports()
        {
            InitializeComponent();
        }

        private void UCReports_Load(object sender, EventArgs e)
        {
            try
            {
                cargar();
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void cargar()
        {
            try
            {
                DAReporte rpt = new DAReporte();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Nombres");
                dt.Columns.Add("Monto", System.Type.GetType("System.Decimal"));
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Fecha");


                dt = rpt.TicketFecha(dtFechaInicio.Text.Trim(), dtFechaFin.Text.Trim());

                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    row["ID"] = i.ToString();
                    row["monto"] = Math.Round(double.Parse(row["monto"].ToString()), 2);
                    i++;
                }
                grdHistorico.DataSource = dt;
                txtContador.Text = "Registros(" + dt.Rows.Count.ToString() + ")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
