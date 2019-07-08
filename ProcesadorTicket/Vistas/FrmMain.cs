﻿using ProcesadorTicket.Core.DA;
using System;
using System.Windows.Forms;

namespace ProcesadorTicket
{
    public partial class FrmMain : Form
    {
        DASeguridad seguridad = new DASeguridad();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                autenticacion();
                UCInicio frm = new UCInicio();
                frm.Dock = DockStyle.Fill;
                nuevaPestana(frm, "Inicio");
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            int indice = tabMain.SelectedIndex;
            if (indice > 0) //no queremos cerrar nunca la inicial.
            {
                tabMain.TabPages.Remove(tabMain.SelectedTab);
                tabMain.SelectedIndex = indice - 1;
                GC.Collect();
            }

        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Tickets"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCTicket frm = new UCTicket();
                frm.Dock = DockStyle.Fill;
                nuevaPestana(frm, "Tickets");
                //frm.ShowDialog(this);
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
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Reportes"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                // FrmReports rpt = new FrmReports();
                // rpt.ShowDialog(this);
                UCReporteTickets reports = new UCReporteTickets();
                reports.Dock = DockStyle.Fill;
                nuevaPestana(reports, "Reportes");

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }



        private void GestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmUsuario frmU = new FrmUsuario();
                //frmU.ShowDialog(this);}
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Usuarios"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCUsuario usuario = new UCUsuario();
                usuario.Dock = DockStyle.Fill;
                nuevaPestana(usuario, "Usuarios");
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void nuevoClienteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Clientes"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCClientes clientes = new UCClientes();
                clientes.Dock = DockStyle.Fill;
                nuevaPestana(clientes, "Clientes");
                //FrmClientes frmCl = new FrmClientes();
                //frmCl.ShowDialog(this);
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
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void AdministrarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Productos"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                //FrmProducto producto = new FrmProducto();
                //producto.ShowDialog(this);
                UCProducto producto = new UCProducto();
                producto.Dock = DockStyle.Fill;
                nuevaPestana(producto, "Productos");
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void NuevaEntradaProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Productos"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                //FrmEntradaProducto entradaProducto = new FrmEntradaProducto();
                //entradaProducto.ShowDialog(this);
                UCEntradaProductot eProducto = new UCEntradaProductot();
                eProducto.Dock = DockStyle.Fill;
                nuevaPestana(eProducto, "Ingreso de productos");

            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void nuevaPestana(UserControl uc, String titulo)
        {
            int indice = indicePestanaAbierta(titulo);
            if (indice < 0)
            {
                TabPage tab = new TabPage();
                tab.Text = titulo;
                tab.Controls.Add(uc);
                tabMain.TabPages.Add(tab);
                tabMain.SelectedTab = tab;
            }
            else
            {
                //si ya existe vamos a limpiar el usercontrol y volverlo a cargar en la misma pestaña
                tabMain.TabPages[indice].Controls.Clear();
                tabMain.TabPages[indice].Controls.Add(uc);
                tabMain.SelectedTab = tabMain.TabPages[indice];
            }

        }

        private int indicePestanaAbierta(String titulo)
        {
            Boolean enc = false;
            int i = 0;
            while ((i < tabMain.TabPages.Count) && (enc == false))
            {
                if (tabMain.TabPages[i].Text == titulo)
                    enc = true;
                else
                    i++;
            }

            return enc ? i : -1;
            /*if (enc)
                return i;
            else
                return -1;*/

        }

        private void NuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Ventas"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCVenta venta = new UCVenta();
                venta.Dock = DockStyle.Fill;
                nuevaPestana(venta, "Venta");
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void VentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Reportes"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCReporteVentas venta = new UCReporteVentas();
                venta.Dock = DockStyle.Fill;
                nuevaPestana(venta, "Reporte Ventas");
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }

        }

        private void ProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!seguridad.validarPermisoUsuario(Globals.usuario, "Reportes"))
                {
                    Helper.MensajeSistema("No tiene permisos para esta acción");
                    return;
                }
                UCReporteProductos venta = new UCReporteProductos();
                venta.Dock = DockStyle.Fill;
                nuevaPestana(venta, "Reporte Productos");
            }
            catch (Exception ex)
            {
                Helper.erroLog(ex);
            }
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
