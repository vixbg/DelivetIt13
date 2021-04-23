using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CustomerServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Should_DeleteSuccessfully_When_ParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(Should_DeleteSuccessfully_When_ParamsAreValid));

            var customer = new Customer()
            {
                CustomerId = 1,
                FirstName = "Pesho",
                LastName = "Gosho",
                User = new User(),
                City = new City(),
                Street = "Dolno nanagornishte"
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.Add(customer);
                arrangeContext.SaveChanges();
            }

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.IsTrue(sut.Delete(1));

            }

        }

        [TestMethod]
        public void Should_ThrowWhen_CustomerToDelete_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_CustomerToDelete_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.ThrowsException<Exception>(() => sut.Delete(1));
            }
        }
    }
}
