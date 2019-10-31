using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioTransaccion : RepositorioBase<Transacciones>
    {

        private RepositorioBase<Clientes> repositorio = new RepositorioBase<Clientes>();
        public override Transacciones Buscar(int id)
        {
            Transacciones transacciones = new Transacciones();
            Contexto db = new Contexto();
            try
            {
                transacciones = db.Transacciones.Include(x => x.Detalle).Where(x => x.TransaccionId == id).FirstOrDefault();
                if (transacciones != null)
                    transacciones.NombreCliente = repositorio.Buscar(transacciones.ClienteID).Nombres;
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return transacciones;
        }

        public override bool Eliminar(int id)
        {
            Transacciones entity = Buscar(id);
            Clientes clientes = repositorio.Buscar(entity.ClienteID);
            foreach (var item in entity.Detalle)
            {
                if (item.TipoTransaccion == TipoTransaccion.Venta)
                    clientes.Balance -= item.Monto;
                else if (item.TipoTransaccion == TipoTransaccion.Pago)
                    clientes.Balance += item.Monto;
            }
            if (repositorio.Modificar(clientes))
                return base.Eliminar(id);
            else
                return false;
        }

        public override List<Transacciones> GetList(Expression<Func<Transacciones, bool>> expression)
        {
            List<Transacciones> Lista = new List<Transacciones>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.Set<Transacciones>().AsNoTracking().Where(expression).ToList();
                if (Lista != null)
                {
                    foreach (var item in Lista)
                    {
                        item.NombreCliente = repositorio.Buscar(item.ClienteID).Nombres; ;
                    }
                }

            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return Lista;
        }

        public override bool Guardar(Transacciones entity)
        {
            Clientes clientes = repositorio.Buscar(entity.ClienteID);
            foreach (var item in entity.Detalle)
            {
                if (item.TipoTransaccion == TipoTransaccion.Venta)
                    clientes.Balance += item.Monto;
                else if (item.TipoTransaccion == TipoTransaccion.Pago)
                    clientes.Balance -= item.Monto;
            }
            if (repositorio.Modificar(clientes))
                return base.Guardar(entity);
            else
                return false;
        }

        public override bool Modificar(Transacciones entity)
        {
            bool paso = false;
            Transacciones Anterior = Buscar(entity.TransaccionId);
            Clientes clientes = repositorio.Buscar(entity.ClienteID);
            //Anterior.Detalle.ForEach(x => clientes.Balance -= x.Monto);
            Contexto db = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in Anterior.Detalle.ToList())
                    {
                        if (!entity.Detalle.Exists(x => x.DetalleId == item.DetalleId))
                        {
                            if (item.TipoTransaccion == TipoTransaccion.Venta)
                                clientes.Balance -= item.Monto;
                            else if (item.TipoTransaccion == TipoTransaccion.Pago)
                                clientes.Balance += item.Monto;
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in entity.Detalle.ToList())
                {
                    var estado = EntityState.Unchanged;
                    if (item.DetalleId == 0)
                    {
                        if (item.TipoTransaccion == TipoTransaccion.Venta)
                            clientes.Balance += item.Monto;
                        else if (item.TipoTransaccion == TipoTransaccion.Pago)
                            clientes.Balance -= item.Monto;
                        estado = EntityState.Added;
                    }
                    db.Entry(item).State = estado;
                }
                db.Entry(entity).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return paso;
        }
    }
}
