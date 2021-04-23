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
    public class CreateShould
    {
        //[TestMethod]
        public void Should_SuccessfullyCreateWarehouse()
        {
            var options = Utils.GetOptions(nameof(Should_SuccessfullyCreateWarehouse));

            var warehouseDTO = new WarehouseCreateDTO
            {
                CityId = 1,
                Street = "bul. Vitosha",
                WarehouseType = Data.Enums.WarehouseType.Main
            };


            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);
                var warehouse = sut.Create(warehouseDTO);


                //var actual = context.Warehouses.FirstOrDefault(x => x.WarehouseId == warehouse.WarehouseId);
                //Assert.IsNotNull(actual);
                //Assert.AreEqual(warehouseDTO.Street, actual.Street);
            }

        }

        [TestMethod]
        public void Should_ThrowWhen_ParamsAreNull()
        {
            var optiions = Utils.GetOptions(nameof(Should_ThrowWhen_ParamsAreNull));

            using (var context = new DeliverItContext(optiions))
            {
                var sut = new WarehouseService(context);

                Assert.ThrowsException<Exception>(() => sut.Create(null));
            }
        }
    }
}
