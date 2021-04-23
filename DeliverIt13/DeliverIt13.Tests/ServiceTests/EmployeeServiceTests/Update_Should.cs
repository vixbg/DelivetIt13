using DeliverIt13.Data;
using DeliverIt13.Data.Models;
using DeliverIt13.Services;
using DeliverIt13.Services.Models.UserDTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt13.Tests.ServiceTests.EmployeeServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Should_Successfully_UpdateEmployee()
        {
            var options = Utils.GetOptions(nameof(Should_Successfully_UpdateEmployee));

            var employee = new Employee()
            {
                EmployeeId = 1,
                FirstName = "Pesho",
                LastName = "Gosho",
                User = new User(),
                Warehouse = new Warehouse()
            };

            //Arrange
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Employees.Add(employee);
                arrangeContext.SaveChanges();
            }

            var employeeDTO = new EmployeeUpdateDTO()
            {
                EmployeeId = 1,
                UserId = 1,
                FirstName = "Petkan",
                LastName = "Ivan",
                WarehouseId = 1
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new EmployeeService(context);

                var updatedEmployee = sut.Update(employeeDTO);

                var actual = context.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                Assert.IsNotNull(actual);
                Assert.AreEqual(updatedEmployee.FirstName, actual.FirstName);
                Assert.AreEqual(updatedEmployee.LastName, actual.LastName);
            }

        }

        [TestMethod]
        public void Should_ThrowWhen_ParamsAreNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_ParamsAreNull));

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

                Assert.ThrowsException<Exception>(() => sut.Update(null));
            }
        }

        [TestMethod]
        public void Should_ThrowWhen_EmployeeToUpdate_IsNull()
        {
            var options = Utils.GetOptions(nameof(Should_ThrowWhen_EmployeeToUpdate_IsNull));

            var employeeDTO = new EmployeeUpdateDTO()
            {
                EmployeeId = 1,
                UserId = 1,
                FirstName = "Petkan",
                LastName = "Ivan",
                WarehouseId = 1
            };

            using (var context = new DeliverItContext(options))
            {
                var sut = new EmployeeService(context);

                Assert.ThrowsException<Exception>(() => sut.Update(employeeDTO));
            }
        }
    }
}
