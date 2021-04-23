using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.EmployeeServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Should_DeleteSuccessfully_When_ParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(Should_DeleteSuccessfully_When_ParamsAreValid));

            var employee = new Employee()
            {
                EmployeeId = 1,     
                FirstName = "Pesho",
                LastName = "Gosho",
                User = new User(),
                Warehouse = new Warehouse()
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Employees.Add(employee);
                arrangeContext.SaveChanges();
            }

            using (var context = new DeliverItContext(options))
            {
                var sut = new EmployeeService(context);

                Assert.IsTrue(sut.Delete(1));

            }

        }

        [TestMethod]
        public void Should_ThrowWhen_EmployeeToDelete_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_EmployeeToDelete_IsNull));

            using (var context = new DeliverItContext(options))
            {
                var sut = new EmployeeService(context);

                Assert.ThrowsException<Exception>(() => sut.Delete(1));
            }
        }
    }
}
