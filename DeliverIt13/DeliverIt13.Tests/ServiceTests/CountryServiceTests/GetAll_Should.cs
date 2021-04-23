using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.CountryServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Throw_When_ThereAreNoCountries()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_When_ThereAreNoCountries));

            //Act & Assert
            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);

                Assert.ThrowsException<Exception>(() => sut.GetAll());
            }
        }

        [TestMethod]
        public void ReturnCorrectCollection()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectCollection));

            var newCountries = new List<Country>()
            {
                new Country
                {
                    CountryId = 10,
                    Name = "Bulgaria"
                },

                new Country
                {
                    CountryId = 11,
                    Name = "Bulgaria1"
                },

                new Country
                {
                    CountryId = 12,
                    Name = "Bulgaria2"
                }
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Countries.AddRange(newCountries);
                arrangeContext.SaveChanges();
            }

            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);

                var countriesList = sut.GetAll();
                                
                Assert.AreEqual(newCountries.Count, countriesList.Count);
            }
        }
        
    }
}
