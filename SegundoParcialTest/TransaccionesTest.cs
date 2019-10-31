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
                ClienteID = 2,
                Fecha = DateTime.Now,
            };
            transacciones.AgregarDetalle(0, transacciones.TransaccionId, TipoTransaccion.Venta, 500);
            Assert.IsTrue(repositorio.Guardar(transacciones));
        }
        [TestMethod]
        public void Modificar()
        {
            //El ID puede variar  
            Transacciones transacciones = new Transacciones();
            transacciones = repositorio.Buscar(1);
            transacciones.ClienteID = 2;
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
