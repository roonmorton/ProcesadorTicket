using ClosedXML.Excel;
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
    public partial class FrmReports : Form
    {
        public FrmReports()
        {
            InitializeComponent();
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            try
            {
                cargar();
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                cargar();
            }catch(Exception ex){
                Helper.erroLog(ex);
            }
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
                    row["monto"] = Math.Round(double.Parse(row["monto"].ToString()),2);
                    i++;
                }
                grdHistorico.DataSource = dt;
                lblStatus.Text = "Registros(" + dt.Rows.Count.ToString() +")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdHistorico.Rows.Count < 1)
                {
                    Helper.MensajeSistema("No hay datos para exportar");
                    return;
                }


                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Reporte_"+DateTime.Today.ToString("dd_MM_yyyy_hh_mm");
                sfd.Filter = "files (*.xlsx)|*.xlsx";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(sfd.FileName))
                    {
                        //Exporting to Excel
                        string fileName = sfd.FileName;
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            DataTable dt = (DataTable)grdHistorico.DataSource;
                            wb.Worksheets.Add(dt, "Ticket");
                           
                            wb.SaveAs(fileName);
                            if (MessageBox.Show("Archivo generado correctamente,¿Desea abrirlo?", "Abrir Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(fileName);
                            }
                        }

                    }
                }




            }
            catch (Exception ex)
            {

                Helper.erroLog(ex);
            }
        }
    }
}
