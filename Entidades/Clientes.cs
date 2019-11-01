using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public decimal BalanceOriginal { get; set; }
        public decimal Balance { get; set; } 
        public Clientes()
        {
            ClienteId = 0;
            Nombres = string.Empty;
            Balance = 0;
            BalanceOriginal = 0;
        }

        public Clientes(int clienteId, string nombres, decimal balance)
        {
            ClienteId = clienteId;
            Nombres = nombres ?? throw new ArgumentNullException(nameof(nombres));
            Balance = balance;
            BalanceOriginal = 0;
        } 
    }
}
