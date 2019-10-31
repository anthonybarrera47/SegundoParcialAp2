using System;
using BLL;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SegundoParcialTest
{
    [TestClass]
    public class ClienteTest
    {

        RepositorioBase<Clientes> repositorio = new RepositorioBase<Clientes>();
        [TestMethod]
        public void Guardar()
        { 
            Clientes clientes = new Clientes
            {
                ClienteId = 0,
                Nombres = "Anthony Barrera",
                Balance = 5000
            };
            clientes.BalanceOriginal = clientes.Balance;
            Assert.IsTrue(repositorio.Guardar(clientes));
        }
        [TestMethod]
        public void Modificar()
        {
            //El ID puede variar 
            Clientes clientes = new Clientes
            {
                ClienteId = 1,
                Nombres = "Anthony Manuel Barrera",
                Balance = 10000
            };
            clientes.BalanceOriginal = clientes.Balance;
            Assert.IsTrue(repositorio.Modificar(clientes));

        }
        [TestMethod]
        public void Eliminar()
        {
            //El ID puede variar 
            Assert.IsTrue(repositorio.Eliminar(1));
        }
    }
}
