using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.WarehouseServiceTests
{
    [TestClass]
    public class Get_Should
    {
        //[TestMethod]
        public void Should_GetCorrect_When_ParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_WarehoudeToGet_IsNull));

            var warehouse = new Warehouse()
            {
                WarehouseId = 10,
                Street = "Dolno nanagornishte",
                CityId = 1,               
                Type = Data.Enums.WarehouseType.Regional,
                City = new City()

            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.Add(warehouse);
                arrangeContext.SaveChanges();
            }

            using(var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);
                var result = sut.Get(10);

                Assert.AreEqual(result.WarehouseId, warehouse.WarehouseId);
                Assert.AreEqual(result.WarehouseType, warehouse.Type);
                Assert.AreEqual(result.Street, warehouse.Street);
                Assert.AreEqual(result.CityId, warehouse.CityId);
                Assert.AreEqual(result.City, warehouse.City);
            }
        }

        [TestMethod]
        public void Should_ThrowWhen_WarehoudeToGet_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_WarehoudeToGet_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                Assert.ThrowsException<Exception>(() => sut.Get(1));
            }
        }
    }
}
