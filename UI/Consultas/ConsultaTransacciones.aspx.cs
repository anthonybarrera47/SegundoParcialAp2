using BLL;
using Entidades;
using Extensores;
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
                lista = repositorio.GetList(filtro).Where(x => x.Fecha >= fechaDesde && x.Fecha <= FechaHasta).ToList();
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
            Clientes clientes = repositorioClientes.Buscar(transaccion.ClienteID);
            List<Transacciones> Itemtransacciones = new List<Transacciones>();
            List<TransaccionDetalle> DetalleTransaccion = new List<TransaccionDetalle>();
            Itemtransacciones.Add(transaccion);
            decimal BalanceOriginal = clientes.BalanceOriginal;
            foreach(var item in transaccion.Detalle)
            {
                TransaccionDetalle details = new TransaccionDetalle();
               
                if (item.TipoTransaccion == TipoTransaccion.Venta)
                {
                    details.DetalleId = item.DetalleId;
                    details.TipoTransaccion = item.TipoTransaccion;
                    details.Suma = item.Monto;
                    details.Resta = 0;
                    details.Balance = details.Suma - details.Resta;
                }else if (item.TipoTransaccion == TipoTransaccion.Pago)
                {
                    details.DetalleId = item.DetalleId;
                    details.TipoTransaccion = item.TipoTransaccion;
                    details.Suma = 0;
                    details.Resta = item.Monto;
                    details.Balance = details.Suma - details.Resta;
                }
                DetalleTransaccion.Add(details);
            }
        }
    }
}