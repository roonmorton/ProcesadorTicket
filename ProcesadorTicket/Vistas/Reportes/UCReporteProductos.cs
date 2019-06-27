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
using ClosedXML.Excel;

namespace ProcesadorTicket
{
    public partial class UCReporteProductos : UserControl
    {
      
        public UCReporteProductos()
        {
            InitializeComponent();
        }


        private void cargar()
        {
            try
            {
                DAReporte rpt = new DAReporte();
                DataTable dt = new DataTable();
                /*dt.Columns.Add("ID");
                dt.Columns.Add("Nombres");
                dt.Columns.Add("Monto", System.Type.GetType("System.Decimal"));
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Fecha");*/
                //Helper.MensajeSistema("Indice: " + cmbTipo.SelectedIndex.ToString());
                switch (cmbTipo.SelectedIndex.ToString())
                {
                    case "0":
                        dt = rpt.EntradaFecha(dtFechaInicio.Text.Trim(), dtFechaFin.Text.Trim());

                        break;
                    case "1":
                        dt = rpt.EntradaDetalleFecha(dtFechaInicio.Text.Trim(), dtFechaFin.Text.Trim());

                        break;
                    case "2":
                        dt = rpt.productosMasVendidos();

                        break;
                    case "3":
                        dt = rpt.productos();
                        break;
                    /*case "3":

                        break;*/


                }
               
                /* int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    row["ID"] = i.ToString();
                    row["monto"] = Math.Round(double.Parse(row["monto"].ToString()), 2);
                    i++;
                }*/
                grdReporte.DataSource = dt;
                txtContador.Text = "Registros(" + dt.Rows.Count.ToString() + ")";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
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

        private void FrmReporteVentas_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipo.Items.Insert(0, "Entradas");
                cmbTipo.Items.Insert(1, "Entradas Detalle");
                cmbTipo.Items.Insert(2, "Productos mas vendidos");
               // cmbTipo.Items.Insert(3, "Producto mas vendido");

                cmbTipo.SelectedIndex = 0;
                cargar();
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdReporte.Rows.Count < 1)
                {
                    Helper.MensajeSistema("No hay datos para exportar");
                    return;
                }


                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Reporte_" + DateTime.Today.ToString("dd_MM_yyyy_hh_mm");
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
                            DataTable dt = (DataTable)grdReporte.DataSource;
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
