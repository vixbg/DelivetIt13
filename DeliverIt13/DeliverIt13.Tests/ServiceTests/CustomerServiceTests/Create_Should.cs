using DeliverIt13.Data;
using DeliverIt13.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CustomerServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Throw_When_CustomerIsNull()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_CustomerIsNull));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.ThrowsException<Exception>(() => sut.Create(null));
            }
        }
    }
}
