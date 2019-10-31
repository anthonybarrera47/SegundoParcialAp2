using System;
using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SegundoParcialTest
{
    [TestClass]
    public class TransaccionesTest
    {
        RepositorioTransaccion repositorio = new RepositorioTransaccion();
        [TestMethod]
        public void Guardar()
        {
            Transacciones transacciones = new Transacciones()
            {
                TransaccionId = 0,
                ClienteID = 1,
                Fecha = DateTime.Now,
            };
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Venta, 3378.72M);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 662.17M);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 864.72M);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Venta, 1603);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 399.27M);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 104);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Venta, 783);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 192.14M);
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 412);
            Assert.IsTrue(repositorio.Guardar(transacciones));
        }
        [TestMethod]
        public void Modificar()
        {
            //El ID puede variar  
            Transacciones transacciones = repositorio.Buscar(1);
            transacciones.ClienteID = 1;
            transacciones.Fecha = DateTime.Now;
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Pago, 500);
            Assert.IsTrue(repositorio.Modificar(transacciones));
        }
        [TestMethod]
        public void Eliminar()
        {
            //El ID puede variar 
            Assert.IsTrue(repositorio.Eliminar(1));
        }
    }
}
