using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Models.CustomerDTOs;
using DeliverIt13.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CustomerServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Should_Successfully_UpdateCustomer()
        {
            var options = Utils.GetOptions(nameof(Should_Successfully_UpdateCustomer));

            var customer = new Customer()
            {
                CustomerId = 1,
                FirstName = "Pesho",
                LastName = "Gosho",
                User = new User(),
                City = new City(),
                Street = "Dolno nanagornishte"
            };

            //Arrange
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.Add(customer);
                arrangeContext.SaveChanges();
            }

            var customerDTO = new CustomerUpdateDTO
            {
                CustomerId = 1,
                FirstName = "Pesho",
                LastName = "Gosho",
                Street = "Dolno nanagornishte",
                CityId = 1,
                UserId = 1
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                var updatedCustomer = sut.Update(customerDTO);

                var actual = context.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                Assert.IsNotNull(actual);
                Assert.AreEqual(updatedCustomer.FirstName, actual.FirstName);
                Assert.AreEqual(updatedCustomer.LastName, actual.LastName);
                Assert.AreEqual(updatedCustomer.Street, actual.Street);
            }

        }

        [TestMethod]
        public void Should_ThrowWhen_ParamsAreNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_ParamsAreNull));

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

                Assert.ThrowsException<Exception>(() => sut.Update(null));
            }
        }

        [TestMethod]
        public void Should_ThrowWhen_CustomerToUpdate_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_CustomerToUpdate_IsNull));

            var customerDTO = new CustomerUpdateDTO
            {
                CustomerId = 1,
                FirstName = "Pesho",
                LastName = "Gosho",
                Street = "Dolno nanagornishte",
                CityId = 1,
                UserId = 1
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new CustomerService(context);

                Assert.ThrowsException<Exception>(() => sut.Update(customerDTO));
            }
        }
    }
}
