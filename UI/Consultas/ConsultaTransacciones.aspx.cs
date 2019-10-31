using BLL;
using Entidades;
using Extensores;
using Herramientas;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SegundoParcialAp2.UI.Consultas
{
    public partial class ConsultaTransacciones : System.Web.UI.Page
    {
        static List<Transacciones> lista = new List<Transacciones>();
        RepositorioTransaccion repositorio = new RepositorioTransaccion();
        RepositorioBase<Clientes> repositorioClientes = new RepositorioBase<Clientes>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechaDesdeTextBox.Text = DateTime.Now.ToFormatDate();
                FechaHastaTextBox.Text = DateTime.Now.ToFormatDate();
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Expression<Func<Transacciones, bool>> filtro = x => true;
            int id;
            switch (BuscarPorDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = x => true;
                    break;
                case 1://ID
                    id = (FiltroTextBox.Text).ToInt();
                    filtro = x => x.TransaccionId == id;
                    break;
                case 2:// nombre
                    id = (FiltroTextBox.Text).ToInt();
                    filtro = x => x.ClienteID == id;
                    break;
            }
            DateTime fechaDesde = FechaDesdeTextBox.Text.ToDatetime();
            DateTime FechaHasta = FechaHastaTextBox.Text.ToDatetime();
            if (FechaCheckBox.Checked)
                lista = repositorio.GetList(filtro).Where(x => x.Fecha.Date >= fechaDesde && x.Fecha.Date <= FechaHasta).ToList();
            else
                lista = repositorio.GetList(filtro);
            repositorio.Dispose();
            this.BindGrid(lista);
        }

        private void BindGrid(List<Transacciones> lista)
        {
            DatosGridView.DataSource = null;
            DatosGridView.DataSource = lista;
            DatosGridView.DataBind();
        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            int Transaccion = lista.ElementAt(row.RowIndex).TransaccionId;
            Transacciones transaccion = repositorio.Buscar(Transaccion);

            List<Clientes> ItemClientes = new List<Clientes>();
            List<Transacciones> Itemtransacciones = new List<Transacciones>();
            List<TransaccionDetalle> DetalleTransaccion = new List<TransaccionDetalle>();
            ItemClientes.Add(repositorioClientes.Buscar(transaccion.ClienteID));
            Itemtransacciones.Add(transaccion);
            decimal Balance = 0;
            foreach(var item in transaccion.Detalle)
            {
                TransaccionDetalle details = new TransaccionDetalle();
               
                if (item.TipoTransaccion == TipoTransaccion.Venta)
                {
                    details.DetalleId = item.DetalleId;
                    details.TipoTransaccion = item.TipoTransaccion;
                    details.Tipo = "Venta";
                    details.Suma = item.Monto;
                    details.Resta = 0;
                    Balance += item.Monto;
                    details.Balance = Balance;
                }else if (item.TipoTransaccion == TipoTransaccion.Pago)
                {
                    details.DetalleId = item.DetalleId;
                    details.TipoTransaccion = item.TipoTransaccion;
                    details.Tipo = "Pago";
                    details.Suma = 0;
                    details.Resta = item.Monto;
                    Balance -= item.Monto;
                    details.Balance = Balance;
                }
                DetalleTransaccion.Add(details);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Popup", $"ShowReporte('Recibo de estudiante');", true);
            ReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReportViewer.Reset();
            ReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\UI\Reporte\ListadoEstadoCuentasClientes.rdlc");
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Clientes",
                                                   ItemClientes));

            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Detalle",
                                                   DetalleTransaccion));
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Transacciones",
                                                   Itemtransacciones));
            ReportViewer.LocalReport.Refresh();
        }
    }
}