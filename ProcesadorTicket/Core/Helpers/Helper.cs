using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXing;

namespace ProcesadorTicket
{
    public static class Helper
    {
        public static void erroLog(Exception Err)
        {
            string usuario;
            try
            {
                usuario = Globals.usuario;
                using (StreamWriter log = new StreamWriter("Error.log", true))
                {
                    log.WriteLine("   " + DateTime.Now.ToString() + "   " + usuario + "   " + Environment.MachineName + "   " + Err.Message + "   " + Err.TargetSite.Name + "   " + Err.StackTrace);
                    log.WriteLine();
                }

                throw new Exception("Error en la aplicación, comunicarse a Soporte" + Environment.NewLine + Err.Message + Environment.NewLine + "Metodo " + Err.TargetSite.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void MensajeSistema(string mensaje, string titulo = "Mensaje del Sistema")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static Boolean tieneDato(System.Windows.Forms.MaskedTextBox field)
        {
            Boolean res = true;
            try
            {
                if (string.IsNullOrEmpty(field.Text.Replace(field.Culture.DateTimeFormat.DateSeparator, "").Replace(field.Culture.DateTimeFormat.TimeSeparator, "").Trim()))
                {
                    res = false;
                }
            }
            catch (Exception)
            { throw; }
            return res;
        }

        public static Boolean tieneDato(System.Windows.Forms.TextBox field)
        {
            Boolean res = true;
            try
            {
                if (string.IsNullOrEmpty(field.Text.Trim()))
                {
                    res = false;
                }
            }
            catch (Exception)
            { throw; }
            return res;
        }

        public static Boolean tieneDato(System.Windows.Forms.ComboBox field)
        {
            Boolean res = true;
            try
            {
                if (string.IsNullOrEmpty(field.Text.Trim())) res = false;
                if (string.IsNullOrEmpty(field.SelectedValue.ToString())) res = false;
                if (string.IsNullOrEmpty(field.SelectedValue.ToString())) res = false;
            }
            catch (Exception)
            { throw; }
            return res;
        }

        public static Boolean camposObligatorios(params Control[] controles)
        {
            Boolean res = true;
            try
            {
                foreach (Control vControl in controles)
                {

                    if (vControl.GetType().Name.ToUpper() == "TextBox".ToUpper())
                    {
                        if (!tieneDato((System.Windows.Forms.TextBox)vControl))
                        {
                            MensajeSistema("El campo " + vControl.Name.ToUpper().Replace("TXT", "") + " es requerido para el proceso");
                            vControl.Focus();
                            res = false;
                            break;
                        }
                    }
                    else if (vControl.GetType().Name.ToUpper() == "ComboBox".ToUpper())
                    {
                        if (!tieneDato((System.Windows.Forms.ComboBox)vControl))
                        {
                            MensajeSistema("El campo " + vControl.Name.ToUpper().Replace("CBO", "") + " es requerido para el proceso");
                            vControl.Focus();
                            res = false;
                            break;
                        }
                    }
                    else if (vControl.GetType().Name.ToUpper() == "MaskedTextBox".ToUpper())
                    {
                        if (!tieneDato((System.Windows.Forms.MaskedTextBox)vControl))
                        {
                            MensajeSistema("El campo " + vControl.Name.ToUpper().Replace("TXT", "") + " es requerido para el proceso");
                            vControl.Focus();
                            res = false;
                            break;
                        }
                    }

                }//foreach
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public static void exportarExcel(string ruta)
        {
            try
            {

            }
            catch (Exception) { throw; }
        }

        public static bool isDate(string date)
        {
            DateTime Temp;
            return (DateTime.TryParse(date, out Temp) && date.Length >= 10);
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }


        public static Bitmap GenerarCodigoBarras(string cadena,int largo=400, int alto = 250 )
        {
            try{
                BarcodeWriter barra = new BarcodeWriter();
                barra.Format = BarcodeFormat.CODE_128;
                return new Bitmap(barra.Write(cadena),largo,alto);
            }catch(Exception ex)
            {
                throw ex;
            }
        }


    }

}
