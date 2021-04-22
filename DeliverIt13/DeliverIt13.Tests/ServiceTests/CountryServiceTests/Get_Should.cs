using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CountryServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Throw_When_CountryNotFound()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_CountryNotFound));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);

                Assert.ThrowsException<Exception>(() => sut.Get(1));
            }
        }

        [TestMethod]
        public void ReturnCorrectEntity()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectEntity));

            var country = new Country
            {
                CountryId = 10,
                Name = "Bulgaria"
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Countries.Add(country);
                arrangeContext.SaveChanges();
            }

            //Act
            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);
                var result = sut.Get(10);

                //Assert
                Assert.AreEqual(country.CountryId, result.CountryId);
                Assert.AreEqual(country.Name, result.Name);
            }

        }

        [TestMethod]
        public void ReturnCorrectType()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectType));

            var country = new Country
            {
                CountryId = 10,
                Name = "Bulgaria"
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Countries.Add(country);
                arrangeContext.SaveChanges();
            }

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);

                var result = sut.Get(10);

                Assert.IsInstanceOfType(result, typeof(CountryDTO));
            }
        }
    }
}
