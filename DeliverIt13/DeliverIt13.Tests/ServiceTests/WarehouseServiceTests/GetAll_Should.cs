using DeliverIt13.Data;
using DeliverIt13.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.WarehouseServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Throw_When_ThereAreNoWarehouses()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_ThereAreNoWarehouses));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new WarehouseService(context);

                Assert.ThrowsException<Exception>(() => sut.GetAll());
            }
        }
    }
}
