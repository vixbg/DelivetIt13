using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DeliverIt13.Tests.ServiceTests.CityServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Throw_When_CityNotFound()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_CityNotFound));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);

                Assert.ThrowsException<Exception>(() => sut.Get(1));
            }
        }

        [TestMethod]
        public void ReturnCorrectEntity()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectEntity));

            var city = new City
            {
                CityId = 10,
                Name = "Blagoevgrad",
                CountryId = 10,
                Country = new Country()
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.Add(city);
                arrangeContext.SaveChanges();
            }

            //Act
            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);
                var result = sut.Get(10);

                //Assert
                Assert.AreEqual(city.CityId, result.CityId);
                Assert.AreEqual(city.Name, result.Name);
            }

        }

        [TestMethod]
        public void ReturnCorrectType()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectType));

            var city = new City
            {
                CityId = 10,
                Name = "Blagoevgrad",
                CountryId = 10,
                Country = new Country()
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.Add(city);
                arrangeContext.SaveChanges();
            }

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);

                var result = sut.Get(10);

                Assert.IsInstanceOfType(result, typeof(CityGetDTO));
            }
        }
    }
}
