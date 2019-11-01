using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TransaccionDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public DateTime Fecha { get; set; }
        public int TransanccionID { get; set; }
        [ForeignKey("TransanccionID")]
        public virtual Transacciones Transacciones { get; set; }
        public TipoTransaccion TipoTransaccion { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public decimal Suma { get; set; }
        public decimal Resta { get; set; }
        public decimal Balance { get; set; }

        public TransaccionDetalle(int detalleId, int transanccionID, TipoTransaccion tipoTransaccion, decimal monto)
        {
            DetalleId = detalleId;
            TransanccionID = transanccionID;
            TipoTransaccion = tipoTransaccion;
            Tipo = string.Empty;
            Monto = monto;
            Suma = 0;
            Resta = 0;
            Balance = 0;
            Fecha = DateTime.Now;
        }
        public TransaccionDetalle()
        {
            DetalleId = 0;
            TransanccionID = 0;
            TipoTransaccion = TipoTransaccion.Venta;
            Tipo = string.Empty;
            Monto = 0; 
            Suma = 0;
            Resta = 0;
            Balance = 0;

            Fecha = DateTime.Now;
        }
    }
}
