using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CityServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Throw_When_ThereAreNoCities()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_ThereAreNoCities));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);

                Assert.ThrowsException<Exception>(() => sut.GetAll());
            }
        }

        [TestMethod]
        public void ReturnCorrectCollection()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectCollection));

            var cities = new List<City>()
            {
                new City
                {
                    CityId = 10,
                    Name = "Blagoevgrad",
                    CountryId = 10,
                    Country = new Country()
                },

                new City
                {
                    CityId = 11,
                    Name = "Blagoevgrad1",
                    CountryId = 10,
                    Country = new Country()
                },

                new City
                {
                    CityId = 12,
                    Name = "Blagoevgrad2",
                    CountryId = 10,
                    Country = new Country()
                },
            };          
        

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.AddRange(cities);
                arrangeContext.SaveChanges();
            }

            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);

                var citiesList = sut.GetAll();


                Assert.AreEqual(cities.Count, citiesList.Count);
            }
        }
               
    }
}
