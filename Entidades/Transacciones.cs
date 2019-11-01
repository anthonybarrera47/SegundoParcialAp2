using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Transacciones
    {
        [Key]
        public int TransaccionId { get; set; }
        public DateTime Fecha { get; set; } 
        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public virtual Clientes Clientes { get; set; }
        public virtual List<TransaccionDetalle> Detalle { get; set; }
        [NotMapped]
        public string NombreCliente { get; set; }
        public Transacciones()
        {
            TransaccionId = 0;
            ClienteID = 0;
            Fecha = DateTime.Now; 
            Detalle = new List<TransaccionDetalle>();
        }
        public Transacciones(int transaccionId, int cliente, DateTime fecha )
        {
            TransaccionId = transaccionId;
            ClienteID = cliente;
            Fecha = fecha; 
            Detalle = new List<TransaccionDetalle>();
        }
        public void AgregarDetalle(int id, int transaccion, TipoTransaccion tipo, decimal Monto)
        {
            if (Detalle != null)
                Detalle.Add(new TransaccionDetalle(id, transaccion, tipo, Monto));
        }
        public void AgregarDetalle(TransaccionDetalle transaccion)
        {
            if (Detalle != null)
                Detalle.Add(transaccion);
        }
    }
}
