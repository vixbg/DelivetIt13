using DeliverIt13.Data;
using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;
using DeliverIt13.Services.Contracts;
using DeliverIt13.Services.Models.CustomerDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt13.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverItContext dbContext;
        public CustomerService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CustomerCreateDTO Create(CustomerCreateDTO customer)
        {
            if (customer == null)
            {
                throw new Exception("Input Parcel is Empty or Null.");
            }

            Customer newCustomer = new Customer()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserId = customer.UserId,
                CityId = customer.CityId,
                Street = customer.Street
            };

            this.dbContext.Add(newCustomer);
            this.dbContext.SaveChanges();

            return customer;
        }

        public bool Delete(int id)
        {
            var customer = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new Exception($"User with id {id} does not exist.");
            }

            this.dbContext.Remove(customer);
            this.dbContext.SaveChanges();

            return true;
        }

        public CustomerGetDTO Get(int id)
        {
            var customer = this.dbContext.Customers
                .Include(c => c.User)
                .Include(c => c.City)
                .ThenInclude(ci => ci.Country)
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                throw new Exception($"Customer with id {id} does not exist.");
            }

            var customerDTO = new CustomerGetDTO(customer);

            return customerDTO;
        }

        public List<CustomerGetDTO> GetAll()
        {
            var customers = this.dbContext.Customers
                .Include(c => c.User)
                .Include(c => c.City)
                .ThenInclude(ci => ci.Country)
                .ToList();

            if (customers.Count == 0)
            {
                throw new Exception("No customers found.");
            }

            var customerList = customers.Select(c => new CustomerGetDTO(c)).ToList();

            return customerList;
        }

        public int GetCount()
        {
            return this.dbContext.Customers.Count();
        }

        public List<CustomerGetDTO> GetAllBySearch(string search, string searchby)
        {
            //TODO: Search By Multiple Criteria
            var customers = this.dbContext.Customers
                .Include(c => c.User)
                .Include(c=>c.City)
                .ThenInclude(ci=>ci.Country)
                .ToList();

            if (customers.Count == 0)
            {
                throw new Exception("No users found.");
            }

            if (string.IsNullOrEmpty(search))
            {
                return customers.Select(c => new CustomerGetDTO(c)).ToList();
            }

            if (string.IsNullOrEmpty(searchby))
            {
                customers = customers
                    .Where(c => 
                                c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                                c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                                c.User.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList() ?? throw new Exception("No matches found.");
            }

            if (searchby == "firstname")
            {
                customers = customers.Where(c => c.FirstName.Equals(search, StringComparison.OrdinalIgnoreCase)).ToList() ?? throw new Exception("No matches found.");
            }

            if (searchby == "lastname")
            {
                customers = customers.Where(c => c.LastName.Equals(search, StringComparison.OrdinalIgnoreCase)).ToList() ?? throw new Exception("No matches found.");
            }

            if (searchby == "email")
            {
                customers = customers.Where(c => c.User.Email.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList() ?? throw new Exception("No matches found.");
            }


            var customersDTO = customers.Select(c => new CustomerGetDTO(c)).ToList();

            return customersDTO;
        }

        public CustomerUpdateDTO Update(CustomerUpdateDTO customerDTO)
        {
            if (customerDTO == null)
            {
                throw new Exception("Input customer cannot be null or empty!");
            }

            var customer = this.dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerDTO.CustomerId);

            if (customer == null)
            {
                throw new Exception("No customer found with this ID.");
            }

            customer.CityId = customerDTO.CityId;
            customer.Street = customerDTO.Street;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;

            this.dbContext.SaveChanges();

            return customerDTO;


        }
    }
}
