using System;
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
                    toolStripStatusLabel1.Text = "Usuario: " + Globals.usuario.ToUpper() + "  ";
                    lblComputadora.Text = "Computadora: " + Environment.MachineName.ToUpper();
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

        

        private void GestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUsuario frmU = new FrmUsuario();
                frmU.ShowDialog(this);
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

       

        private void CambiarContraseniaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            try
            {
                FrmCambioPass pass = new FrmCambioPass();
                pass.ShowDialog(this);
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void nuevoClienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                FrmClientes frmCl = new FrmClientes();
                frmCl.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void administrarUnidadesDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmUnidadMedida unidad = new FrmUnidadMedida();
                unidad.ShowDialog(this);
            }catch(Exception ex)
            {
                Helper.erroLog(ex);
            }
        }
    }
}
