using ApiPagos.Core.Interface;
using ApiPagos.Core.Service;
using ApiPagos.Infrastruture;
using ApiPagos.Models.Facturas;
using ApiPagos.Models.Logistica;
using ApiUnitTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiUnitTest
{
    [TestClass]
    public class ApiPagosUnitTest
    {
        DatosFactura _DFactura;
        IFacturasService _service;
        IConsumerServices _consumer;
        ILogisticaService _logistica;
        IConfiguration _config;

        [TestInitialize]
        public void Initialize()
        {
            _consumer = Substitute.For<IConsumerServices>();
            _logistica = Substitute.For<ILogisticaService>();
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();
            _service = new FacturasService(_consumer, _logistica, _config);
            _DFactura = new DatosFactura();
        }

        [TestMethod]
        public async Task TestGenerarFactura()
        {
            //Arrange
            var request = _DFactura.RequestFacturas();
            var response = _DFactura.ResponseFacturas();
            var logistica = response.Logistica;

            _consumer.PostDataRest<ResponseFacturasDto>(Arg.Any<string>(), Arg.Any<StringContent>()).Returns(response);
            _logistica.GenerarLogistica(Arg.Any<Logistica>()).Returns(logistica);

            //Act
            var result = await _service.GenerarFactura(request);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Logistica);
            Assert.IsNotNull(result.factura);
            Assert.IsNotNull(result.Productos);

            Assert.AreEqual("Cali", result.Logistica.Ciudad);
            Assert.AreEqual("Direccion de pruebas", result.Logistica.Direccion);
            Assert.AreEqual(100, result.Logistica.Id);
            Assert.AreEqual("true", result.Logistica.Estado);
            Assert.AreEqual("Ok", result.Logistica.Descripcion);

            Assert.AreEqual(Guid.Parse("9d6872fd-5c92-4443-843f-69febccac958"), result.factura.Id);
            Assert.AreEqual(300, result.factura.ClienteId);
            Assert.AreEqual(12000, result.factura.Total);
            Assert.IsNotNull(result.factura.FechaFactura.ToString());

            Assert.AreEqual(1, result.Productos[0].Id);
            Assert.AreEqual(3, result.Productos[0].ProductoId);
            Assert.AreEqual(20, result.Productos[0].Cantidad);
            Assert.AreEqual(600, result.Productos[0].Precio);

        }

        [TestMethod]
        public async Task Test()
        {
            //Arrange          
            var response = _DFactura.ResponseFacturas();
            var logistica = response.Logistica;
            var _id = Guid.Parse("9D6872FD-5C92-4443-843F-69FEBCCAC958");

            _consumer.GetDataRest<ResponseFacturasDto>(Arg.Any<string>(), Arg.Any<string>()).Returns(response);
            _logistica.Consultar(Arg.Any<Guid>()).Returns(logistica);
            //Act
            var result = await _service.ConsultarPorIdFactura(_id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Logistica);
            Assert.IsNotNull(result.factura);
            Assert.IsNotNull(result.Productos);

            Assert.AreEqual("Cali", result.Logistica.Ciudad);
            Assert.AreEqual("Direccion de pruebas", result.Logistica.Direccion);
            Assert.AreEqual(100, result.Logistica.Id);
            Assert.AreEqual("true", result.Logistica.Estado);
            Assert.AreEqual("Ok", result.Logistica.Descripcion);

            Assert.AreEqual(Guid.Parse("9d6872fd-5c92-4443-843f-69febccac958"), result.factura.Id);
            Assert.AreEqual(300, result.factura.ClienteId);
            Assert.AreEqual(12000, result.factura.Total);
            Assert.IsNotNull(result.factura.FechaFactura.ToString());

            Assert.AreEqual(1, result.Productos[0].Id);
            Assert.AreEqual(3, result.Productos[0].ProductoId);
            Assert.AreEqual(20, result.Productos[0].Cantidad);
            Assert.AreEqual(600, result.Productos[0].Precio);
        }
    }
}
