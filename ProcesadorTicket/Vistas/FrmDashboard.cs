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
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                autenticacion();
                //Hilo que muestra la alerta
                //Thread alertaThread = new Thread(new ThreadStart(this.mostrarAlerta));
                //alertaThread.Start();
                
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void autenticacion()
        {
            try
            {
                Boolean success = false;
                FrmLogin FrmLogin = new FrmLogin();
                success = FrmLogin.autenticar();
                if (success && !String.IsNullOrEmpty(Globals.usuario) && Globals.idUSuario > 0)
                {
                    //lblUsuario.Text = "Usuario: " + ClsGlobals.usuario + "  ";
                    //lblComputadora.Text = "Computadora: " + Environment.MachineName;
                    toolStripStatusLabel1.Text = "Usuario: " + Globals.usuario + "  ";
                    lblComputadora.Text = "Computadora: " + Environment.MachineName;
                    //mostrarMenu();

                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                Application.Exit();

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCambioPass frmPass = new FrmCambioPass();
                frmPass.ShowDialog(this);
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTicket frm = new FrmTicket();
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void ticketsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmReports rpt = new FrmReports();
                rpt.ShowDialog(this);
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
