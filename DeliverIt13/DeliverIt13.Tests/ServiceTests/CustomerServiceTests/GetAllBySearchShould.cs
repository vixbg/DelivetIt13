using DeliverIt13.Data;
using DeliverIt13.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CustomerServiceTests
{
    [TestClass]
    public class GetAllBySearchShould
    {
        [TestMethod]
        public void Should_ThrowWhen_CustomersList_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_CustomersList_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.ThrowsException<Exception>(() => sut.GetAllBySearch("", ""));
            }
        }
    }
}
