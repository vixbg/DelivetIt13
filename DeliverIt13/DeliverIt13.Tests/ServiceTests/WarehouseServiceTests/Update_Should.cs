using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.WarehouseServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Should_Successfully_UpdateWarehouse()
        {
            var options = Utils.GetOptions(nameof(Should_Successfully_UpdateWarehouse));

            var warehouse = new Warehouse()
            {
                WarehouseId = 10,
                Street = "Dolno nanagornishte",
                CityId = 1,
                Type = Data.Enums.WarehouseType.Regional,
                City = new City()
            };

            //Arrange
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.Add(warehouse);
                arrangeContext.SaveChanges();
            }

            var warehouseDTO = new WarehouseUpdateDTO
            {                
                WarehouseId = 10,
                CityId = 1,
                Street = "bul. Vitosha",
                WarehouseType = Data.Enums.WarehouseType.Main
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                var updatedWarehouse = sut.Update(warehouseDTO);

                var actual = context.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouse.WarehouseId);
                Assert.IsNotNull(actual);
                Assert.AreEqual(updatedWarehouse.Street, actual.Street);
                Assert.AreEqual(updatedWarehouse.WarehouseType, actual.Type);
            }

        }

        [TestMethod]
        public void Should_ThrowWhen_ParamsAreNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_ParamsAreNull));

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

                Assert.ThrowsException<Exception>(() => sut.Update(null));
            }
        }

        [TestMethod]
        public void Should_ThrowWhen_WarehouseToUpdate_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_WarehouseToUpdate_IsNull));

            var warehouseDTO = new WarehouseUpdateDTO
            {
                WarehouseId = 10,
                CityId = 1,
                Street = "bul. Vitosha",
                WarehouseType = Data.Enums.WarehouseType.Main
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                Assert.ThrowsException<Exception>(() => sut.Update(warehouseDTO));
            }
        }
    }
}
