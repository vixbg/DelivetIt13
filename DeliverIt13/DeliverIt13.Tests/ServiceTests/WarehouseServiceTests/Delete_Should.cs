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
    public class Delete_Should
    {

        [TestMethod]
        public void Should_DeleteSuccessfully_When_ParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(Should_DeleteSuccessfully_When_ParamsAreValid));

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

            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                Assert.IsTrue(sut.Delete(10));

            }

        }

        [TestMethod]
        public void Should_ThrowWhen_WarehoudeToDelete_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_WarehoudeToDelete_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                Assert.ThrowsException<NullReferenceException>(() => sut.Delete(1));
            }
        }
    }
}
