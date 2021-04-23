using DeliverIt13.Data;
using DeliverIt13.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CustomerServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Should_ThrowWhen_CustomerToGet_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_CustomerToGet_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.ThrowsException<Exception>(() => sut.Get(1));
            }
        }
    }
}
